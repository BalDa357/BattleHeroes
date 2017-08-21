using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Все связанное с игроками обеих сторон
/// </summary>
public class PlayerController : MonoBehaviour {

    #region SINGLETON
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerController>();

            return _instance;
        }
    }
    #endregion

    #region UNITS_DATA
    //TODO: данные игроков должны хранится отдельно и/или формироватся перед боем

    [Header("Префабы всех юнитов моего игрока")]
    [SerializeField] private List<GameObject> myUnitPrefabs;

    [Header("Префабы всех юнитов противника")]
    [SerializeField]private List<GameObject> enemyUnitPrefabs;

    [Header("Количество линий спавна моего игрока")]
    [SerializeField][Range(2, 3)]
    private byte mySpawnLinesCount;

    [Header("Количество линий спавна моего игрока")]
    [SerializeField]
    [Range(2, 3)]
    private byte enemySpawnLinesCount;

    #endregion


    public List<GameObject> GetUnitsPrefabs(Side side)
    {
        return side == Side.My ? myUnitPrefabs : enemyUnitPrefabs;
    }

    public byte GetSpawnLinesCount(Side side)
    {
        return side == Side.My ? mySpawnLinesCount : enemySpawnLinesCount;
    }

    public void SetMaxSpawnLinesCount(Side side)
    {
        if (side == Side.My)
            mySpawnLinesCount = 3;
        else
            enemySpawnLinesCount = 3;
    }

}

public enum Side
{
    My,
    Enemy
}

