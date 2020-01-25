using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    private bool isMoving;
    private bool inSafe = false;
    private bool inDanger = false;
    private float backgroundVolume;
    void Start()
    {
        AkSoundEngine.PostEvent("main_loop", gameObject);
    }

    void Update()
    {
        isMoving = GetComponentInParent<PlayerController>().isMoving;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            AkSoundEngine.PostEvent("fade_to_calm", gameObject);
            inSafe = true;
            if (inDanger)
            {
            }
        }
        else if(other.gameObject.CompareTag("DangerZone"))
        {
            AkSoundEngine.PostEvent("fade_to_stress", gameObject);
            inDanger = true;
            if (!inSafe)
            {
            }
        }

        if (other.gameObject.CompareTag("Collectible") || other.gameObject.CompareTag("Objectives"))
        {
            bool triggered = other.gameObject.GetComponent<SoundTrigger>().collected;
            if (!triggered)
            {
                other.gameObject.GetComponent<SoundTrigger>().collected = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {

            AkSoundEngine.PostEvent("fade_to_main", gameObject);
            inSafe = false;
            if (inDanger)
            {
            }
        }
        else if (other.gameObject.CompareTag("DangerZone"))
        {
            AkSoundEngine.PostEvent("fade_to_main", gameObject);
            inDanger = false;
        }
        if(!inDanger && !inSafe)
        {
        }
    }

}
