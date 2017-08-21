using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceController : MonoBehaviour
{


    #region SINGLETON
    private static ReplaceController _instance;

    public static ReplaceController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ReplaceController>();

            return _instance;
        }
    }
    #endregion
    [SerializeField] private float pickUpDistance = 25f;

    [HideInInspector] public Unit currentReplacingUnit;

    /// <summary>
    /// Выбраный юнит (на которого нажали)
    /// </summary>
    [HideInInspector] public Unit selectedUnit;

    private Transform prevUnitPosition;
    private Transform parentUnitTransform;
    private Transform unitTransform;

    private void Awake()
    {
        currentReplacingUnit = null;
    }

    /// <summary>
    /// Поднятие юнита
    /// </summary>
    /// <param name="currentReplacingUnit"></param>
    public void TakeUnit(Unit currentReplacingUnit)
    {
        if (currentReplacingUnit.side == Side.Enemy)
            return;

        this.currentReplacingUnit = currentReplacingUnit;

        currentReplacingUnit.GetComponent<Collider>().enabled = false;

        unitTransform = currentReplacingUnit.transform;
        parentUnitTransform = currentReplacingUnit.unitParentGO.transform;
        prevUnitPosition = parentUnitTransform;

        unitTransform.position =
            new Vector3(unitTransform.position.x, unitTransform.position.y + pickUpDistance, unitTransform.position.z);

        currentReplacingUnit.currentPlane.SetEmpty(currentReplacingUnit.size, true);

        SelectUnit(currentReplacingUnit);
    }

    /// <summary>
    /// Опускание юнита
    /// </summary>
    /// <param name="setAtPrevPosition"></param>
    public void SetUnit(bool setAtPrevPosition = false)
    {
        if (currentReplacingUnit == null)
            return;

        currentReplacingUnit.GetComponent<Collider>().enabled = true;
        currentReplacingUnit.currentPlane.SetEmpty(currentReplacingUnit.size, false);

        if (setAtPrevPosition)
            parentUnitTransform = prevUnitPosition;

        unitTransform.position =
            new Vector3(unitTransform.position.x, unitTransform.position.y - pickUpDistance, unitTransform.position.z);

        currentReplacingUnit = null;
    }

    public void SelectUnit(Unit unit)
    {

        if (selectedUnit)
        {
            //selectedUnit.spawnUnitItem.StopSwitch();
            selectedUnit.SwitchUnit(false);
        }

        selectedUnit = unit;
        selectedUnit.SwitchUnit(true);
        //selectedUnit.spawnUnitItem.Switch();
    }

    private void Update()
    {
        if (currentReplacingUnit != null && GameController.Instance.stage == Stage.Spawning)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SetUnit(true);
            }
        }
    }

}
