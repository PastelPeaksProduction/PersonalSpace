using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;
using UnityEngine.UI;

public class TextBubbleSingle : MonoBehaviour
{
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    public bool isEnemyBubble;
    public bool isFlip;
    public float aliveTime;
    private GameObject Canvas;
    private RectTransform CanvasTrans;
    private RectTransform TextBubbleTrans;
    private Vector2 offset = new Vector2(80, 80);
    private Vector2 flipoffset = new Vector2(-80, 80);
    private GameController Game;
    private GameObject Player;
    private float animateTime;

    public Sprite breath1;
    public Sprite breath2;


    private void Start()
    {
        Player = GameObject.Find("Player");
        Game = GameObject.Find("Player").GetComponent<GameController>();
        Canvas = GameObject.Find("OverLayFOVCanvas");
        CanvasTrans = Canvas.GetComponent<RectTransform>();
        TextBubbleTrans = gameObject.GetComponent<RectTransform>();
    }
    private void TimeToDestroy()
    {
        aliveTime -= Time.deltaTime;

        if (aliveTime < 0)
        {
            DieBubble();
            aliveTime = 3;
        }

        if (!Game.isGamePaused() && CanvasTrans != null && !isEnemyBubble)
        {
            if (isFlip)
            {
                UpdatePosFlip();
            }
            else
            {
                UpdatePos();
            }
        }
    }

    private void Update()
    {
        if (aliveTime != 0)
        {
            TimeToDestroy();
        }
    }
    public void DieBubble()
    {
        GetComponent<Animation>().Play("ThoughtBubbleDestroy");
        Destroy(gameObject, 0.5f);
    }

    private void UpdatePos()
    {
        TextBubbleTrans.anchoredPosition = CanvasUtil.GetWorldPos(CanvasTrans, Player, true) + offset;
    }

    private void AnimateBreath()
    {
        var cur = transform.GetChild(0).GetComponent<Image>().sprite;
        if (cur == breath1)
            transform.GetChild(0).GetComponent<Image>().sprite = breath2;
        else
            transform.GetChild(0).GetComponent<Image>().sprite = breath1;

    }
    private void UpdatePosFlip()
    {
        animateTime -= Time.deltaTime;

        if (animateTime < 0)
        {
            AnimateBreath();
            animateTime = 0.2f;
        }

        TextBubbleTrans.anchoredPosition = CanvasUtil.GetWorldPos(CanvasTrans, Player, true) + flipoffset;
    }
}
