using System;
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
    private ObjectivesManager obj_man;
    private bool show_arrow;
    private float count_down = 10f;
    private bool count_down_bool = false;

    public GameObject indicator;
    public Transform target;
    

    

    // Start is called before the first frame update
    void Start()
    {
        //Canvas = GameObject.Find("EnemyBubbleCanvas");
        //Player = GameObject.Find("Player");
        //ArrowObj = Instantiate(ArrowPrefab);
        //ArrowObj.transform.SetParent(Canvas.transform);
        //ArrowTrans = ArrowObj.GetComponent<RectTransform>();
        obj_man = gameObject.transform.GetComponentInParent<ObjectivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (show_arrow)
        {
            /*Objective = obj_man.GetCurrentObjective();
            ArrowTrans.position = gameObject.transform.position + new Vector3(0,-4.f,0);
            ArrowTrans.LookAt(Objective.transform);
            ArrowTrans.Rotate(new Vector3(90, 0, 0));*/
            if (obj_man.GetCurrentObjective() == null)
            {
                return;
            }
            target = obj_man.GetCurrentObjective().transform;
            indicator.transform.LookAt(target);
            indicator.transform.position = obj_man.gameObject.transform.position + new Vector3(0, -4.5f, 0);
        }
        //if (count_down_bool)
        //{
            //StartCountDown();
        //}
    }

    public void SetShowArrow()
    {
        show_arrow = true;
        indicator.SetActive(true);
        //count_down_bool = true;
    }

    /*private void StartCountDown()
    {
        if (count_down >= 0)
        {
            count_down -= Time.deltaTime;
        }
        else
        {
            SetHideArrow();
            count_down = 10f;
            count_down_bool = false;
        }
    }*/

    public void SetHideArrow()
    {
        show_arrow = false;
        indicator.SetActive(false);
    }

    public void ToggleArrow()
    {
        show_arrow = !show_arrow;
        indicator.SetActive(show_arrow);
    }
}

