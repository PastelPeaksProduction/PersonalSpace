using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerController player;
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
    private PhoneUI PhoneUI;
    public PhonePauseUI PhonePauseUI;
    private float LastHitObj;

    private bool isPaused;
    public bool isPhoneShow;

    // Start is called before the first frame update
    void Start()
    {
        if (!nullValues)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            aerialListener = aerialCamera.GetComponent<AudioListener>();
            mainListener = mainCamera.GetComponent<AudioListener>();
            ObjMng = gameObject.GetComponent<ObjectivesManager>();
            ArrInd = gameObject.GetComponent<ArrowIndicator>();
            PhoneUI = GameObject.Find("PopUpPhone").GetComponent<PhoneUI>();
            int width = Screen.width;
            int height = Screen.height;
            float ratio = width / 1920;
            width = (int) (width * ratio);
            height = (int)(height * ratio);
            #if UNITY_EDITOR
                Debug.Log("In Editor");
            #else
                Screen.SetResolution(width,height, true);
            #endif
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHint();
        CheckReminder();
        CheckPause();
        CheckGameStatus();
        CheckPhonePopUp();
    }

    private void CheckPhonePopUp()
    {
        // TODO: Bind controller keys
        if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown("joystick button 19")|| Input.GetKeyDown("joystick button 3"))
        {
            if (!isPhoneShow)
            {
                PhoneUI.ShowMessage();
                isPhoneShow = true;
            }
            else
            {
                PhoneUI.HideMessage();
                isPhoneShow = false;
            }
        }
    }

    private void CheckHint()
    {
        if(ObjMng.GetTimeSinceLastObj() < 1)
        {
            ArrInd.SetHideArrow();
        }
        if(Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("joystick button 19") || Input.GetKeyDown(KeyCode.Return))
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

                if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 9") || Input.GetKeyDown("joystick button 6")))
                {
                    Debug.Log("switch" + isPaused);
                    isPaused = true;
                    

                   // pauseDialog.SetActive(true);

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
                    //RestartBtn.SetActive(true);
                    //MenuBtn.SetActive(true);
                    PhonePauseUI.ShowMessage();
                    PauseGame();
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 9") || Input.GetKeyDown("joystick button 6")))
                {
                    Debug.Log("switch2" + isPaused);

                    //pauseDialog.SetActive(false);
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
                    //RestartBtn.SetActive(false);
                    //MenuBtn.SetActive(false);
                    PhonePauseUI.HideMessage();
                    ContinueGame();

                    isPaused = false;
                }
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("0.0StartMenu");
        ContinueGame();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ContinueGame();
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
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKey("joystick button 16") || Input.GetKey("joystick button 0"))
                {
                    deadDialog.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    ContinueGame();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Prom");

        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("4.0HouseParty");

        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("5.0WorkParty");

        }
    }

    public void AdvanceLevel(object in_cookie, AkCallbackType in_type, object in_info)
    {
        // GetComponent<ObjectivesManager>().endLevel = false;
        

        if (SceneManager.GetActiveScene().name == "Prom")
        {
            SceneManager.LoadScene("4.0HouseParty");
        }

        if (SceneManager.GetActiveScene().name == "4.0HouseParty")
        {
            SceneManager.LoadScene("5.0WorkParty");
        }

        if (SceneManager.GetActiveScene().name == "5.0WorkParty")
        {
            SceneManager.LoadScene("NewStartMenu");
        }

    }

   

    public bool isGamePaused()
    {
        return isPaused;
    }
}
