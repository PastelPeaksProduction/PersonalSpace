using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteControl : MonoBehaviour
{


    public GameObject player;
    private Image vignette;

    // Start is called before the first frame update
    void Start()
    {
        vignette = this.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
        float healthScale = 3*(player.GetComponent<PlayerController>().health / 100)+1;
        vignette.transform.localScale = new Vector3(healthScale, healthScale, 0);
    }
}
