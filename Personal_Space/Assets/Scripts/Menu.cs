using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SchoolDance");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
}
