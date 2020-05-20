using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OneTimeDialogController : MonoBehaviour
{
    /* Object to trigger Objective */
    [Header("Will show in 1sec after game starts")]
    public string gameStartObjectiveDescription;

    public string android;
    public string controller;
    public string keyboard;
    


   
    public GameObject OnetimeDialog;
    public GameObject pauseDialogText;
    public float reminderTime;
    public GameObject ObjMarker;


    private GameController gameController;
    private SpecialsManager specialsManager;




    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        specialsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialsManager>();


        
       
        StartCoroutine(GameStartDelay(0));
        // pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;

    }
    IEnumerator GameStartDelay(int sec)
    {
        Debug.Log("Started");
        yield return new WaitForSeconds(sec);
        specialsManager.isEnabled = false;
        gameController.PauseGame();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<OnScreenJoystickController>().androidTesting)
        {
            pauseDialogText.GetComponent<TextMeshProUGUI>().text = android;
        }
        else
        {
            bool controllerPresent = false;
            string[] names = Input.GetJoystickNames();
            foreach (string name in names)
            {
                if (name != "")
                {
                    controllerPresent = true;
                }
            }
            if (controllerPresent)
            {
                pauseDialogText.GetComponent<TextMeshProUGUI>().text = controller;
            }
            else
            {
                pauseDialogText.GetComponent<TextMeshProUGUI>().text = keyboard;
            }
        }
        //pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        OnetimeDialog.SetActive(true);
    }

    private void Update()
    {
        CheckContinue();
    }

    private void CheckContinue()
    {
        if (Input.GetKeyUp("joystick button 16") || Input.GetKeyUp("joystick button 0"))
        {
            Continue();
        }
    }

    public void Continue()
    {

        gameController.ContinueGame();
        OnetimeDialog.SetActive(false);
        specialsManager.isEnabled = true;
    }
   
    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {

        

    }

    
}
