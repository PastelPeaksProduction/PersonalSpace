using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public AudioSource backgroundSound;
    private bool isMoving;
    void Start()
    {
    }

    void Update()
    {
        isMoving = GetComponentInParent<PlayerController>().isMoving; 
        if (isMoving)
        {
            backgroundSound.volume = 1;
        }
        else
        {
            backgroundSound.volume = 0.4f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            backgroundSound.pitch = 0.5f;
        }
        else if(other.gameObject.CompareTag("DangerZone"))
        {
            backgroundSound.pitch = 2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        backgroundSound.pitch = 1f;
    }
}
