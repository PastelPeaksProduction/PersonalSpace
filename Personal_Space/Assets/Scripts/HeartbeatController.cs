using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    public AudioSource heartbeat;
    private float timeRemaining;
    private bool isWaitingHeartBeat = false;
    private float health;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<PlayerController>().health; 
        timeRemaining = 0.00455f * health + 0.545f;
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponentInParent<PlayerController>().health;
        if (!isWaitingHeartBeat)
        {
            if (health > 0)
            {
                timeRemaining = 0.00455f * health + 0.545f;
                isWaitingHeartBeat = true;
                Invoke("PlayHeartBeat", timeRemaining);
            }
            else
            {
                heartbeat.Stop();
            }
        }


    }

    void PlayHeartBeat()
    {
        isWaitingHeartBeat = false;
        heartbeat.Play();
    }
}
