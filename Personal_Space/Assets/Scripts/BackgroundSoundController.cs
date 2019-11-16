using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public AudioSource backgroundSound;
    public AudioSource stressSound;
    public AudioSource calmSound;


    public float backgroundNormal;
    public float backgroundStop;
    public float backgroundZone;

    public float calmVolume;
    public float calmStop;

    public float stressVolume;
    public float stressStop;

    public float lowPassStop;
    public float lowPassMoving;

    private bool isMoving;
    private bool inSafe = false;
    private bool inDanger = false;
    private float backgroundVolume;
    void Start()
    {
        backgroundVolume = backgroundNormal;
    }

    void Update()
    {
        isMoving = GetComponentInParent<PlayerController>().isMoving;
        setVolume(isMoving);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            backgroundVolume = backgroundZone;
            inSafe = true;
            calmSound.Play();
            calmSound.volume = calmVolume;
            if (inDanger)
            {
                stressSound.Stop();
            }
        }
        else if(other.gameObject.CompareTag("DangerZone"))
        {
            backgroundVolume = backgroundZone;
            inDanger = true;
            if (!inSafe)
            {
                stressSound.Play();
                stressSound.volume = stressVolume;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            calmSound.Stop();
            inSafe = false;
            if (inDanger)
            {
                stressSound.Play();
                stressSound.volume = stressVolume;
            }
        }
        else if (other.gameObject.CompareTag("DangerZone"))
        {
            stressSound.Stop();
            inDanger = false;
        }
        if(!inDanger && !inSafe)
        {
            backgroundVolume = backgroundNormal;
        }
    }

    /// <summary>
    /// helper function that sets volume based on movement
    /// </summary>
    /// <param name="isMoving"></param>
    private void setVolume(bool isMoving)
    {
        if (isMoving)
        {
            backgroundSound.volume = backgroundVolume;
            backgroundSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassMoving;
            stressSound.volume = stressVolume;
            stressSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassMoving;
            calmSound.volume = calmVolume;
            calmSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassMoving;

        }
        else
        {
            backgroundSound.volume = backgroundStop;
            backgroundSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
            stressSound.volume = stressStop;
            stressSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
            calmSound.volume = calmStop;
            calmSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
        }

    }
}
