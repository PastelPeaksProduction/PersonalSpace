using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{

    /* Object to trigger Objective */
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

    public GameObject pauseDialogText;


    private List<KeyValuePair<GameObject, string>> Objectives;
    private int objectiveCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Objectives = new List<KeyValuePair<GameObject, string>>();
        if (firstObject_1 != null)
        {
            Objectives.Add(new KeyValuePair<GameObject, string>(firstObject_1, firstDescription));
        }
        if (secondObject_2 != null)
        {
            Objectives.Add(new KeyValuePair<GameObject, string>(secondObject_2, secondDescription));
        }
        if (thirdObject_3 != null)
        {
            Objectives.Add(new KeyValuePair<GameObject, string>(thirdObject_3, thirdDescription));
        }
        if (fourthObject_4 != null)
        {
            Objectives.Add(new KeyValuePair<GameObject, string>(fourthObject_4, fourthDescription));
        }
        if (fifthObject_5 != null)
        {
            Objectives.Add(new KeyValuePair<GameObject, string>(fifthObject_5, fifthDescription));
        }

        GetComponent<DialogManager>().showDialog(gameStartObjectiveDescription);
        pauseDialogText.GetComponent<TextMeshProUGUI>().text = gameStartObjectiveDescription;

    }

    // Update is called once per frame
    void Update()
    {

    }


    // public function when player triggers objective tag
    public void OnObjectiveTriggered(GameObject obj)
    {
        Debug.Log("Triggered");
        if (objectiveCount < Objectives.Count &&  Objectives[objectiveCount].Key == obj)
        {
            // Pass the corresponding description to dialogmng
            GetComponent<DialogManager>().showDialog(Objectives[objectiveCount].Value);
            pauseDialogText.GetComponent<TextMeshProUGUI>().text = Objectives[objectiveCount].Value;

            objectiveCount++;
        }
        
    }

}
