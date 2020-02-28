using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAccessory : MonoBehaviour
{
    public GameObject[] options;
    private int optionsLength = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(options != null)
        {
            if(options.Length > 1)
            {
                optionsLength = options.Length;
                
                int randomAccInd =  Random.Range(0,optionsLength);
                //Debug.Log(""+randomAccInd);
                Instantiate(options[randomAccInd], transform.position, transform.rotation);
            }
            else
            {
                Instantiate(options[0], transform.position, transform.rotation);
            } 
        }    
    }
}
