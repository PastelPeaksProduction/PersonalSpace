using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;

public class TextBubbleSingle : MonoBehaviour
{
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    public bool isFlip;
    public float aliveTime;
    private GameObject Canvas;
    private RectTransform CanvasTrans;
    private RectTransform TextBubbleTrans;
    private Vector2 offset = new Vector2(80, 80);
    private Vector2 flipoffset = new Vector2(-80, 80);
    private GameController Game;
    private GameObject Player;
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

        if (!Game.isGamePaused() && CanvasTrans != null)
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
    private void UpdatePosFlip()
    {
        TextBubbleTrans.anchoredPosition = CanvasUtil.GetWorldPos(CanvasTrans, Player, true) + flipoffset;
    }
}
