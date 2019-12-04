using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathSoundController : MonoBehaviour
{
    public AudioSource breathSound;

    [Range(0.0f,1.0f)]
    public float BreathVolume;

    private bool isBreathing = false;
    private bool isRunning = false;
 
    void Start()
    {
        breathSound.volume = BreathVolume;
    }

    void Update()
    {

        isBreathing = GetComponentInParent<PlayerController>().isBreathing;
        if (isBreathing)
        {
            if (isRunning)
            {
                StopAllCoroutines();
                isRunning = false;
                breathSound.Stop();
            }
            breathSound.volume = BreathVolume;
            if (!breathSound.isPlaying)
            {
                breathSound.Play();
            }
        }
        else if(!isBreathing && breathSound.isPlaying)
        {

            float fadeTime = breathSound.clip.length - breathSound.time;
            IEnumerator fadeOut = fadeOutAudio(breathSound, fadeTime);
            StartCoroutine(fadeOut);
            StopCoroutine(fadeOut);
        }
    }


    private  IEnumerator fadeOutAudio(AudioSource breathSound, float fadeTime)
    {
        float startVolume = breathSound.volume;
        isRunning = true;
        while(breathSound.volume > 0)
        {
            breathSound.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        isRunning = false;
        breathSound.Stop();
    }
}
