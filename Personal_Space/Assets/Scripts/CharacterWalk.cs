using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : MonoBehaviour
{

	public Animator anim;
	public float speed = 1.5F;
	public float rotSpeed = 100.0F;
   
	// Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float Rot = Input.GetAxis("Horizontal");
		float Trans = Input.GetAxis("Vertical");

        anim.SetFloat("Rot", Rot);
        anim.SetFloat("Trans", Trans);

        //Smooth Motion
        Rot *= Time.deltaTime;
        Trans *= Time.deltaTime;

        //Transform
        transform.Translate(0, 0, Trans * speed);
        transform.Rotate(0, Rot * rotSpeed, 0);

        if (Trans != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        else // Is Idle
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
        }
    }
}
