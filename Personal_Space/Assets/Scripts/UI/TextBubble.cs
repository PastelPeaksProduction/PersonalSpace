using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
   
    public GameObject TextBubblePrefab;
    public GameObject TextBubbleFlipPrefab;
    public Vector2 offset = new Vector2(85, 200);
    public Vector2 flipoffset = new Vector2(85, 200);
    public float ExistingTime = 30;

    private Animation Ani;
    private GameObject Canvas;
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    private GameObject TextBubbleObj;
    private RectTransform TextBubbleTrans;
    private RectTransform CanvasTrans;
    private GameController Game;

    // Start is called before the first frame update
    void Start()
    {
        Game = GetComponent<GameController>();
        Canvas = GameObject.Find("OverLayFOVCanvas");
        CanvasTrans = Canvas.GetComponent<RectTransform>();
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

    public void SpawnFlipBubble(Sprite objectiveEmoji, Sprite objectiveEmoji2)
    {
        // Initialize bubble
        TextBubbleObj = Instantiate(TextBubbleFlipPrefab);
        TextBubbleObj.transform.SetParent(Canvas.transform);
        TextBubbleTrans = TextBubbleObj.GetComponent<RectTransform>();
        TextBubbleObj.transform.GetChild(0).GetComponent<Image>().sprite = objectiveEmoji;
        TextBubbleObj.GetComponent<TextBubbleSingle>().aliveTime = ExistingTime;
        TextBubbleObj.GetComponent<TextBubbleSingle>().isFlip = true;
        TextBubbleObj.GetComponent<TextBubbleSingle>().breath1 = objectiveEmoji;
        TextBubbleObj.GetComponent<TextBubbleSingle>().breath2 = objectiveEmoji2;
        Ani = TextBubbleObj.GetComponent<Animation>();
        
        Ani.Play("ThoughtBubbleSpawn");
    }
}
