using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane1x1 : MonoBehaviour {

    [Header ("ПОЗИЦИЯ ПЛИТКИ:")]
    [SerializeField] private byte xPosition;
    [SerializeField] private byte yPosition;
    [Header("------------------")]


    [Header("Соответствующие плитки 2х2")]
    [SerializeField] public GameObject plane2x2GO;
    public GameObject[] planes2x2GO;

    private bool isEmpty = true;



    public Position1x1 GetPosition()
    {
        return new Position1x1(xPosition, yPosition);
    }

    /// <summary>
    /// Проверка на пустую клетку
    /// </summary>
    /// <param name="size"></param>
    /// <returns>Вернет null если клетка занята или пустую Plane1x1 если пустая</returns>
    public Plane1x1 GetEmptyPlanes(Size size)//, MoveDirection direction = MoveDirection.NorthWest)
    {
        if (size == Size.Unit1x1)
            if (isEmpty) return this;

        if (size == Size.Unit2x2)
        {
            return GetEmpty2x2Plane();// (direction);
        }

        return null;
    }

    private Plane1x1 GetEmpty2x2Plane()// (MoveDirection direction)
    {        
        foreach (var plane2x2 in planes2x2GO)
        {
            bool isEmpty2x2 = true;
            var usingPlanes = plane2x2.GetComponent<Plane2x2>().usingPlanes;
            foreach (var plane1x1 in usingPlanes)
            {
                if (!plane1x1.isEmpty || !PlanesController.Instance.openedPlanes.Contains(plane1x1))
                    isEmpty2x2 = false;
            }
            if (isEmpty2x2) return usingPlanes[0];
        }

        return null;
    }



    public void SetPlaneActive()
    {
        gameObject.SetActive(isEmpty);
    }

    public void SetEmpty(Size size, bool isEmpty)
    {
        if (size == Size.Unit1x1)
        {
            this.isEmpty = isEmpty;
            gameObject.SetActive(false);
        }

        if (size == Size.Unit2x2)
        {
            foreach (var plane1x1 in plane2x2GO.GetComponent<Plane2x2>().usingPlanes)
            {
                plane1x1.isEmpty = isEmpty;
                plane1x1.gameObject.SetActive(false);
            }
        }

        PlanesController.Instance.UpdateGrid();
    }

}
