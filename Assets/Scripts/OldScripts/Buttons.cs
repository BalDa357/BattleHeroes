using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public Sprite layer_green, layer_red;    

	void OnMouseDown()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.25f);
    }

    void OnMouseUp()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.25f);
    }

    void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "1vs1Button":
                SceneManager.LoadScene ("Play");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
