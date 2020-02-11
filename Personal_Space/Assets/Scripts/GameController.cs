using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerController1 player;
    public float TimeToReminder;
    public bool nullValues = false;
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
    public GameObject WarningMsg;
    private ObjectivesManager ObjMng;
    private ArrowIndicator ArrInd;
    private float LastHitObj;
    private bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        if (!nullValues)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController1>();
            aerialListener = aerialCamera.GetComponent<AudioListener>();
            mainListener = mainCamera.GetComponent<AudioListener>();
            ObjMng = gameObject.GetComponent<ObjectivesManager>();
            ArrInd = gameObject.GetComponent<ArrowIndicator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHint();
        CheckReminder();
        CheckPause();
        CheckGameStatus();
    }

    private void CheckHint()
    {
        if(ObjMng.GetTimeSinceLastObj() < 1)
        {
            ArrInd.SetHideArrow();
        }
        if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Return))
        {
            ArrInd.SetShowArrow();
        }
    }

    private void CheckReminder()
    {
        WarningMsg.SetActive(ObjMng.GetTimeSinceLastObj() > TimeToReminder);
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
        if (!nullValues)
        {
            if (!isPaused)
            {

                if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown("joystick button 3")))
                {
                    Debug.Log("switch" + isPaused);
                    isPaused = true;
                    PauseGame();

                    pauseDialog.SetActive(true);
                    mainCamera.GetComponent<CameraBlur>().SetPause();
                    mainCamera.GetComponent<CameraScript>().SetPause();

                    //Stressbar.SetActive(true);
                    //PlayerAngle.SetActive(true);
                    //FOV.SetActive(true);
                    //TextBubbleCanvas.SetActive(false);
                    //mainCamera.SetActive(false);
                    //aerialCamera.SetActive(true);
                    //aerialListener.enabled = true;
                    //ObjMarker.SetActive(true);
                    //BottomHealthBar.SetActive(false);
                    //Enemies.SetActive(false);
                    RestartBtn.SetActive(true);
                    MenuBtn.SetActive(true);

                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown("joystick button 3")))
                {
                    Debug.Log("switch2" + isPaused);

                    pauseDialog.SetActive(false);
                    mainCamera.GetComponent<CameraBlur>().SetPause();
                    mainCamera.GetComponent<CameraScript>().SetPause();
                    //Stressbar.SetActive(false);
                    //PlayerAngle.SetActive(false);
                    //FOV.SetActive(false);
                    //TextBubbleCanvas.SetActive(true);
                    //mainCamera.SetActive(true);
                    //aerialCamera.SetActive(false);
                    //aerialListener.enabled = false;
                    //ObjMarker.SetActive(false);
                    //BottomHealthBar.SetActive(true);
                    //Enemies.SetActive(true);
                    RestartBtn.SetActive(false);
                    MenuBtn.SetActive(false);
                    ContinueGame();

                    isPaused = false;
                }
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("0.0StartMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void CheckGameStatus()
    {
        if (!nullValues)
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
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("3.0SchoolDance");

        }
        
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("4.0HouseParty");

        }
       
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("5.0WorkParty");

        }

        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("6.0Convention");

        }

        if (Input.GetKeyUp(KeyCode.Space) && nullValues)
        {
           

            

            if (SceneManager.GetActiveScene().name == "2.5Cutscene")
            {
                SceneManager.LoadScene("3.0SchoolDance");
            }

            if (SceneManager.GetActiveScene().name == "3.5Cutscene")
            {
                SceneManager.LoadScene("4.0HouseParty");
            }

            if (SceneManager.GetActiveScene().name == "4.5Cutscene")
            {
                SceneManager.LoadScene("5.0WorkParty");
            }

            if (SceneManager.GetActiveScene().name == "5.5Cutscene")
            {
                SceneManager.LoadScene("6.0Convention");
            }

            if (SceneManager.GetActiveScene().name == "6.5Cutscene")
            {
                SceneManager.LoadScene("0.0StartMenu");
            }
        }
    }

    public void AdvanceLevel(object in_cookie, AkCallbackType in_type, object in_info)
    {
        // GetComponent<ObjectivesManager>().endLevel = false;
        

        if (SceneManager.GetActiveScene().name == "3.0SchoolDance")
        {
            SceneManager.LoadScene("3.5Cutscene");
        }

        if (SceneManager.GetActiveScene().name == "4.0HouseParty")
        {
            SceneManager.LoadScene("4.5Cutscene");
        }

        if (SceneManager.GetActiveScene().name == "5.0WorkParty")
        {
            SceneManager.LoadScene("5.5Cutscene");
        }

        if (SceneManager.GetActiveScene().name == "6.0Convention")
        {
            SceneManager.LoadScene("6.5Cutscene");
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
