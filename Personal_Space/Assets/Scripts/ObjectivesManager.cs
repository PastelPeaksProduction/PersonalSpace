using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Message
{
    public string Name;
    public string messageText;
    public bool isResponse;
}

[System.Serializable]
public class NewObjective
{
    public GameObject objectiveObject;
    public Sprite emoji;
    public Message[] messages;
}


public class ObjectivesManager : MonoBehaviour
{

    public NewObjective[] Objectives;

    /* Object to trigger Objective */
    

    public GameObject pauseDialogText;
    public float reminderTime;
    public GameObject ObjMarker;

    //private ObjectiveMarker ObjMarkerSingle;
    //private List<Objective> Objectives;
    private int objectiveCount = 0;
    private float _reminderTime;
    private float _sinceLastObj = 0;
    private PhoneUI _phoneUI;
    public AK.Wwise.Event end_of_level_event;
    public bool endLevel = false;

    private ParticleController particles;

    private DrivePost dataPost;
    // Start is called before the first frame update
    void Start()
    {
        particles = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<ParticleController>();
        endLevel = false;
        //ObjMarkerSingle = ObjMarker.GetComponent<ObjectiveMarker>();
        _reminderTime = reminderTime;
        _phoneUI = GameObject.Find("PopUpPhone").GetComponent<PhoneUI>();
        dataPost = GameObject.FindGameObjectWithTag("Data").GetComponent<DrivePost>();

        for (int i = 1; i < Objectives.Length; i++)
        {
            if (Objectives[i].objectiveObject != null)
            {
                Objectives[i].objectiveObject.SetActive(false);
            }
        }

       

        StartCoroutine(GameStartDelay(3));
        //pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        //ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);
        //_phoneUI.SendEmojiMessage(Objectives[0].emoji);
        foreach (Message m in Objectives[0].messages)
        {
            _phoneUI.SetNotifyMessage(m);
        }
        objectiveCount++;
    }
    IEnumerator GameStartDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
    }

    private void Update()
    {
        _sinceLastObj += Time.deltaTime;
        Reminder();
    }
    private void Reminder()
    {
        _reminderTime -= Time.deltaTime;
        if (_reminderTime < 0)
        {
            _reminderTime = reminderTime;
            if (objectiveCount == 0)
            {
            }
            else
            {
            }

        }
    }
    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {
        Debug.Log("OBJ TRIGGERED: "+objectiveCount+" LEN: "+Objectives.Length);

        if (objectiveCount <= Objectives.Length  && Objectives[objectiveCount - 1].objectiveObject == obj)
        {
            particles.ObjectivePlay();
            _sinceLastObj = 0;
            _reminderTime = reminderTime;
            // Pass the corresponding description to dialogmng
            
            dataPost.ObjectiveCompleted("Objective: " + Objectives[objectiveCount-1].objectiveObject.name);

            
            //_phoneUI.SetNotifyMessage(Objectives[objectiveCount].ObjectiveDes);
            if (objectiveCount < Objectives.Length)
            {
                //GetComponent<TextBubble>().SpawnBubble(Objectives[objectiveCount].emoji);
                //_phoneUI.SendEmojiMessage(Objectives[objectiveCount].emoji);
                //pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives[objectiveCount-1].messages[0].messageText;
                //_phoneUI.SetNotifyMessage(Objectives[objectiveCount].ObjectiveDes.Substring(0, Objectives[objectiveCount].ObjectiveDes.Length - 2));
                foreach (Message m in Objectives[objectiveCount].messages)
                {
                    _phoneUI.SetNotifyMessage(m);
                }
                //ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);
                if (Objectives[objectiveCount].objectiveObject != null)
                {
                    Objectives[objectiveCount].objectiveObject.SetActive(true);
                }
                
                //Objectives[objectiveCount-1].objectiveObject.SetActive(false);
                //
                Debug.Log("OBJECT NAME: " + obj.name);
                if (obj.GetComponent<ObjectiveStateChange>())
                {
                    obj.GetComponent<ObjectiveStateChange>().FireEvent();
                }
                objectiveCount++;

            }
            else if(objectiveCount == Objectives.Length)
            {
                endLevel = true;
                Debug.Log("Ending Level");
                Debug.Log("OBJECT NAME: " + obj.name);
                
                //Objectives[objectiveCount - 1].objectiveObject.SetActive(false);
                if (obj.GetComponent<ObjectiveStateChange>())
                {
                    obj.GetComponent<ObjectiveStateChange>().FireEvent();
                }
                end_of_level_event.Post(gameObject, (uint)(AkCallbackType.AK_EndOfEvent), GetComponent<GameController>().AdvanceLevel);
                //GetComponent<GameController>().AdvanceLevel();
                //GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>().AdvanceLevel();
                StartCoroutine(WaitForEnd());

            }

            Debug.Log("OTHER: " + obj.name);

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
        if (objectiveCount > Objectives.Length)
            return null;
        return Objectives[objectiveCount-1].objectiveObject;
    }

    public float GetTimeSinceLastObj()
    {
        return _sinceLastObj;
    }


    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Player").GetComponent<BackgroundSoundController>().EndOfLevel();

    }
}   
