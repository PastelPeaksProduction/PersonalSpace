using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIUtil;

public class ObjectiveMarker : MonoBehaviour
{
    public Vector2 offset = new Vector2(0, -30);
    private RectTransform Marker;
    private RectTransform CanvasPos;
    private GameObject CurrentObj;
    public Animation Ani;
    private UIWorldSpaceUti uti = new UIWorldSpaceUti();
    private float animPlay = 3;
    void Start()
    {
        CanvasPos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Marker = GetComponent<RectTransform>();
    }

    private void Update()
    {

        if(CurrentObj != null)
        {
            Marker.anchoredPosition = uti.GetWorldPos(CanvasPos, CurrentObj, false) + offset;
        }
    }
    public void PlayAtObjective(GameObject obj)
    {
        CurrentObj = obj;
       // Ani.playAutomatically = true;
       
    }

    IEnumerator AnimationDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        Ani.Play();
    }
}
