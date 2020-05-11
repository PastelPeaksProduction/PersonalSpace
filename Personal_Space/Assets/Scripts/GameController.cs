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

    private DrivePost dataPost;
    private bool isPaused;
    public bool isPhoneShow;
    public bool hasFocus;
    public bool changeFocus = false;
    public bool controllerPresent = false;
    public SpecialsManager specials;

    // Start is called before the first frame update
    void Start()
    {
        if (!nullValues)
        {
            string[] names = Input.GetJoystickNames();
            foreach (string name in names)
            {
                if (name != "")
                {
                    controllerPresent = true;
                    Cursor.visible = false;
                }
            }


            player = GameObject.Find("Player").GetComponent<PlayerController>();
            specials = player.gameObject.GetComponent<SpecialsManager>();
            aerialListener = aerialCamera.GetComponent<AudioListener>();
            mainListener = mainCamera.GetComponent<AudioListener>();
            ObjMng = gameObject.GetComponent<ObjectivesManager>();
            ArrInd = gameObject.GetComponent<ArrowIndicator>();
            PhoneUI = GameObject.Find("PopUpPhone").GetComponent<PhoneUI>();
            dataPost = GameObject.FindGameObjectWithTag("Data").GetComponent<DrivePost>();
            int width = Screen.width;
            int height = Screen.height;
            float ratio = width / 1920;
            width = (int)(width * ratio);
            height = (int)(height * ratio);
            ArrInd.SetHideArrow();
#if UNITY_EDITOR
            Debug.Log("In Editor");
#else
                Screen.SetResolution(width,height, true);
#endif
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckHint();
        CheckReminder();
        CheckPause();
        CheckGameStatus();
        CheckPhonePopUp();




        if (changeFocus)
        {
            if (!hasFocus)
            {
                PhonePauseUI.ShowMessage();
                PauseGame();
            }
            else
            {
                //PhonePauseUI.HideMessage();
               // ContinueGame();
            }
            changeFocus = false;
        }


        bool temp = false;
        string[] names = Input.GetJoystickNames();
        foreach (string name in names)
        {
            if (name != "")
            {
                temp = true;
            }
        }
        if (!temp)
        {
            if (controllerPresent)
            {
                Cursor.visible = true;
                controllerPresent = false;
                PhonePauseUI.ShowMessage();
                PauseGame();
            }
        }
        else
        {
            if (!controllerPresent)
            {
                Cursor.visible = false;
                controllerPresent = true;
                PhonePauseUI.ShowMessage();
                PauseGame();
            }
        }

    }

    private void OnApplicationFocus(bool focus)
    {
        changeFocus = true;
        hasFocus = focus;

    }

    private void CheckPhonePopUp()
    {
        // TODO: Bind controller keys
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown("joystick button 19") || Input.GetKeyDown("joystick button 3"))
        {
            if (!isPhoneShow)
            {
                //PhoneUI.ShowMessage();
                isPhoneShow = true;
            }
            else
            {
                //PhoneUI.HideMessage();
                isPhoneShow = false;
            }
        }
    }

    private void CheckHint()
    {
        // if (ObjMng.GetTimeSinceLastObj() < 1)
        // {
        //ArrInd.SetHideArrow();
        // }
        if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("joystick button 19") || Input.GetKeyDown(KeyCode.Return))
        {
            ArrInd.ToggleArrow();
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
        isPaused = true;
        //specials.isEnabled = false;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        player.canMove = true;
        isPaused = false;
        //specials.isEnabled = true;
    }
    private void CheckPause()
    {
        if (!nullValues)
        {
            if (!isPaused)
            {

                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 9") || Input.GetKeyDown("joystick button 6")))
                {
                    Debug.Log("switch" + isPaused);
                    isPaused = true;

                    PhonePauseUI.ShowMessage();
                    PauseGame();
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 9") || Input.GetKeyDown("joystick button 6")))
                {
                    Debug.Log("switch2" + isPaused);


                    PhonePauseUI.HideMessage();
                    ContinueGame();

                    isPaused = false;
                }
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("NewStartMenu");
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
                    dataPost.FailLevel();
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
            SceneManager.LoadScene("HousePartyCutScene");
        }

        if (SceneManager.GetActiveScene().name == "4.0HouseParty")
        {
            SceneManager.LoadScene("WorkPartyCutScene");
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
