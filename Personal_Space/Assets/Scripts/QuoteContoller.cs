using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuoteContoller : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI quote;
    public string[] quotes;
    private float startTime;
    public float show;
    public int fade;
    private bool isShowing;
    private Color color;

    void Start()
    {
        int rand = Random.Range(0, quotes.Length);
        quote.text = quotes[0];
        isShowing = true;
        color = quote.color;
        startTime = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.time;
        Debug.Log("Time "+Time.time+ "  "+startTime + " show " + show);
        if(startTime >= show && isShowing)
        {
            Debug.Log("Here");
            startTime = 0;
            isShowing = false;
        }
        if (!isShowing)
        {
            float a = (100 - (((float)100 / fade) * startTime))/100f;
            Debug.Log(a);
            if (a <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                color.a = a;
                quote.color = color;
            }
        }
    }

    
}
