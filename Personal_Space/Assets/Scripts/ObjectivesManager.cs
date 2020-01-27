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

    public GameObject pauseDialogText;
    public float reminderTime;
    public GameObject ObjMarker;

    private ObjectiveMarker ObjMarkerSingle;
    private List<Objective> Objectives;
    private int objectiveCount = 0;
    private float _reminderTime;

    public AK.Wwise.Event end_of_level_event;
    public bool endLevel = false;
    // Start is called before the first frame update
    void Start()
    {
        endLevel = false;
        ObjMarkerSingle = ObjMarker.GetComponent<ObjectiveMarker>();
        _reminderTime = reminderTime;

        Objectives = new List<Objective>();

        if (firstObject_1 != null)
        {
            Objectives.Add(new Objective(firstObject_1, firstDescription,firstEmoji));

        }
        if (secondObject_2 != null)
        {
            Objectives.Add(new Objective(secondObject_2, secondDescription, secondEmoji));
        }
        if (thirdObject_3 != null)
        {
            Objectives.Add(new Objective(thirdObject_3, thirdDescription, thirdEmoji));
        }
        if (fourthObject_4 != null)
        {
            Objectives.Add(new Objective(fourthObject_4, fourthDescription, fourthEmoji));
        }
        if (fifthObject_5 != null)
        {
            Objectives.Add(new Objective(fifthObject_5, fifthDescription, fifthEmoji));
        }
        StartCoroutine(GameStartDelay(3));
        pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;
        ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);


    }
    IEnumerator GameStartDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        GetComponent<TextBubble>().SpawnBubble(gameStartEmoji);
    }

    private void Update()
    {
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
        Debug.Log("TRIGGERED");

        if (objectiveCount < Objectives.Count &&  Objectives[objectiveCount].ObjectiveObj == obj)
        {

            _reminderTime = reminderTime;
            // Pass the corresponding description to dialogmng
            GetComponent<TextBubble>().SpawnBubble(Objectives[objectiveCount].ObjectiveEmoji);

            pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives[objectiveCount].ObjectiveDes;

            if (++objectiveCount < Objectives.Count)
            {
                ObjMarkerSingle.PlayAtObjective(Objectives[objectiveCount].ObjectiveObj);       
            }
            else
            {
                endLevel = true;
                end_of_level_event.Post(gameObject,(uint)AkCallbackType.AK_EndOfEvent,GetComponent<GameController>().AdvanceLevel);
                //GetComponent<GameController>().AdvanceLevel();
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

}
