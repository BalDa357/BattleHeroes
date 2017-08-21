using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [Header("Уникальный ID юнита: ")]
    [SerializeField] private int id;

    [Header("ХАРАКТЕРИСТИКИ ЮНИТА: ")]
    [Header("Инициатива: ")]
    public float initiative = 10;
    [Header("Дальний бой: ")]
    public bool isShooter = false;
    [Header("Дальность передвижения: ")]
    public int movingDistance = 3;
    [Header("Летающий: ")]
    public bool isCanFly = false;
    [Header("Max health points: ")]
    [SerializeField] private int maxHP;

    [Header("_______________________________")]
    [Header("ВНЕШНИЙ ВИД: ")]
    [Header("Скорость передвижения по карте: ")]
    public float movingSpeed = 1;
    [Header("Размер : ")]
    public Size size = Size.Unit1x1;
    [Header("Картинка : ")]
    public Sprite picture;
    [Header("Плитка тени: ")]
    [SerializeField] private GameObject shadowPlane;
    [Header("Партикл подсветки юнита: ")]
    [SerializeField] private ParticleSystem switchParticle;

    [Header("_______________________________")]
    [Header("Ссылка на своего родителя: ")]
    public GameObject unitParentGO;

    [HideInInspector] public Side side;
    [HideInInspector] public int unitsCount = 1;
    [HideInInspector] public int currentHP;
    [HideInInspector] public Plane1x1 currentPlane;
    [HideInInspector] public SpawnUnitItem spawnUnitItem;

    public int ID
    {
        get { return id; }
    }

    private void Awake()
    {
        
    }

    public void Initialization(Plane1x1 currentPlane, Side side)
    {
        this.side = side;
        this.currentPlane = currentPlane;
        GameController.Instance.units.Add(unitParentGO);
        if (side == Side.My)
            GameController.Instance.myUnits.Add(this);
        if (side == Side.Enemy)
        {
            GameController.Instance.enemyUnits.Add(this);
            shadowPlane.SetActive(false);
        }
    }

    public void SwitchUnit(bool isSwitch)
    {
        switchParticle.gameObject.SetActive(isSwitch);
        var emission = switchParticle.emission;
        emission.enabled = isSwitch;
    }

    private void FixedUpdate()
    {

    }


}
