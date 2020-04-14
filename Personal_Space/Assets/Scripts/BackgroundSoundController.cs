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

    private PlayerController player;
    private bool heartbeat_slow = true;
    private int stressLevel = 0;
    void Start()
    {
        AkSoundEngine.PostEvent("main_loop", gameObject);
        //AkSoundEngine.PostEvent("Heartbeat_S", gameObject);
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!isMenu)
        {
            endLevel = GetComponentInParent<ObjectivesManager>().endLevel;
            isMoving = GetComponentInParent<PlayerController>().isMoving;
            HealthSound();
            if (isMoving || endLevel )
            {
                AkSoundEngine.PostEvent("is_moving", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("not_moving", gameObject);
            }
        }
    }
    private void HealthSound()
    {

        if (player.health <= 100 && player.health > 80)
        {
            if (stressLevel != 5)
            {
                AkSoundEngine.PostEvent("health_5", gameObject);
                stressLevel = 5;
            }
        }
        else if (player.health <= 80 && player.health > 60)
        {
            if (stressLevel != 4)
            {
                AkSoundEngine.PostEvent("health_4", gameObject);
                stressLevel = 4;
            }
        }
        else if (player.health <= 60 && player.health > 40)
        {
            if (!heartbeat_slow)
            {
                heartbeat_slow = true;
                //AkSoundEngine.PostEvent("Heartbeat_S", gameObject);
            }
            if (stressLevel != 3)
            {
                AkSoundEngine.PostEvent("health_3", gameObject);
                stressLevel = 3;
            }
        }
        else if (player.health <= 40 && player.health > 20)
        {
            if (heartbeat_slow)
            {
                heartbeat_slow = false;
                //AkSoundEngine.PostEvent("Heartbeat_F", gameObject);
            }
            if (stressLevel != 2)
            {
                AkSoundEngine.PostEvent("health_2", gameObject);
                stressLevel = 2;
            }
        }
        else if (player.health <= 20)
        {
            if (stressLevel != 1)
            {
                AkSoundEngine.PostEvent("health_1", gameObject);
                stressLevel = 1;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DangerZone"))
        {
            if (inDanger >= 0)
            {
                AkSoundEngine.PostEvent("fade_to_stress", gameObject);
            }
            inDanger += 1;
        }
        else if (other.gameObject.CompareTag("SafeZone"))
        {
            if (inSafe >= 0)
            {
                AkSoundEngine.PostEvent("fade_to_calm", gameObject);
            }
            inSafe += 1;
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
