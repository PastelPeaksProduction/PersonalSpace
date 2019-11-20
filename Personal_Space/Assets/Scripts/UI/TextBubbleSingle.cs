using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubbleSingle : MonoBehaviour
{
    public float aliveTime; 
    private void TimeToDestroy()
    {
        aliveTime -= Time.deltaTime;

        if (aliveTime < 0)
        {
            DieBubble();
            aliveTime = 3;
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
}
