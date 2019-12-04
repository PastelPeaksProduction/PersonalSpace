using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBack : MonoBehaviour
{
    private CameraAnimation cam;
    private Menu menu;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraAnimation>();
        menu = GameObject.Find("Main").GetComponent<Menu>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "LevelsBackBtn": menu.LevelsBack();break;
            case "ControlBackBtn": menu.ControlBack(); break;

        }

    }
}
