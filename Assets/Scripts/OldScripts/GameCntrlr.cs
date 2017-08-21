using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameCntrlr : MonoBehaviour {

    public GameObject heroBody;

    private Vector3[,] _positions = new Vector3[10, 5] { {new Vector3(-17.4f, 1f, 12.9f), new Vector3(-17.4f, 1f, 9f),
                               new Vector3(-17.4f, 1f, 5.1f), new Vector3(-17.4f, 1f, 1.5f),new Vector3(-17.4f, 1f, -2.4f)},
                                                       {new Vector3(-13.5f, 1f, 12.9f), new Vector3(-13.5f, 1f, 9f),
                               new Vector3(-13.5f, 1f, 5.1f), new Vector3(-13.5f, 1f, 1.5f),new Vector3(-13.5f, 1f, -2.4f)},
                                                       {new Vector3(-9.8f, 1f, 12.9f), new Vector3(-9.8f, 1f, 9f),
                               new Vector3(-9.8f, 1f, 5.1f), new Vector3(-9.8f, 1f, 1.5f),new Vector3(-9.8f, 1f, -2.4f)},
                                                       {new Vector3(-5.9f, 1f, 12.9f), new Vector3(-5.9f, 1f, 9f),
                               new Vector3(-5.9f, 1f, 5.1f), new Vector3(-5.9f, 1f, 1.5f),new Vector3(-5.9f, 1f, -2.4f)},
                                                       {new Vector3(-2f, 1f, 12.9f), new Vector3(-2f, 1f, 9f),
                               new Vector3(-2f, 1f, 5.1f), new Vector3(-2f, 1f, 1.5f),new Vector3(-2f, 1f, -2.4f)},
                                                       {new Vector3(2f, 1f, 12.9f), new Vector3(2f, 1f, 9f),
                               new Vector3(2f, 1f, 5.1f), new Vector3(2f, 1f, 1.5f),new Vector3(2f, 1f, -2.4f)},
                                                       {new Vector3(5.9f, 1f, 12.9f), new Vector3(5.9f, 1f, 9f),
                               new Vector3(5.9f, 1f, 5.1f), new Vector3(5.9f, 1f, 1.5f),new Vector3(5.9f, 1f, -2.4f)},
                                                       {new Vector3(9.8f, 1f, 12.9f), new Vector3(9.8f, 1f, 9f),
                               new Vector3(9.8f, 1f, 5.1f), new Vector3(9.8f, 1f, 1.5f),new Vector3(9.8f, 1f, -2.4f)},
                                                       {new Vector3(13.5f, 1f, 12.9f), new Vector3(13.5f, 1f, 9f),
                               new Vector3(13.5f, 1f, 5.1f), new Vector3(13.5f, 1f, 1.5f),new Vector3(13.5f, 1f, -2.4f)},
                                                       {new Vector3(17.4f, 1f, 12.9f), new Vector3(17.4f, 1f, 9f),
                               new Vector3(17.4f, 1f, 5.1f), new Vector3(17.4f, 1f, 1.5f),new Vector3(17.4f, 1f, -2.4f)}};

    public GameObject[,] herosOnField = new GameObject[10,5];
    public List<HeroChar> heroes = new List<HeroChar>();

    public bool isPlayerMoved = false;
    public int xMove = 0, yMove = 0;

    public bool isPlayerChose = false;
    public int newChosenX, newChosenY;
    private bool isChosen = false;
    private int chosenX, chosenY;

    private int currentHero = 0;
    private int currentX;
    private int currentY;
    private int currentMoveRange;
    

    public int curHeroDamage;
    public bool isPlayerAttacked = false;
    public bool isPlayerDamaged = false;

    void InitCurrentPosition(int curX, int curY)
    {
        currentX = curX;
        currentY = curY;
        currentMoveRange = herosOnField[curX, curY].GetComponent<player>().moveRange;     
    }

    void Start () {
        AddNewHero(0, 1, 250, 60, 80, 2, false, 0);
        AddNewHero(9, 3, 250, 60, 80, 2, true, 1);
        AddNewHero(0, 3, 250, 60, 80, 2, false, 2);
        AddNewHero(9, 1, 250, 60, 80, 2, true, 3);

        InitCurrentPosition(heroes[0].xPos, heroes[0].yPos);
        herosOnField[currentX, currentY].transform.Find("Underlight").gameObject.SetActive(true);
        ShowShadowPlanes(true);
    }
	
    void OnMouseUpAsButton()
    {
        if (isChosen)
        {
            ShowAttackpointers(false);
            isChosen = false;
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 1f;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (isPlayerMoved)
        {
            isPlayerMoved = false;

            MoveHero();
        }

        if (isPlayerChose)
        {
            PointersOnChosenHero();
            curHeroDamage = Random.Range(herosOnField[currentX, currentY].GetComponent<player>().minDamage, herosOnField[currentX, currentY].GetComponent<player>().maxDamage);
        }

        if (isPlayerAttacked)
        {
            isPlayerAttacked = false;
            
        }
        if (isPlayerDamaged)
        {
            isPlayerDamaged = false;
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 1f;
            MoveHero();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");

    }

    private void PointersOnChosenHero()
    {
        if (isChosen)
        {
            ShowAttackpointers(false);
            isChosen = false;
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 1f;
        }

        isPlayerChose = false;

        if (herosOnField[newChosenX, newChosenY].GetComponent<player>().rightSide != herosOnField[currentX, currentY].GetComponent<player>().rightSide)
        {
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.7f;
            isChosen = true;
            chosenX = newChosenX;
            chosenY = newChosenY;
            ShowAttackpointers(true);
        }
    }

    private void MoveHero()
    {
        if (isChosen)
        {
            ShowAttackpointers(false);
            isChosen = false;
            GameObject.Find("Directional light").GetComponent<Light>().intensity = 1f;
        }
        else {

            MoveHeroOnBoard();

            herosOnField[currentX, currentY].transform.Find("Underlight").gameObject.SetActive(false);

            for (bool loop = true; loop;)
            {
                if (currentHero == heroes.Count - 1)
                    currentHero = 0;
                else
                    currentHero++;
                if (heroes[currentHero] == null)
                    loop = true;
                else loop = false;
            }

            InitCurrentPosition(heroes[currentHero].xPos, heroes[currentHero].yPos);
            herosOnField[currentX, currentY].transform.Find("Underlight").gameObject.SetActive(true);

            ShowShadowPlanes(true);
        }
    }

    private void AddNewHero(int xPos, int yPos, int HealthPoints, int minDamage, int maxDamage, int moveRange, bool isRight, int index)
    {
        herosOnField[xPos, yPos] = Instantiate(heroBody, _positions[xPos, yPos], Quaternion.identity) as GameObject;
        herosOnField[xPos, yPos].GetComponent<player>().currentHealthPoints = HealthPoints;
        herosOnField[xPos, yPos].GetComponent<player>().maxHealthPoints = HealthPoints;
        herosOnField[xPos, yPos].GetComponent<player>().minDamage = minDamage;
        herosOnField[xPos, yPos].GetComponent<player>().maxDamage = maxDamage;
        herosOnField[xPos, yPos].GetComponent<player>().moveRange = moveRange;
        herosOnField[xPos, yPos].GetComponent<player>().rightSide = isRight;
        herosOnField[xPos, yPos].GetComponent<player>().X = xPos;
        herosOnField[xPos, yPos].GetComponent<player>().Y = yPos;
        herosOnField[xPos, yPos].GetComponent<player>().index = index;
        heroes.Add(new HeroChar(herosOnField[xPos, yPos], xPos, yPos));
    }

    private void MoveHeroOnBoard ()
    {
        ShowShadowPlanes(false);
        herosOnField[xMove, yMove] = herosOnField[currentX, currentY];
        if (!((currentX == xMove) && (currentY == yMove)))
            herosOnField[currentX, currentY] = null;
        herosOnField[xMove, yMove].GetComponent<player>().transform.localPosition = _positions[xMove, yMove];
        heroes[currentHero].xPos = xMove;
        heroes[currentHero].yPos = yMove;
        herosOnField[xMove, yMove].GetComponent<player>().X = xMove;
        herosOnField[xMove, yMove].GetComponent<player>().Y = yMove;
        InitCurrentPosition(xMove, yMove);
    }

    private void ShowAttackpointers(bool isShow)
    {
        for (int k = currentMoveRange; k >= 0; k--)
            for (int i = currentMoveRange, j = 0; i >= 0; i--, j++)
                if (((currentY + j - k) >= 0) && ((currentY + j - k) < 5) &&
                   ((currentX + i - k) >= 0) && ((currentX + i - k) < 10))
                    if (herosOnField[currentX + i - k, currentY + j - k] == null || ((i - k == 0) && (j - k == 0)))
                        ActivePointers(currentX + i - k, currentY + j - k, isShow);                 

        for (int k = currentMoveRange - 1; k >= 0; k--)
            for (int i = currentMoveRange, j = 0; i > 0; i--, j++)
                if ((currentY + j - k >= 0) && (currentY + j - k < 5) &&
                   ((currentX + i - k - 1) >= 0) && ((currentX + i - k - 1) < 10))
                    if ((herosOnField[currentX + i - k - 1, currentY + j - k] == null) || ((i - k - 1 == 0)&&(j - k == 0)))
                        ActivePointers(currentX + i - k - 1, currentY + j - k, isShow);
    }

    private void ActivePointers (int X, int Y, bool isShow)
    {
        if (((X) == chosenX + 1) && ((Y) == chosenY))
            herosOnField[chosenX, chosenY].transform.Find("PointerE").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX + 1) && ((Y) == chosenY + 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerSE").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX + 1) && ((Y) == chosenY - 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerNE").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX) && ((Y) == chosenY - 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerN").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX) && ((Y) == chosenY + 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerS").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX - 1) && ((Y) == chosenY))
            herosOnField[chosenX, chosenY].transform.Find("PointerW").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX - 1) && ((Y) == chosenY - 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerNW").gameObject.SetActive(isShow);
        else
        if (((X) == chosenX - 1) && ((Y) == chosenY + 1))
            herosOnField[chosenX, chosenY].transform.Find("PointerSW").gameObject.SetActive(isShow);
    }

    private void ShowShadowPlanes(bool isShow)
    {
        for (int k = currentMoveRange; k >= 0; k--)
            for (int i = currentMoveRange, j = 0; i >= 0; i--, j++)
                if (((currentY + j - k) >= 0) && ((currentY + j - k) < 5) &&
                   ((currentX + i - k) >= 0) && ((currentX + i - k) < 10))
                    if (herosOnField[currentX + i - k, currentY + j - k] == null)
                        GameObject.Find("Shadow Planes").transform.Find((currentY + j - k).ToString() + " "
                            + (currentX + i - k).ToString()).gameObject.SetActive(isShow);

         for (int k = currentMoveRange - 1; k >= 0; k--)
            for (int i = currentMoveRange, j = 0; i > 0; i--, j++)
                if ((currentY + j - k >= 0) && (currentY + j - k < 5) &&
                   ((currentX + i - k - 1) >= 0) && ((currentX + i - k - 1) < 10))
                    if (herosOnField[currentX + i - k - 1, currentY + j - k] == null)
                        GameObject.Find("Shadow Planes").transform.Find((currentY + j - k).ToString() + " "
                         + (currentX + i - k - 1).ToString()).gameObject.SetActive(isShow);
    }
}

public class HeroChar
{
    public GameObject hero;
    public int xPos;
    public int yPos;
    public HeroChar(GameObject newHero, int x, int y)
    {
        hero = newHero;
        xPos = x;
        yPos = y;
    }
}