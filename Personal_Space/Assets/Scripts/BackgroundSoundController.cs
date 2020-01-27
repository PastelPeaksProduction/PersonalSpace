using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    private bool isMoving;
    private int inSafe = 0;
    private int inDanger = 0;
    private bool endLevel;
    public bool isMenu = false;
    void Start()
    {
        AkSoundEngine.PostEvent("main_loop", gameObject);
    }

    void Update()
    {
        endLevel = GetComponent<ObjectivesManager>().endLevel;
        isMoving = GetComponentInParent<PlayerController>().isMoving;
        if (!isMenu)
        {
            if (isMoving || endLevel)
            {
                AkSoundEngine.PostEvent("is_moving", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("not_moving", gameObject);
            }
        }
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

        if ((other.gameObject.CompareTag("Collectible") || other.gameObject.CompareTag("Objectives")) && !endLevel)
        {
            AkSoundEngine.PostEvent("objective_event", gameObject);
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
            AkSoundEngine.PostEvent("fade_to_main", gameObject);
        }
    }

}
