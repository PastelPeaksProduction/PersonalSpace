using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseChooser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] noses;

    void Start()
    {
        int random = Random.Range(0, noses.Length);
        noses[random].SetActive(true);
        noses[random].GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    
}
