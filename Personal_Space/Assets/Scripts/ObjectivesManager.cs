using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesManager : MonoBehaviour
{

    /* Object to trigger Objective */
    public string gameStartObjectiveDescription;
    public Sprite gameStartEmoji;

    public GameObject firstObject_1;
    public string firstDescription;
    public Sprite firstEmoji;

    public GameObject secondObject_2;
    public string secondDescription;
    public Sprite secondEmoji;

    public GameObject thirdObject_3;
    public string thirdDescription;
    public Sprite thirdEmoji;

    public GameObject fourthObject_4;
    public string fourthDescription;
    public Sprite fourthEmoji;

    public GameObject fifthObject_5;
    public string fifthDescription;
    public Sprite fifthEmoji;

    public GameObject sixthObject_6;
    public string sixthDescription;
    public Sprite sixthEmoji;

    public GameObject seventhObject_7;
    public string seventhDescription;
    public Sprite seventhEmoji;

    public GameObject eightObject_8;
    public string eightDescription;
    public Sprite eightEmoji;

    public GameObject ninthObject_9;
    public string ninthDescription;
    public Sprite ninthEmoji;

    public GameObject tenthObject_10;
    public string tenthDescription;
    public Sprite tenthEmoji;

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
        if (firstObject_1 != null)
        {
            Objectives.Add(new Objective(firstObject_1, firstDescription,firstEmoji));
            firstObject_1.SetActive(true); //

        }
        if (secondObject_2 != null)
        {
            Objectives.Add(new Objective(secondObject_2, secondDescription, secondEmoji));
            secondObject_2.SetActive(false);    //
        }
        if (thirdObject_3 != null)
        {
            Objectives.Add(new Objective(thirdObject_3, thirdDescription, thirdEmoji));
            thirdObject_3.SetActive(false); //
        }
        if (fourthObject_4 != null)
        {
            Objectives.Add(new Objective(fourthObject_4, fourthDescription, fourthEmoji));
            fourthObject_4.SetActive(false);    //
        }
        if (fifthObject_5 != null)
        {
            Objectives.Add(new Objective(fifthObject_5, fifthDescription, fifthEmoji));
            fifthObject_5.SetActive(false); //
        }

        if (sixthObject_6 != null)
        {
            Objectives.Add(new Objective(sixthObject_6, sixthDescription, sixthEmoji));
            sixthObject_6.SetActive(false); //
        }


        if (seventhObject_7 != null)
        {
            Objectives.Add(new Objective(seventhObject_7, seventhDescription, seventhEmoji));
            seventhObject_7.SetActive(false); //
        }

        if (eightObject_8 != null)
        {
            Objectives.Add(new Objective(eightObject_8, eightDescription, eightEmoji));
            eightObject_8.SetActive(false); //
        }

        if (ninthObject_9 != null)
        {
            Objectives.Add(new Objective(ninthObject_9, ninthDescription, ninthEmoji));
            ninthObject_9.SetActive(false); //
        }

        if (tenthObject_10 != null)
        {
            Objectives.Add(new Objective(tenthObject_10, tenthDescription, tenthEmoji));
            tenthObject_10.SetActive(false); //
        }

        StartCoroutine(GameStartDelay(3));
        pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);

        _phoneUI.SetNotifyMessage(gameStartObjectiveDescription);
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
