using System.Collections;
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
    private GameController Game;
    private float _reminderTime;

    // Start is called before the first frame update
    void Start()
    {

        offset = new Vector3(-2, 3);
        _reminderTime = ReminderTime;
        Canvas = GameObject.Find("EnemyBubbleCanvas");
        Player = GameObject.Find("Player");
        Game = Player.GetComponent<GameController>();
        CanvasTrans = Canvas.GetComponent<RectTransform>();
        SpawnBubble();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.isGamePaused())
        {
            Reminder();
            if (TextBubbleObj != null && !Game.isGamePaused())
            {
                UpdatePos();
            }
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
        //Debug.Log("spwan enemy");
        if(TextBubbleObj == null)
        {
            TextBubbleObj = Instantiate(TextBubblePrefab);
            TextBubbleObj.transform.SetParent(Canvas.transform);
            TextBubbleTrans = TextBubbleObj.GetComponent<RectTransform>();
            TextBubbleObj.transform.GetChild(0).GetComponent<Image>().sprite = Emoji;
            TextBubbleObj.GetComponent<TextBubbleSingle>().aliveTime = ExistTime;
            TextBubbleObj.GetComponent<TextBubbleSingle>().isEnemyBubble = true;
            Ani = TextBubbleObj.GetComponent<Animation>();
        }
        else
        {
            TextBubbleObj.SetActive(true);
        }
        

        Ani.Play("ThoughtBubbleSpawn");
    }



    private void UpdatePos()
    {
        TextBubbleTrans.position = gameObject.transform.position + offset;
        TextBubbleTrans.transform.LookAt(Player.transform);
    }
}
