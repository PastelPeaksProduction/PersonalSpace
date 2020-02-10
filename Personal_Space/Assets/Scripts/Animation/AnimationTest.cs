using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator anim;
    public PlayerController1 controller;
    bool walking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isMoving != walking)
        {
            walking = controller.isMoving;
            anim.SetBool("isWalking", walking);
        }
        
    }
}
