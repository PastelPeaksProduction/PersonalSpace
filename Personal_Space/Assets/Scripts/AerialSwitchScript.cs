using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialSwitchScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aerialCamera;

    private AudioListener aerialListener;
    private AudioListener mainListener;

    private bool isPaused;
    private bool pauseUpdated = false;
    private bool playUpdated = true;

    // Start is called before the first frame update
    void Start()
    {
        aerialListener = aerialCamera.GetComponent<AudioListener>();
        mainListener = mainCamera.GetComponent<AudioListener>();
        isPaused = GetComponent<GameController>().isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        //Update isPaused
        isPaused = GetComponent<GameController>().isPaused;
        if (isPaused)
        {

            mainCamera.SetActive(false);
            mainListener.enabled = false;
            aerialCamera.SetActive(true);
            aerialListener.enabled = true;
            pauseUpdated = true;
            playUpdated = false;


        }
        else
        {

            mainCamera.SetActive(true);
            mainListener.enabled = true;
            aerialCamera.SetActive(false);
            aerialListener.enabled = false;
            playUpdated = true;


        }
    }
}
