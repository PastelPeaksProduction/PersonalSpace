using System.Collections;
using System.Collections.Generic;
using UIUtil;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public GameObject ArrowPrefab;
    private GameObject ArrowObj;
    private RectTransform ArrowTrans;
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    private GameObject Canvas;
    private GameObject Player;
    public GameObject Objective;
    public ObjectivesManager obj_man;


    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("EnemyBubbleCanvas");
        Player = GameObject.Find("Player");
        ArrowObj = Instantiate(ArrowPrefab);
        ArrowObj.transform.SetParent(Canvas.transform);
        ArrowTrans = ArrowObj.GetComponent<RectTransform>();
        obj_man = gameObject.transform.GetComponentInParent<ObjectivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Objective = obj_man.GetCurrentObjective();
        ArrowTrans.position = gameObject.transform.position + new Vector3(0,-4.5f);
        ArrowTrans.LookAt(Objective.transform);
        ArrowTrans.Rotate(new Vector3(90, 0, 0));
    }
}
