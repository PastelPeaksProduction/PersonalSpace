using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Animation ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GOTOCredits()
    {
        ani.Play("Menu_MoveToWall");
    }

    public void GOTOLevels()
    {
        ani.Play("Menu_MoveToSky");
    }
}
