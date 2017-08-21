using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnUnitItem : MonoBehaviour {

    [Header ("Ссылка на картинку в дочернем элементе: ")]
    [SerializeField] private Image childrenImageSprite;

    [HideInInspector] public Unit unit;

    private Sprite picture;


    public void Initialization(Unit unit)
    {
        GetComponent<BoxCollider>().enabled = true;
        this.unit = unit;
        this.picture = unit.picture;
        childrenImageSprite.sprite = picture;
        unit.spawnUnitItem = this;
    }

    public void Switch()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void StopSwitch()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
