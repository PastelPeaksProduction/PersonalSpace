﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;
using UnityEngine.UI;

public class EnemyTextBubble : MonoBehaviour
{

    public GameObject TextBubblePrefab;
    
    public Sprite Emoji;
    public Vector3 offset;
    public int ExistTime;
    public float ReminderTime = 3;

    private Animation Ani;
    private GameObject Canvas;
    private GameObject Player;
    private UIWorldSpaceUti CanvasUtil = new UIWorldSpaceUti();
    private GameObject TextBubbleObj;
    private RectTransform TextBubbleTrans;
    private RectTransform CanvasTrans;
    private float _reminderTime;

    // Start is called before the first frame update
    void Start()
    {
        _reminderTime = ReminderTime;
        Canvas = GameObject.Find("Canvas2");
        Player = GameObject.Find("Player");
        CanvasTrans = Canvas.GetComponent<RectTransform>();
        SpawnBubble();
    }

    // Update is called once per frame
    void Update()
    {
        Reminder();
        if (TextBubbleObj != null)
        {
            UpdatePos();
        }
    }

    private void Reminder()
    {
        _reminderTime -= Time.deltaTime;
        if (_reminderTime < 0)
        {
            _reminderTime = ReminderTime;
            SpawnBubble();
        }
    }

    public void SpawnBubble()
    {
        // Initialize bubble
        TextBubbleObj = Instantiate(TextBubblePrefab);
        TextBubbleObj.transform.SetParent(Canvas.transform);
        TextBubbleTrans = TextBubbleObj.GetComponent<RectTransform>();
        TextBubbleObj.transform.GetChild(0).GetComponent<Image>().sprite = Emoji;
        TextBubbleObj.GetComponent<TextBubbleSingle>().aliveTime = ExistTime;

        Ani = TextBubbleObj.GetComponent<Animation>();

        Ani.Play("ThoughtBubbleSpawn");
    }



    private void UpdatePos()
    {
        TextBubbleTrans.position = gameObject.transform.position + offset;
        TextBubbleTrans.transform.LookAt(Player.transform);
    }
}