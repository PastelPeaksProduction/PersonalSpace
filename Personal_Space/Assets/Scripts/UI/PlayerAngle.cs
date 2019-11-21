using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;

public class PlayerAngle : MonoBehaviour
{
    public Vector2 offset = new Vector2(0, 150);
    private GameObject Player;
    private RectTransform AnglePos;
    private RectTransform CanvasPos;
    private GameController Game;
    private UIWorldSpaceUti uti = new UIWorldSpaceUti();
    void Start()
    {

        CanvasPos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Player = GameObject.Find("Player");
        Game = Player.GetComponent<GameController>();
        AnglePos = GetComponent<RectTransform>();
    }


    private void Update()
    {

        var playerRo = Player.transform.eulerAngles;
        AnglePos.eulerAngles = new Vector3(0, 0, -playerRo.y);
        AnglePos.anchoredPosition = uti.GetWorldPos(CanvasPos, Player, false) + offset;

    }

}
