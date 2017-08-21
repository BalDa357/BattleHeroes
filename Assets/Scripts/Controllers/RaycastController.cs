using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

    #region SINGLETON
    private static RaycastController _instance;

    public static RaycastController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<RaycastController>();

            return _instance;
        }
    }
    #endregion

    private int rayDistance = 1000;

    // Use this for initialization
    void Start () {
		
	}



    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
            CheckMouseDown(Mouse.Down);

        if (Input.GetMouseButton(0))
            CheckMouseDown(Mouse.Pressed);

        if (Input.GetMouseButtonUp(0))
            CheckMouseDown(Mouse.Up);

    }

    private void CheckMouseDown(Mouse mouse)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            switch (mouse)
            {
                case Mouse.Down:
                    ClickController.Instance.OnRaycastMouseDown(hit.transform.gameObject);
                    break;
                case Mouse.Pressed:
                    ClickController.Instance.OnRaycastMousePressed(hit.transform.gameObject);
                    break;
                case Mouse.Up:
                    ClickController.Instance.OnRaycastMouseUp(hit.transform.gameObject);
                    break;
            }
        }
    }

    private enum Mouse
    {
        Down,
        Pressed,
        Up
    }
}
