using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour {


    #region SINGLETON
    private static ClickController _instance;

    public static ClickController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ClickController>();

            return _instance;
        }
    }
    #endregion


    //БЛОК ОБРАБОТКИ НАЖАТИЯ НА ЮНИТОВ
    #region OnUnitClick

    private void OnTargetMouseDown(Unit unit)
    {
        ReplaceController.Instance.TakeUnit(unit);
    }

    private void OnTargetMousePressed(Unit unit)
    {
        if (GameController.Instance.stage != Stage.Spawning)
        {
            //TODO: начать бить
        }
    }

    private void OnTargetMouseUp(Unit unit)
    {
    }

    #endregion
    //--------------------------------




    //БЛОК ОБРАБОТКИ НАЖАТИЯ НА Plane1x1
    #region OnPlane1x1Click

    private void OnTargetMouseDown(Plane1x1 plane1x1)
    {
    }

    private void OnTargetMousePressed(Plane1x1 plane1x1)
    {
        if (GameController.Instance.stage == Stage.Spawning)
        {
            if (ReplaceController.Instance.currentReplacingUnit != null)
            {
                Plane1x1 targetPlane1x1 = plane1x1.GetComponent<Plane1x1>();
                if (targetPlane1x1 != null)//if plane
                {
                    PlanesController.Instance.TrySetAtFirstFoundPlane(targetPlane1x1);
                }
            }
        }
    }

    private void OnTargetMouseUp(Plane1x1 plane1x1)
    {
    }

    #endregion
    //--------------------------------



    //БЛОК ОБРАБОТКИ НАЖАТИЯ НА SpawnUnitItem
    #region OnSpawnUnitItemClick

    private void OnTargetMouseDown(SpawnUnitItem spawnUnitItem)
    {
        if (GameController.Instance.stage == Stage.Spawning)
        {
            ReplaceController.Instance.SelectUnit(spawnUnitItem.unit);
        }
    }

    private void OnTargetMousePressed(SpawnUnitItem spawnUnitItem)
    {

    }

    private void OnTargetMouseUp(SpawnUnitItem spawnUnitItem)
    {
    }

    #endregion
    //--------------------------------



    //БЛОК ОБРАБОТКИ ВСЕХ НАЖАТИЙ
    //если необходимо добавить возможность нажатия на новый тип, нужно добвать новый таг для этого типа на объектах,
    //добавить проверку на него в switch case а так же перегрузить для него методы OnTargetMouseClick (см. выше)
    #region Click
    public void OnRaycastMouseDown(GameObject targetGO)
    {
        OnTargetMouseClick(targetGO, Mouse.Down);
    }

    public void OnRaycastMousePressed(GameObject targetGO)
    {
        OnTargetMouseClick(targetGO, Mouse.Pressed);
    }

    public void OnRaycastMouseUp(GameObject targetGO)
    {
        OnTargetMouseClick(targetGO, Mouse.Up);
    }


    /// <summary>
    /// Проверка на тип на который совершен клик (совершается по тагу)
    /// </summary>
    /// <param name="targetGO"></param>
    /// <param name="mouse"></param>
    private void OnTargetMouseClick(GameObject targetGO, Mouse mouse)
    {
        switch (targetGO.tag)
        {
            case "Unit":
                switch (mouse)
                {
                    case Mouse.Down:
                        OnTargetMouseDown(targetGO.GetComponent<Unit>());
                        break;
                    case Mouse.Pressed:
                        OnTargetMousePressed(targetGO.GetComponent<Unit>());
                        break;
                    case Mouse.Up:
                        OnTargetMouseUp(targetGO.GetComponent<Unit>());
                        break;
                }
                break;

            case "Plane 1x1":
                switch (mouse)
                {
                    case Mouse.Down:
                        OnTargetMouseDown(targetGO.GetComponent<Plane1x1>());
                        break;
                    case Mouse.Pressed:
                        OnTargetMousePressed(targetGO.GetComponent<Plane1x1>());
                        break;
                    case Mouse.Up:
                        OnTargetMouseUp(targetGO.GetComponent<Plane1x1>());
                        break;
                }
                break;

            case "Spawn Unit Item":
                switch (mouse)
                {
                    case Mouse.Down:
                        OnTargetMouseDown(targetGO.GetComponent<SpawnUnitItem>());
                        break;
                    case Mouse.Pressed:
                        OnTargetMousePressed(targetGO.GetComponent<SpawnUnitItem>());
                        break;
                    case Mouse.Up:
                        OnTargetMouseUp(targetGO.GetComponent<SpawnUnitItem>());
                        break;
                }
                break;
        }
    }
#endregion


    private enum Mouse
    {
        Down,
        Pressed,
        Up
    }
}
