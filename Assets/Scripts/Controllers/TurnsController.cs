using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsController : MonoBehaviour {

    #region SINGLETON
    private static TurnsController _instance;

    public static TurnsController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TurnsController>();

            return _instance;
        }
    }
    #endregion

    /// <summary>
    /// Текущий юнит, совершающий ход
    /// </summary>
    [HideInInspector] public Unit currentTurningUnit;

    /// <summary>
    /// очередь из юнитов для даного хода
    /// </summary>
    private List<Unit> unitsQueue;


    /// <summary>
    /// Начало боя (после растановки)
    /// </summary>
    public void OnGameStart()
    {
        GenerateNewUnitsQueue();

        GetFirstUnitAsCurrent();
    }

    /// <summary>
    /// Сгенерировать новую линейку инициативы (List<Unit> unitsQueue)
    /// </summary>
    private void GenerateNewUnitsQueue()
    {
        //Создаем новую очередь(порядок) ходов юнитов
        unitsQueue = new List<Unit>();

        //Перебираем всех юнитов на карте
        foreach (GameObject unitGO in GameController.Instance.units)
        {
            //Получаем компоненту Unit конкретного юнита
            Unit unit = unitGO.GetComponentInChildren<Unit>();

            //Если пока что в очереди нету ниодного юнита, то просто добавляем первого
            if (unitsQueue.Count == 0)
            {
                unitsQueue.Add(unit);
                continue;
            }

            //Список индексов для юнитов с одинаковой инициативой
            List<int> temporaryUnitsWithEqualInitiation = new List<int>();

            //Перебираем всех юнитов добавленых в очередь
            foreach (Unit unitInQueue in unitsQueue)
            {
                //Сравниваем инициативы юнитов в очереди с инициативой того, которого хотим добавить

                //если инициатива нового юнита больше текущего, то ставим его на позицию текущего
                if (unit.initiative > unitInQueue.initiative)
                {
                    unitsQueue.Insert(unitsQueue.IndexOf(unitInQueue), unit);
                    return;
                }

                //если инициатива нового юнита равна инициативе текущего то добаляем в список для рандома 
                //(перед ним/ими или после него/их [или между ими])
                if (unit.initiative == unitInQueue.initiative)
                {
                    temporaryUnitsWithEqualInitiation.Add(unitsQueue.IndexOf(unitInQueue));
                }

                //если инициатива нового юнита меньше текущего
                if (unit.initiative < unitInQueue.initiative)
                {
                    //если список для рандома не пуст (были найдены юниты с такой же инициативой как у нового юнита)
                    if (temporaryUnitsWithEqualInitiation.Count > 0)
                    {
                        //Рандом из минимальной и максимальной позиции на которую может стать юнит в линейке инициативы
                        int randomIndex =
                            Random.Range(temporaryUnitsWithEqualInitiation[0],
                            temporaryUnitsWithEqualInitiation[temporaryUnitsWithEqualInitiation.Count - 1] + 1);

                        //Если зарандомленная позиция не последняя, то вставляем новый юнит в нее
                        if (randomIndex < unitsQueue.Count)
                            unitsQueue.Insert(randomIndex, unit);
                        //иначе добавляем в конец
                        else
                            unitsQueue.Add(unit);
                    }
                    //если список для рандома пуст (небыли найдены юниты с меньшей или
                    //такой же инициативой как у нового юнита), то этот юнит последний
                    else
                    {
                        unitsQueue.Add(unit);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Получить первого юнита из очереди
    /// </summary>
    private void GetFirstUnitAsCurrent()
    {
        if (unitsQueue.Count > 0)
            currentTurningUnit = unitsQueue[0];
        else
            Debug.Log("В ИГРЕ НЕТУ ЮНИТОВ!!! LOL");
    }

    /// <summary>
    /// Ход следующего юнита
    /// </summary>
    public void NextTurn()
    {
        int indexOfCurrentUnit = unitsQueue.IndexOf(currentTurningUnit);

        if (indexOfCurrentUnit == unitsQueue.Count - 1)
        {
            GenerateNewUnitsQueue();
            GetFirstUnitAsCurrent();
        }
        else
        {
            currentTurningUnit = unitsQueue[indexOfCurrentUnit+1];
        }
    }



}
