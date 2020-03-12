using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMainMenu : MonoBehaviour
{
    public Button MainButton;
    public Button CreditsButton;
    public Button ControlsButton;
    public Button LevelsButton;
    public EventSystem system;


    private Vector3 prevPos;
    private CameraAnimation cam;


    public void StartGame()
    {
        SceneManager.LoadScene("Prom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScreen()
    {
        system.SetSelectedGameObject(CreditsButton.gameObject);
    }

    public void MainScreen()
    {
        system.SetSelectedGameObject(MainButton.gameObject);
    }

    public void ControlsScreen()
    {
        //ControlsButton.Select();
        system.SetSelectedGameObject(ControlsButton.gameObject);
    }

    public void LevelsScreen()
    {
        system.SetSelectedGameObject(LevelsButton.gameObject);
    }
}
