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

    

    //private ObjectiveMarker ObjMarkerSingle;
    //private List<Objective> Objectives;
    private int objectiveCount = 0;
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
       
        _phoneUI = GameObject.Find("PopUpPhone").GetComponent<PhoneUI>();
        dataPost = GameObject.FindGameObjectWithTag("Data").GetComponent<DrivePost>();

        Objectives[0].objectiveObject.SetActive(true);
        for (int i = 1; i < Objectives.Length; i++)
        {
            if (Objectives[i].objectiveObject != null)
            {
                //Debug.Log("Turning off " + Objectives[i].objectiveObject.name);
                if (Objectives[0].objectiveObject != Objectives[i].objectiveObject)
                {
                    Objectives[i].objectiveObject.SetActive(false);
                }
                
            }
        }

        

        StartCoroutine(GameStartDelay(3));
        foreach (Message m in Objectives[0].messages)
        {
            _phoneUI.SetNotifyMessage(m);
        }
        objectiveCount++;
    }
    IEnumerator GameStartDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        //GetComponent<TextBubble>().SpawnBubble(gameStartEmoji);
    }

    private void Update()
    {
        _sinceLastObj += Time.deltaTime;
    }
   
    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {
        Debug.Log("OBJ TRIGGERED: " + objectiveCount + " LEN: " + Objectives.Length);

        if (objectiveCount <= Objectives.Length && Objectives[objectiveCount - 1].objectiveObject == obj)
        {
            particles.ObjectivePlay();
            _sinceLastObj = 0;
            // Pass the corresponding description to dialogmng

            dataPost.ObjectiveCompleted("Objective: " + Objectives[objectiveCount - 1].objectiveObject.name);


            //_phoneUI.SetNotifyMessage(Objectives[objectiveCount].ObjectiveDes);
            if (objectiveCount < Objectives.Length)
            {
                foreach (Message m in Objectives[objectiveCount].messages)
                {
                    _phoneUI.SetNotifyMessage(m);
                }
                //ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);
                Objectives[objectiveCount - 1].objectiveObject.SetActive(false);
                if (Objectives[objectiveCount].objectiveObject != null)
                {
                    Objectives[objectiveCount].objectiveObject.SetActive(true);
                }

                //
                //
                Debug.Log("OBJECT NAME: " + obj.name);
                if (Objectives[objectiveCount - 1].objectiveObject.GetComponent<ObjectiveStateChange>())
                {
                    Debug.Log("FIRED STATE CHANGE");
                    Objectives[objectiveCount - 1].objectiveObject.GetComponent<ObjectiveStateChange>().OnTriggered.Invoke();
                }
                objectiveCount++;

            }
            else if (objectiveCount == Objectives.Length)
            {
                //endLevel = true;
                Objectives[objectiveCount - 1].objectiveObject.SetActive(false);
                Debug.Log("Ending Level");
                Debug.Log("OBJECT NAME: " + obj.name);

                //Objectives[objectiveCount - 1].objectiveObject.SetActive(false);
                if (obj.GetComponent<ObjectiveStateChange>())
                {
                    obj.GetComponent<ObjectiveStateChange>().FireEvent();
                }
                //GetComponent<GameController>().AdvanceLevel();
               // this.GetComponent<BackgroundSoundController>().EndOfLevel();
                GetComponent<ArrowIndicator>().SetHideArrow();
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
        return Objectives[objectiveCount - 1].objectiveObject;
    }

    public float GetTimeSinceLastObj()
    {
        return _sinceLastObj;
    }

    private IEnumerator WaitForEnd()
    {
        
            yield return new WaitForSeconds(4);
            //this.GetComponent<BackgroundSoundController>().Silence();
            //yield return new WaitForSeconds(2);
        
            
        end_of_level_event.Post(gameObject, (uint)(AkCallbackType.AK_EndOfEvent), GetComponent<GameController>().AdvanceLevel);
    }

}
