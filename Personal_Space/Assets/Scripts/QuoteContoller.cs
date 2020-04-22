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
    private float timeElapsed;
    public float show;
    public int fade;
    private bool isShowing;
    private Color color;
    public BackgroundSoundController sound;
    public GameObject cutscene;
    private float messedupshow = 0;
    void Start()
    {
        if (quotes.Length > 0)
        {

            int rand = Random.Range(0, quotes.Length);
            quote.text = quotes[rand];
            sound.Silence();
            color = quote.color;

        }
        isShowing = true;
        startTime = 0;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed = Time.time - startTime;
        messedupshow += Time.time;
        //startTime += Time.time;
        Debug.Log("Time "+Time.time+ "  "+messedupshow + " show " + show);
        
        if (!isShowing)
        {
            Debug.Log("Here");
            float a;
            if (cutscene == null)
            {
                a = (100 - (((float)100 / fade) * (messedupshow))) / 100f;
            }
            else
            {
                a = (100 - (((float)100 / fade) * (timeElapsed))) / 100f;
            }
            Debug.Log(a);
            if (a <= 0)
            {
                if (cutscene != null)
                {
                    cutscene.SetActive(true);
                }
                gameObject.SetActive(false);
            }
            else
            {
                color.a = a;
                quote.color = color;
            }
        }
        else if ((timeElapsed >= show && isShowing) || (messedupshow >= show))
        {
            timeElapsed = 0;
            startTime = Time.time;
            messedupshow = 0;
            isShowing = false;
            Debug.Log("Here "+isShowing);
        }
    }

    
}
