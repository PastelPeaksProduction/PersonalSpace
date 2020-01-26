using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    private bool isMoving;
    private int inSafe = 0;
    private int inDanger = 0;
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
            if (inSafe <= 0)
            {
                AkSoundEngine.PostEvent("fade_to_calm", gameObject);
            }
            inSafe += 1;
        }
        else if(other.gameObject.CompareTag("DangerZone"))
        {
            if (inDanger <= 0)
            {
                AkSoundEngine.PostEvent("fade_to_stress", gameObject);
            }
            inDanger += 1;
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
            inSafe -= 1;

            if (inSafe <= 0)
            {
                AkSoundEngine.PostEvent("fade_out_calm", gameObject);
            }
        }
        else if (other.gameObject.CompareTag("DangerZone"))
        {
            inDanger -= 1;
            if (inDanger <= 0)
            {
                AkSoundEngine.PostEvent("fade_out_stress", gameObject);
            }
        }
        if(inDanger <= 0 && inSafe <= 0)
        {
            Debug.Log("Here");
            AkSoundEngine.PostEvent("fade_to_main", gameObject);
        }
    }

}
