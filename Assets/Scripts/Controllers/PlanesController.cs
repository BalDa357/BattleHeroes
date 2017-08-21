using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesController : MonoBehaviour {

    #region SINGLETON
    private static PlanesController _instance;

    public static PlanesController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlanesController>().GetComponent<PlanesController>();

            return _instance;
        }
    }
    #endregion

    [Header ("Размер карты: ")]
    [SerializeField] private int rows = 11;
    [SerializeField] private int columns = 7;

    [Header ("All planes")]
    [SerializeField] private Plane1x1[] planes;
    //[Header ("----------------------")]


    private MapSize mapSize;

    public MapSize MapSize
    {
        get
        {
            if (mapSize == null)
                mapSize = new MapSize(rows, columns);

            return mapSize;
        }
    }

    [HideInInspector] public List<Plane1x1> openedPlanes;


    public void OpenAllPlanes()
    {
        openedPlanes = new List<Plane1x1>();

        foreach (var plane in planes)
        {
            openedPlanes.Add(plane);
        }
    }

    public void SwitchUnitPlanesToMove(Unit unit)
    {
        if (unit.isCanFly)
            SwitchFlyingUnitPlanes(unit);
        else
            SwitchNotFlyingUnitPlanes(unit);
    }

    /// <summary>
    /// Подсветка для летающего юнита
    /// </summary>
    /// <param name="unit"></param>
    private void SwitchFlyingUnitPlanes(Unit unit)
    {
        foreach (var plane in planes)
        {
            if (unit.size == Size.Unit1x1)
            {
              //  if (planeP)
            }
        }
    }

    /// <summary>
    /// Подсветка для нелетающего юнита
    /// </summary>
    /// <param name="unit"></param>
    private void SwitchNotFlyingUnitPlanes(Unit unit)
    {

    }

    /// <summary>
    /// Делает все клетки на поле неактивными
    /// </summary>
    public void ClearAllPlanes()
    {
        foreach (var plane in planes)
        {
            plane.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Показывает пустые плитки
    /// </summary>
    public void UpdateGrid()
    {
        foreach (var openedPlane in openedPlanes)
        {
            openedPlane.SetPlaneActive();
        }
    }

    /// <summary>
    /// Cтавит на первой найденой плитке, на которой возможно поставить
    /// </summary>
    /// <param name="plane1x1"></param>
    /// <returns></returns>
    public bool TrySetAtFirstFoundPlane(Plane1x1 plane1x1, Unit unit = null)
    {
        if (unit == null)
            unit = ReplaceController.Instance.currentReplacingUnit;

        var planeToSet = MoveController.Instance.GetUnitPosition(unit, plane1x1);
        if (!planeToSet)
        {
            return false;
        }


        SetUnitPosition(unit, planeToSet);
        return true;
    }

    /// <summary>
    /// Поставит юнит на указанную плитку
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="planeToSet"></param>
    private void SetUnitPosition(Unit unit, Plane1x1 planeToSet)
    {
        if (unit.size == Size.Unit1x1)
            unit.unitParentGO.transform.position = planeToSet.transform.position;
        else
            unit.unitParentGO.transform.position = planeToSet.plane2x2GO.transform.position;

        unit.currentPlane = planeToSet;
    }

    ///// <summary>
    ///// Показывает возможные плитки на которые можно поставить
    ///// </summary>
    ///// <param name="plane1x1"></param>
    //public void SwitchPlanesToChose(Plane1x1 plane1x1)
    //{
    //    var unit = ReplaceController.Instance.currentReplacingUnit;
    //    unit.currentPlane.SetEmpty(unit.size, true);
    //    Plane1x1 planeToSet = MoveController.Instance.GetUnitPosition(unit, plane1x1);

    //    foreach (var plane in planes)
    //    {
    //        plane.gameObject.SetActive(false);
    //    }

    //}
}

public class MapSize
{
    public int rows;
    public int columns;

    public MapSize(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }
}

public enum Size
{
    Unit1x1,
    Unit2x2
}

public class Position1x1
{
    public byte x;
    public byte y;

    public Position1x1(byte x, byte y)
    {
        this.x = x;
        this.y = y;
    }
}