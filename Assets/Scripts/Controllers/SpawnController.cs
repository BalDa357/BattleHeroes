using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    #region SINGLETON
    private static SpawnController _instance;

    public static SpawnController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SpawnController>();

            return _instance;
        }
    }
    #endregion

    [Header("Линии спавнов игроков: ")]
    [Header("МОЙ ИГРОК: ")]
    [Header("Первая линия спавна:")]
    [SerializeField] private Plane1x1[] myFirstLine;
    [Header("Вторая линия спавна:")]
    [SerializeField] private Plane1x1[] mySecondLine;
    [Header("Третья линия спавна:")]
    [SerializeField] private Plane1x1[] myThirdLine;
    [Header("ПРОТИВНИК ИГРОК:")]
    [Header("Первая линия спавна:")]
    [SerializeField] private Plane1x1[] enemyFirstLine;
    [Header("Вторая линия спавна:")]
    [SerializeField] private Plane1x1[] enemySecondLine;
    [Header("Третья линия спавна:")]
    [SerializeField] private Plane1x1[] enemyThirdLine;


    /// <summary>
    /// Возвращает места для линии спавна
    /// </summary>
    /// <param name="side"></param>
    /// <param name="lineNumber">Номер линии (от 1 до 3)</param>
    /// <returns></returns>
    private Plane1x1[] GetSpawnLine(Side side, byte lineNumber)
    {
        switch (lineNumber)
        {
            case 1:
                return side == Side.My ? myFirstLine : enemyFirstLine;
            case 2:
                return side == Side.My ? mySecondLine : enemySecondLine;
            case 3:
                return side == Side.My ? myThirdLine : enemyThirdLine;
            default:
                Debug.Log("Error: Неверно указана линия спавна");
                return null;
        }
    }


    private void Awake()
    {
        ShowSpawnGrid();
        SpawnUnits();
        ReplaceController.Instance.selectedUnit = GameController.Instance.myUnits.First();
        
        if (GameController.Instance.gameMode == GameMode.vsBot)
            BotSpawn();
    }


    /// <summary>
    /// Спавнит юнитов с одной из сторон (немного умный спавн)
    /// </summary>
    /// <param name="side"></param>
    private void SpawnUnits(Side side = Side.My)
    {
        int unitsCount = PlayerController.Instance.GetUnitsPrefabs(side).Count;

        switch (unitsCount)
        {
            case 1:
                #region HARD_SPAWN_ONE_UNIT
                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[0], side,
                    (byte)(PlanesController.Instance.MapSize.columns / 2));

                break;
            #endregion

            case 2:
                #region HARD_SPAWN_TWO_UNITS

                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[0], side,
                    (byte)(PlanesController.Instance.MapSize.columns / 3));
                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[1], side,
                    (byte)(2 * PlanesController.Instance.MapSize.columns / 3));

                break;
            #endregion

            case 3:
                #region HARD_SPAWN_THREE_UNITS
                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[0], side, 1);

                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[1], side,
                    (byte)(PlanesController.Instance.MapSize.columns / 2));
                SmartSpawn(PlayerController.Instance.GetUnitsPrefabs(side)[2], side,
                    (byte)(PlanesController.Instance.MapSize.columns - 2));

                break;
            #endregion

            case 4:
                #region HARD_SPAWN_FOUR_UNITS

                bool is1x1Spawned = false;
                byte counter = 0;

                foreach (var unitPrefabGO in PlayerController.Instance.GetUnitsPrefabs(side))
                {
                    if (unitPrefabGO.GetComponentInChildren<Unit>().size == Size.Unit1x1 && !is1x1Spawned)
                    {
                        SmartSpawn(unitPrefabGO, side, counter);
                        counter++;
                        is1x1Spawned = true;
                    }
                    else
                    {
                        SmartSpawn(unitPrefabGO, side, counter);
                        counter += 2;
                    }
                }


                break;
            #endregion

            default:
                HardSpawnManyUnits(side);
                break;
                //byte spawnPosition = GetAndCheck2x2SpawnPosition(side, ref count2x2Units);
                //SmartSpawn(unitPrefabGO, side, spawnPosition); 


        }
    }

    /// <summary>
    /// Показать сетку для дальнейшего выставления своих игроков
    /// </summary>
    private void ShowSpawnGrid()
    {
        PlanesController.Instance.openedPlanes = new List<Plane1x1>();
        for (byte i = 1; i <= PlayerController.Instance.GetSpawnLinesCount(Side.My); i++)
        {
            foreach (var plane1x1 in GetSpawnLine(Side.My, i))
            {
                PlanesController.Instance.openedPlanes.Add(plane1x1);
                //plane1x1.SetActive(true);
            }
        }
        PlanesController.Instance.OpenAllPlanes();

        PlanesController.Instance.UpdateGrid();
    }

    /// <summary>
    /// Спавн бота с рандомом стороны
    /// </summary>
    private void BotSpawn()
    {
        if (Random.Range(0, 2) == 1)
            for (byte i = 1; i <= 3; i++)
                RevertBotSpawnLine(i);

        SpawnUnits(Side.Enemy);
    }

    /// <summary>
    /// Сменить сторону спавна для бота
    /// </summary>
    /// <param name="lineNumber">Количество линий спавна</param>
    private void RevertBotSpawnLine(byte lineNumber)
    {
        var spawnLine = GetSpawnLine(Side.Enemy, lineNumber);
        Plane1x1[] newSpawnLine = new Plane1x1[spawnLine.Length];

        for (int i = 0; i < spawnLine.Length; i++)
        {
            newSpawnLine[spawnLine.Length - i - 1] = spawnLine[i];
        }
        for (int i = 0; i < spawnLine.Length; i++)
        {
            spawnLine[i] = newSpawnLine[i];
        }
    }

    /// <summary>
    /// Спавнит юнитов в свободные места (стрелки сзади)
    /// </summary>
    /// <param name="side"></param>
    private void HardSpawnManyUnits(Side side)
    {
        byte count2x2Units = 0;
        foreach (var unitPrefabGO in PlayerController.Instance.GetUnitsPrefabs(side))
            if (unitPrefabGO.GetComponentInChildren<Unit>().size == Size.Unit2x2)
                count2x2Units++;

        if (count2x2Units == 3)
            PlayerController.Instance.SetMaxSpawnLinesCount(side);


        foreach (var unitPrefabGO in PlayerController.Instance.GetUnitsPrefabs(side))
        {
            Unit unit = unitPrefabGO.GetComponentInChildren<Unit>();
            bool isFound = false;
            byte spawnLinesCount;
            if (side == Side.Enemy)
                if (unit.size == Size.Unit2x2)
                    spawnLinesCount = (byte)(PlayerController.Instance.GetSpawnLinesCount(side) - 1);
                else
                    spawnLinesCount = PlayerController.Instance.GetSpawnLinesCount(side);
            else
                spawnLinesCount = PlayerController.Instance.GetSpawnLinesCount(side);

            if (unit.isShooter)
            {
                for (byte i = 1; i <= spawnLinesCount; i++)
                {
                    foreach (var plane in GetSpawnLine(side, i))
                    {
                        if (TrySpawnUnit(unitPrefabGO, plane, side))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (isFound) break;
                }
            }

            else
            {
                for (byte i = spawnLinesCount; i >= 1; i--)
                {
                    foreach (var plane in GetSpawnLine(side, i))
                    {
                        if (TrySpawnUnit(unitPrefabGO, plane, side))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (isFound) break;
                }
            }
        }

    }

    /// <summary>
    /// Спавнит юнита стрелков сзади, не стрелков спереди
    /// </summary>
    /// <param name="unitPrefabGO"></param>
    /// <param name="side"></param>
    /// <param name="spawnPosition">Позиция спавна (в каком столбике (column))</param>
    private void SmartSpawn(GameObject unitPrefabGO, Side side, byte spawnPosition)
    {
        Unit unit = unitPrefabGO.GetComponentInChildren<Unit>();

        if (unit.isShooter)
            TrySpawnUnit(unitPrefabGO, GetSpawnLine(side, 1)[spawnPosition], side);
        else
        {
            if (side == Side.Enemy && unit.size == Size.Unit2x2)
                TrySpawnUnit(unitPrefabGO, GetSpawnLine(side, 
                    (byte)(PlayerController.Instance.GetSpawnLinesCount(side) - 1))[spawnPosition], side);
            else
                TrySpawnUnit(unitPrefabGO, GetSpawnLine(side, 
                    PlayerController.Instance.GetSpawnLinesCount(side))[spawnPosition], side);
        }
    }

    /// <summary>
    /// Спавнит юнита на позицию указаной плиточки
    /// </summary>
    /// <param name="unitPrefabGO"></param>
    /// <param name="plane1x1"></param>
    private bool TrySpawnUnit(GameObject unitPrefabGO, Plane1x1 plane1x1, Side side)
    {
        if (plane1x1.GetEmptyPlanes(unitPrefabGO.GetComponentInChildren<Unit>().size) == null)
            return false;

        var unitGO = Instantiate(unitPrefabGO);
        Unit unit = unitGO.GetComponentInChildren<Unit>();
        unit.Initialization(plane1x1, side);

        bool isSpawned = PlanesController.Instance.TrySetAtFirstFoundPlane(plane1x1, unit);

        if (isSpawned)
            unit.currentPlane.SetEmpty(unit.size, false);

        return isSpawned;
    }

}