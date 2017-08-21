using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    #region SINGLETON
    private static MoveController instance;

    public static MoveController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MoveController>();

            return instance;
        }
    }
    #endregion


    /// <summary>
    /// Попробует поставить юнит и вернет плитку на которую поставит
    /// </summary>
    /// <param name="unit"></param>
    /// <returns>Свободная плитка</returns>
    public Plane1x1 GetUnitPosition(Unit unit, Plane1x1 plane)
    {
        return plane.GetEmptyPlanes(unit.size);
    }
}