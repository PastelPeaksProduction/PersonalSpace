﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBubbles : MonoBehaviour
{
    private GameObject[] dangerZones;
    public bool turnOffBubbles = false;
    // Start is called before the first frame update
    void Start()
    {
        if (turnOffBubbles)
        {
            dangerZones = GameObject.FindGameObjectsWithTag("DangerZone");
            foreach (GameObject zone in dangerZones)
            {
                zone.GetComponent<MeshRenderer>().enabled = false;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}