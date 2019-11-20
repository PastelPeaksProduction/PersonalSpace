using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
   
    public GameObject TextBubblePrefab;
    public Vector2 offset = new Vector2(85, 200);
    public float ExistingTime = 30;

    private Animation Ani;
    private GameObject Canvas;
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    private GameObject TextBubbleObj;
    private RectTransform TextBubbleTrans;
    private RectTransform CanvasTrans;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("OverLayFOVCanvas");
        CanvasTrans = Canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TextBubbleObj != null)
        {
            UpdatePos();
        }
    }

    public void SpawnBubble(Sprite objectiveEmoji)
    {
        // Initialize bubble
        TextBubbleObj = Instantiate(TextBubblePrefab);
        TextBubbleObj.transform.SetParent(Canvas.transform);
        TextBubbleTrans = TextBubbleObj.GetComponent<RectTransform>();
        TextBubbleObj.transform.GetChild(0).GetComponent<Image>().sprite = objectiveEmoji;
        TextBubbleObj.GetComponent<TextBubbleSingle>().aliveTime = ExistingTime;

        Ani = TextBubbleObj.GetComponent<Animation>();

        Ani.Play("ThoughtBubbleSpawn");
    }



    private void UpdatePos()
    {
        TextBubbleTrans.anchoredPosition = CanvasUtil.GetWorldPos(CanvasTrans, gameObject,true) + offset;
    }


}
