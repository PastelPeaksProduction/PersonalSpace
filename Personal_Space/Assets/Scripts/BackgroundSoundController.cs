using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public AudioSource backgroundSound;
    public AudioSource stressSound;
    public AudioSource calmSound;
    public AudioSource collectableSound;

    [Range(0.0f,1.0f)]
    public float backgroundNormal;
    [Range(0.0f,1.0f)]
    public float backgroundStop;
    [Range(0.0f,1.0f)]
    public float backgroundZoneMoving;
    [Range(0.0f,1.0f)]
    public float backgroundZoneStop;

    [Range(-3.0f, 3.0f)]
    public float backgroundPitchStop;
    [Range(-3.0f, 3.0f)]
    public float backgroundPitchMoving;



    [Range(0.0f,1.0f)]
    public float calmVolume;
    [Range(0.0f,1.0f)]
    public float calmStop;
    [Range(-3.0f, 3.0f)]
    public float calmPitchStop;
    [Range(-3.0f, 3.0f)]
    public float calmPitchMoving;


    [Range(0.0f,1.0f)]
    public float stressVolume;
    [Range(0.0f,1.0f)]
    public float stressStop;
    [Range(-3.0f, 3.0f)]
    public float stressPitchStop;
    [Range(-3.0f, 3.0f)]
    public float stressPitchMoving;


    [Range(10.0f,22000.0f)]
    public float lowPassStop;
    [Range(10.0f,22000.0f)]
    public float lowPassMoving;

    [Range(0.0f, 1.0f)]
    public float collectibleVolume;

    private bool isMoving;
    private bool inSafe = false;
    private bool inDanger = false;
    private float backgroundVolume;
    void Start()
    {
        backgroundVolume = backgroundNormal;
        collectableSound.volume = collectibleVolume;
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
            backgroundVolume = backgroundZoneMoving;
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
            backgroundVolume = backgroundZoneMoving;
            inDanger = true;
            if (!inSafe)
            {
                stressSound.Play();
                stressSound.volume = stressVolume;
            }
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            collectableSound.Play();
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
            backgroundSound.pitch = backgroundPitchMoving;

            stressSound.volume = stressVolume;
            stressSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassMoving;
            stressSound.pitch = stressPitchMoving;

            calmSound.volume = calmVolume;
            calmSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassMoving;
            calmSound.pitch = calmPitchMoving;

        }
        else
        {
            if (inDanger || inSafe)
            {
                backgroundSound.volume = backgroundZoneStop;
            }
            else
            {
                backgroundSound.volume = backgroundStop;
            }
            backgroundSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
            backgroundSound.pitch = backgroundPitchStop;

            stressSound.volume = stressStop;
            stressSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
            stressSound.pitch = stressPitchStop;

            calmSound.volume = calmStop;
            calmSound.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassStop;
            calmSound.pitch = calmPitchStop;

        }

    }
}
