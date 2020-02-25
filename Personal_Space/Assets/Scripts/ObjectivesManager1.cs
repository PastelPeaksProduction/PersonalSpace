using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjectiveObj
{
    public GameObject obj;
    public string description;
    public Sprite emoji;
}
public class ObjectivesManager1 : MonoBehaviour
{

    /* Object to trigger Objective */
    public string gameStartObjectiveDescription;
    public Sprite gameStartEmoji;

    public ObjectiveObj[] objectivesList;

    public GameObject pauseDialogText;
    public float reminderTime;
    public GameObject ObjMarker;

    private ObjectiveMarker ObjMarkerSingle;
    private List<Objective> Objectives;
    private int objectiveCount = 0;
    private float _reminderTime;
    private float _sinceLastObj = 0;
    private PhoneUI _phoneUI;
    public AK.Wwise.Event end_of_level_event;
    public bool endLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        endLevel = false;
        ObjMarkerSingle = ObjMarker.GetComponent<ObjectiveMarker>();
        _reminderTime = reminderTime;
        _phoneUI = GameObject.Find("PopUpPhone").GetComponent<PhoneUI>();

        Objectives = new List<Objective>();
        //  CHRIS CODE
        //  Set each objective as false when game initializes, and set each active as the previous one is triggered
        //  Added to intantiating of each objective as well as one line down in TRIGGERED
       

        foreach(ObjectiveObj obj in objectivesList)
        {
            Objectives.Add(new Objective(obj.obj, obj.description, obj.emoji));
            obj.obj.SetActive(true); //
        }

        StartCoroutine(GameStartDelay(3));
        pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);

        _phoneUI.SetNotifyMessage(gameStartObjectiveDescription.Substring(1, gameStartObjectiveDescription.Length-2));
    }
    IEnumerator GameStartDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        GetComponent<TextBubble>().SpawnBubble(gameStartEmoji);
    }

    private void Update()
    {
        _sinceLastObj += Time.deltaTime;
        Reminder();
    }
    private void Reminder()
    {
        _reminderTime -= Time.deltaTime;
        if(_reminderTime < 0)
        {
            _reminderTime = reminderTime;
            if(objectiveCount == 0)
            {
                GetComponent<TextBubble>().SpawnBubble(gameStartEmoji);
            }
            else
            {
                GetComponent<TextBubble>().SpawnBubble(Objectives[objectiveCount - 1].ObjectiveEmoji);               
            }

        }
    }
    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {
        Debug.Log("OBJ TRIGGERED");

        if (objectiveCount < Objectives.Count &&  Objectives[objectiveCount].ObjectiveObj == obj)
        {
            _sinceLastObj = 0;
            _reminderTime = reminderTime;
            // Pass the corresponding description to dialogmng
            GetComponent<TextBubble>().SpawnBubble(Objectives[objectiveCount].ObjectiveEmoji);

            pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives[objectiveCount].ObjectiveDes;
            //_phoneUI.SetNotifyMessage(Objectives[objectiveCount].ObjectiveDes.Substring(0, Objectives[objectiveCount].ObjectiveDes.Length - 2));
            _phoneUI.SetNotifyMessage(Objectives[objectiveCount].ObjectiveDes);
            if (++objectiveCount < Objectives.Count)
            {
                ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);
                Objectives[objectiveCount].ObjectiveObj.SetActive(true);    //
                Debug.Log("OBJECT NAME: " + obj.name);
                if (obj.GetComponent<ObjectiveStateChange>())
                {
                    obj.GetComponent<ObjectiveStateChange>().FireEvent();
                }
            }
            else
            {
                endLevel = true;
                end_of_level_event.Post(gameObject,(uint)AkCallbackType.AK_EndOfEvent,GetComponent<GameController>().AdvanceLevel);
                //GetComponent<GameController>().AdvanceLevel();
                Debug.Log("OBJECT NAME: " + obj.name);
                if (obj.GetComponent<ObjectiveStateChange>())
                {
                    obj.GetComponent<ObjectiveStateChange>().FireEvent();
                }
            }
           
        }
        
    }

    private class Objective
    {
        public GameObject ObjectiveObj { get; set; }
        public string ObjectiveDes { get; set; }
        public Sprite ObjectiveEmoji { get; set; }
        public Objective(GameObject objectiveObj, string objectiveDes, Sprite objectiveEmoji)
        {
            ObjectiveObj = objectiveObj;
            ObjectiveDes = objectiveDes;
            ObjectiveEmoji = objectiveEmoji;
        }
    }

    public GameObject GetCurrentObjective()
    {
        if (objectiveCount >= Objectives.Count)
            return null;
        return Objectives[objectiveCount].ObjectiveObj;
    }

    public float GetTimeSinceLastObj()
    {
        return _sinceLastObj;
    }

}
