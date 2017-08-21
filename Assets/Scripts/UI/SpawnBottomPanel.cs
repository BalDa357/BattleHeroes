using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBottomPanel : MonoBehaviour {

    [SerializeField] private SpawnUnitItem[] spawnUnitItems;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        int i = 0;
        foreach (var unit in GameController.Instance.myUnits)
        {
            spawnUnitItems[i].Initialization(unit);
            i++;
        }



    }

}
