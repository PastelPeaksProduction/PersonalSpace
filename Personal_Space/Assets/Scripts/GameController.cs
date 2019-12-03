using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerController player;

    public GameObject mainCamera;
    public GameObject aerialCamera;
    private AudioListener aerialListener;
    private AudioListener mainListener;

    public GameObject pauseDialog;
    public GameObject deadDialog;
    public GameObject Stressbar;
    public GameObject PlayerAngle;
    public GameObject FOV;
    public GameObject TextBubbleCanvas;
    public GameObject ObjMarker;
    public GameObject BottomHealthBar;
    private bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        aerialListener = aerialCamera.GetComponent<AudioListener>();
        mainListener = mainCamera.GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckPause();

         CheckGameStatus();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        player.canMove = false;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        player.canMove = true;
    }
    private void CheckPause()
    {
        if (!isPaused)
        {
            if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown("joystick button 1")))
            {
                Debug.Log("switch" + isPaused);
                isPaused = true;
                PauseGame();


                pauseDialog.SetActive(true);
                Stressbar.SetActive(true);
                PlayerAngle.SetActive(true);
                FOV.SetActive(true);
                TextBubbleCanvas.SetActive(false);
                mainCamera.SetActive(false);
                mainListener.enabled = false;
                aerialCamera.SetActive(true);
                aerialListener.enabled = true;
                ObjMarker.SetActive(true);
                BottomHealthBar.SetActive(false);
            }
        }
        else
        {
            if ((Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp("joystick button 17") || Input.GetKeyUp("joystick button 1")))
            {
                Debug.Log("switch2" + isPaused);

                pauseDialog.SetActive(false);
                Stressbar.SetActive(false);
                PlayerAngle.SetActive(false);
                FOV.SetActive(false);
                TextBubbleCanvas.SetActive(true);
                mainCamera.SetActive(true);
                mainListener.enabled = true;
                aerialCamera.SetActive(false);
                aerialListener.enabled = false;
                ObjMarker.SetActive(false);
                BottomHealthBar.SetActive(true);

                ContinueGame();

                isPaused = false;
            }
        }
    }

    private void CheckContinue()
    {


    }
    private void CheckGameStatus()
    {
        if (player.health <= 0)
        {
            PauseGame();
            deadDialog.SetActive(true);

            //X to Restart game
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKey("joystick button 18") || Input.GetKey("joystick button 2"))
            {
                deadDialog.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                ContinueGame();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("03House Party");

        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("02SchoolDance");

        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("WorkParty");

        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("GroceryStore");

        }
    }

    public void AdvanceLevel()
    {
        if (SceneManager.GetActiveScene().name == "01GroceryStore")
        {
            SceneManager.LoadScene("02SchoolDance");
        }

        if (SceneManager.GetActiveScene().name == "02SchoolDance")
        {
            SceneManager.LoadScene("03HouseParty");
        }

        if (SceneManager.GetActiveScene().name == "03HouseParty")
        {
            SceneManager.LoadScene("04WorkParty");
        }

        if (SceneManager.GetActiveScene().name == "04WorkParty")
        {
            SceneManager.LoadScene("00StartMenu");
        }
    }

    private void CameraSwitch(bool tab)
    {
        if (tab && !mainCamera.activeSelf)
        {

        }
        else if (tab && mainCamera.activeSelf)
        {

        }

    }

    public bool isGamePaused()
    {
        return isPaused;
    }
}
