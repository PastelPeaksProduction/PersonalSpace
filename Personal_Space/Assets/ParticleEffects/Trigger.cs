using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem[] particles;

    //should be between 0 and length(particles) - 1
    public int test; 
    void Start()
    {
        //This just loops through all of them and stops them to start. 
        foreach( ParticleSystem p in particles)
        {
            p.Stop();
        }

        //This takes the value in the test field and turns on just that effect. 
        particles[test].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
