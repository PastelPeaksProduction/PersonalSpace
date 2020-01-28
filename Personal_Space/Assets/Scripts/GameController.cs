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
    public GameObject Enemies;
    public GameObject RestartBtn;
    public GameObject MenuBtn;


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
                Enemies.SetActive(false);
                RestartBtn.SetActive(true);
                MenuBtn.SetActive(true);

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
                Enemies.SetActive(true);
                RestartBtn.SetActive(false);
                MenuBtn.SetActive(false);
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
            SceneManager.LoadScene("03SchoolDance");

        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("02Playground");

        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("04HouseParty");

        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            
            SceneManager.LoadScene("01GroceryStore");

        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("05WorkParty");

        }

        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("06Convention");

        }
    }

    public void AdvanceLevel(object in_cookie, AkCallbackType in_type, object in_info)
    {
        // GetComponent<ObjectivesManager>().endLevel = false;
        if (SceneManager.GetActiveScene().name == "01GroceryStore")
        {
            SceneManager.LoadScene("02Playground");
        }

        if (SceneManager.GetActiveScene().name == "02Playground")
        {
            SceneManager.LoadScene("03SchoolDance");
        }

        if (SceneManager.GetActiveScene().name == "03SchoolDance")
        {
            SceneManager.LoadScene("04HouseParty");
        }

        if (SceneManager.GetActiveScene().name == "04HouseParty")
        {
            SceneManager.LoadScene("05WorkParty");
        }

        if (SceneManager.GetActiveScene().name == "05WorkParty")
        {
            SceneManager.LoadScene("06Convention");
        }

        if (SceneManager.GetActiveScene().name == "06Convention")
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
