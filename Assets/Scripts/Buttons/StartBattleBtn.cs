using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleBtn : MonoBehaviour {

    private void OnMouseUpAsButton()
    {
        GameController.Instance.BattleStart();
    }
}
