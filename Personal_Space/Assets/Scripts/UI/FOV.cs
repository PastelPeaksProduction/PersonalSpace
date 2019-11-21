using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;


public class FOV : MonoBehaviour
{
    public Vector2 offset = new Vector2(0, 150);
    private GameObject Player;
    private RectTransform FOVPos;
    private RectTransform CanvasPos;
    private UIWorldSpaceUti uti = new UIWorldSpaceUti();

    // Start is called before the first frame update
    void Start()
    {
        CanvasPos = GameObject.Find("OverLayFOVCanvas").GetComponent<RectTransform>();
        Player = GameObject.Find("Player");
        FOVPos = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        FOVPos.anchoredPosition = uti.GetWorldPos(CanvasPos, Player,false) + offset;
    }
}
