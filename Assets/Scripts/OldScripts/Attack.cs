using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    private GameObject _GameController;

    void Start()
    {
        _GameController = GameObject.Find("Plane");
    }

    void OnMouseUpAsButton()
    {
        gameObject.GetComponentInParent<player>().currentHealthPoints -= _GameController.GetComponent<GameCntrlr>().curHeroDamage;

        if (gameObject.name == "PointerN")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y-1;
        }
        if (gameObject.name == "PointerS")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y+1;
        }
        if (gameObject.name == "PointerW")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X-1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y;
        }
        if (gameObject.name == "PointerE")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X+1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y;
        }
        if (gameObject.name == "PointerNE")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X + 1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y-1;
        }
        if (gameObject.name == "PointerSE")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X + 1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y+1;
        }
        if (gameObject.name == "PointerNW")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X-1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y - 1;
        }
        if (gameObject.name == "PointerSW")
        {
            _GameController.GetComponent<GameCntrlr>().xMove = gameObject.GetComponentInParent<player>().X-1;
            _GameController.GetComponent<GameCntrlr>().yMove = gameObject.GetComponentInParent<player>().Y + 1;
        }

        _GameController.GetComponent<GameCntrlr>().isPlayerMoved = true;
        _GameController.GetComponent<GameCntrlr>().isPlayerAttacked = true;
    }
}
