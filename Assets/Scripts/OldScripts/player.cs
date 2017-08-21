using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public int maxHealthPoints = 200, currentHealthPoints = 200, minDamage = 60, maxDamage = 80, moveRange = 4;
    public bool rightSide = false;
    private Transform Health;
    private GameObject _GameController;
    private int prevHealthPoints;

    public int index;
    public int X;
    public int Y;

    void OnMouseUpAsButton()
    {
        _GameController.GetComponent<GameCntrlr>().newChosenX = X;
        _GameController.GetComponent<GameCntrlr>().newChosenY = Y;
        _GameController.GetComponent<GameCntrlr>().isPlayerChose = true;
    }

    public player(int hpNew, int minDamageNew, int maxDamageNew, int moveRangeNew, bool isRightSide)
    {
        maxHealthPoints = hpNew;
        currentHealthPoints = maxHealthPoints;
        minDamage = minDamageNew;
        maxDamage = maxDamageNew;
        moveRange = moveRangeNew;
        rightSide = isRightSide;
    }

    void Start()
    {
        
        prevHealthPoints = currentHealthPoints;
        _GameController = GameObject.Find("Plane");
        Health = transform.Find("HP").Find("HealthPoints");
        Health.localScale -= new Vector3(((float)(maxHealthPoints - currentHealthPoints) / maxHealthPoints), 0);
        Health.position = new Vector3 (Health.position.x + 2*(Health.localScale.x*((float)(currentHealthPoints) / maxHealthPoints - 1)), Health.position.y, Health.position.z);
        if (rightSide)
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        else
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void Update()
    {
        if (currentHealthPoints <= 0)
        {
            _GameController.GetComponent<GameCntrlr>().heroes[index] = null;
            _GameController.GetComponent<GameCntrlr>().herosOnField[X, Y] = null;
            _GameController.GetComponent<GameCntrlr>().isPlayerDamaged = true;
            Destroy(gameObject);
        }
        else
            if (currentHealthPoints < prevHealthPoints)
            {
                Health.localScale -= new Vector3(0, Health.localScale.y * (((float)(maxHealthPoints - currentHealthPoints)) / (maxHealthPoints * 1.5f)), 0);
                Health.GetComponent<MeshRenderer>().material.color = new Color(1.3f - ((float)currentHealthPoints / maxHealthPoints), (0.9f * currentHealthPoints / maxHealthPoints), 0f);
                prevHealthPoints = currentHealthPoints;
                _GameController.GetComponent<GameCntrlr>().isPlayerDamaged = true;
            }
        
    }
}
