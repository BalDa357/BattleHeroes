using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class. Use GameController.Instance
/// </summary>
public class GameController : MonoBehaviour {

#region SINGLETON
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();

            return _instance;
        }
    }
    #endregion

    [Header ("Игровой режим: ")]
    public GameMode gameMode;

    [HideInInspector] public Stage stage;
    [HideInInspector] public List<GameObject> units = new List<GameObject>();

    [HideInInspector] public List<Unit> myUnits = new List<Unit>();
    [HideInInspector] public List<Unit> enemyUnits = new List<Unit>();


    private void Awake()
    {
    }

    private void GameStart()
    {
        stage = Stage.Spawning;
    }

    public void BattleStart()
    {
        stage = Stage.Battle;

        TurnsController.Instance.OnGameStart();

        Buttle();
    }

    private void Buttle()
    {

    }

}

public enum Stage
{
    Spawning,
    Battle,
    GameEnd
}

public enum GameMode
{
    vsBot,
    Buttle,
    Ranked
}