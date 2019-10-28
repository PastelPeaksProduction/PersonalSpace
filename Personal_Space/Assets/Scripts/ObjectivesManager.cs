using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{

    /* Object to trigger Objective */
    public GameObject firstObject;
    public string firstDescription;
    public GameObject secondObject;
    public string secondDescription;
    public GameObject thirdObject;
    public string thirdDescription;
    public GameObject fourthObject;
    public string fourthDescription;
    public GameObject fifthObject;
    public string fifthDescription;

    private Dictionary<GameObject, string> Objectives;

    // Start is called before the first frame update
    void Start()
    {
        Objectives = new Dictionary<GameObject, string>();
        if (firstObject != null)
        {
            Objectives.Add(firstObject, firstDescription);
        }
        if (secondObject != null)
        {
            Objectives.Add(secondObject, secondDescription);
        }
        if (thirdObject != null)
        {
            Objectives.Add(thirdObject, thirdDescription);
        }
        if (fourthObject != null)
        {
            Objectives.Add(fourthObject, fourthDescription);
        }
        if (fifthObject != null)
        {
            Objectives.Add(fifthObject, fifthDescription);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    // public function when player triggers objective tag
    public void onObjectiveTriggered(GameObject obj)
    {
        if (Objectives.ContainsKey(obj))
        {
            GetComponent<DialogManager>()
        }
    }

}
