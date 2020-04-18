using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RandomAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public int MaxSeconds = 8;
    public int MinSeconds = 3;
    public int Seconds;
    bool isAnimating = false;
    void Start()
    {
        anim = this.GetComponent<Animator>();

        StartCoroutine(PickRandom());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnimating)
        {
            Seconds = Random.Range(MinSeconds, MaxSeconds + 1);
            StartCoroutine(PickRandom());
            
        }
        
    }

    private IEnumerator PickRandom()
    {
        Debug.Log("Here");
        isAnimating = true;
        int random = Random.Range(0, 3);
        Debug.Log(this.name + " " + random);
        if(random == 0)
        {
            
            anim.SetBool("isWiggle", true);
            anim.SetBool("isSwivel", false);
            anim.SetBool("isBounce", false);
            anim.Play("Wiggle");
        }
        if (random == 1)
        {
            anim.SetBool("isWiggle", false);
            anim.SetBool("isSwivel", true);
            anim.SetBool("isBounce", false);
            anim.Play("Swivel");
            
        }
        if (random == 2)
        {
            anim.SetBool("isWiggle", false);
            anim.SetBool("isSwivel", false);
            anim.SetBool("isBounce", true);
            anim.Play("Bounce");
        }
        yield return new WaitForSeconds(Seconds);
        isAnimating = false;
    }

    
}
