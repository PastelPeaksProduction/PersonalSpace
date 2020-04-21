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
        vignette.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        vignette.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

    }

    // Update is called once per frame
    void Update()
    {
        
        float healthScale = 4*(player.GetComponent<PlayerController>().health / 100)+1.2f;
        vignette.transform.localScale = new Vector3(healthScale, healthScale, 0);
    }
}
