using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteControl : MonoBehaviour
{


    public GameObject player;
    private float intensity;
    private Vignette vignette = null;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = this.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignette);

    }

    // Update is called once per frame
    void Update()
    {
        vignette.enabled.value = true;
        float healthFraction = player.GetComponent<PlayerController1>().health / 100;
        vignette.intensity.value = 1 - healthFraction;
    }
}
