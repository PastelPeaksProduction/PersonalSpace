using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Material[] colors = GameObject.FindGameObjectWithTag("ColorLibrary").GetComponent<RandomColorLibrary>().materials;
        int random = Random.Range(0, colors.Length);
        Material color = colors[random];
        //child.transform.localScale = new Vector3(1, 1, 1)/ (this.transform.localScale.x);

        this.GetComponent<MeshRenderer>().material = color;
    }

    
}
