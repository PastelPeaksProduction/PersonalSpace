using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;

public class PlayerAngle : MonoBehaviour
{
    public Vector2 offset = new Vector2(0,150);
    private GameObject Player;
    private RectTransform AnglePos;
    private RectTransform CanvasPos;
    private UIWorldSpaceUti uti = new UIWorldSpaceUti();
    void Start()
    {
        CanvasPos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Player = GameObject.Find("Player");
        AnglePos = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerRo = Player.transform.eulerAngles;

        AnglePos.eulerAngles = new Vector3(0, 0, -playerRo.y);
        AnglePos.anchoredPosition = uti.GetWorldPos(CanvasPos, Player) + offset;
    }
}
