using UnityEngine;
using System.Collections;
using System;

public class Turns : MonoBehaviour {

    private GameObject _GameController;

    void Start()
    {
        _GameController = GameObject.Find("Plane"); 
    }

    void OnMouseDown()
    {
        transform.localPosition = new Vector3 (transform.localPosition.x, -1.35f, transform.localPosition.z);
    }

    void OnMouseUp()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -1.5f, transform.localPosition.z);
    }

    void OnMouseUpAsButton()
    {
        _GameController.GetComponent<GameCntrlr>().yMove = Convert.ToInt32(gameObject.name[0].ToString());
        _GameController.GetComponent<GameCntrlr>().xMove = Convert.ToInt32(gameObject.name[2].ToString());
        _GameController.GetComponent<GameCntrlr>().isPlayerMoved = true;
    }
}
