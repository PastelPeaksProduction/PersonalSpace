using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSript : MonoBehaviour
{

    private double popTime = 0.3f;
    private double zoomTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        PopEffect();
    }

    private void PopEffect()
    {
        popTime -= Time.deltaTime;
        if(popTime < 0)
        {
            zoomTime -= Time.deltaTime;
            transform.localScale -= new Vector3(0.002f, 0.002f, 0.002f);
            if(zoomTime < 0)
            {
                popTime = 1f;
                zoomTime = 1f;
            }
        }
        else
        {
            transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);
        }
    }
}
