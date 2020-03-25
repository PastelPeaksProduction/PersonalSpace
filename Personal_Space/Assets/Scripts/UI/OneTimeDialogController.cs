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


    public GameObject firstObject_1;
    public string firstDescription;
    


    public GameObject secondObject_2;
    public string secondDescription;


    public GameObject thirdObject_3;
    public string thirdDescription;


    public GameObject fourthObject_4;
    public string fourthDescription;


    public GameObject fifthObject_5;
    public string fifthDescription;

    public GameObject OnetimeDialog;
    public GameObject pauseDialogText;
    public float reminderTime;
    public GameObject ObjMarker;

    private ObjectiveMarker ObjMarkerSingle;
    private List<ObjectiveTutorial> Objectives;
    private int objectiveCount = 0;
    private float _reminderTime;
    private GameController controller;
    private SpecialsManager specialsManager;

    public EventSystem system;
    public GameObject button;

    private int currentObjective = 0;


    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        specialsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialsManager>();
        _reminderTime = reminderTime;

        Objectives = new List<ObjectiveTutorial>();

        if (firstObject_1 != null)
        {
            Objectives.Add(new ObjectiveTutorial(firstObject_1, firstDescription));

        }
        if (secondObject_2 != null)
        {
            Objectives.Add(new ObjectiveTutorial(secondObject_2, secondDescription));
        }
        if (thirdObject_3 != null)
        {
            Objectives.Add(new ObjectiveTutorial(thirdObject_3, thirdDescription));
        }
        if (fourthObject_4 != null)
        {
            Objectives.Add(new ObjectiveTutorial(fourthObject_4, fourthDescription));
        }
        if (fifthObject_5 != null)
        {
            Objectives.Add(new ObjectiveTutorial(fifthObject_5, fifthDescription));
        }
        StartCoroutine(GameStartDelay(0));
        // pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;

    }
    IEnumerator GameStartDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        specialsManager.isEnabled = false;
        controller.PauseGame();
        pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        OnetimeDialog.SetActive(true);
        system.SetSelectedGameObject(button.gameObject);
    }

    private void Update()
    {
        Reminder();
        CheckContinue();
    }

    private void CheckContinue()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp("joystick button 16") || Input.GetKeyUp("joystick button 0")))
        {
            Continue();
        }
    }

    public void Continue()
    {
        Debug.Log("Continue!");
        controller.ContinueGame();
        OnetimeDialog.SetActive(false);
        specialsManager.isEnabled = true;
        currentObjective++;
    }
    private void Reminder()
    {
        _reminderTime -= Time.deltaTime;
        if (_reminderTime <= 0)
        {
            _reminderTime = reminderTime;
            //pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives[currentObjective].ObjectiveDes;
            //controller.PauseGame();
            //OnetimeDialog.SetActive(true);
        }
    }
    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {

        if (Objectives.Where(o => o.ObjectiveObj == obj).Any())
        {
            _reminderTime = reminderTime;

            pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives.Where(o => o.ObjectiveObj == obj).FirstOrDefault().ObjectiveDes;

            controller.PauseGame();
            OnetimeDialog.SetActive(true);

        }

    }

    private class ObjectiveTutorial
    {
        public GameObject ObjectiveObj { get; set; }
        public string ObjectiveDes { get; set; }
        public ObjectiveTutorial(GameObject objectiveObj, string objectiveDes)
        {
            ObjectiveObj = objectiveObj;
            ObjectiveDes = objectiveDes;
        }
    }
}
