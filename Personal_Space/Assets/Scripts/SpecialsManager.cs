using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsManager : MonoBehaviour
{
    public float sprintTime = 3f;
    public float sprintSpeed = 40f;
    public float cooldownTime = 0f;
    public float cooldownTimeSprint = 5f;
    public float cooldownTimeShout = 10f;
    private float normalSpeed;
    private int specialIteration = 0;
    public bool sprintAvailable = true;
    public bool shoutAvailable = true;
    private bool startingSprintAvailable = true;
    private bool startingShoutAvailable = true;
    private PlayerController1 playerCntrl;
    public float shoutForce = 10f;
    public float shoutRadius = 40f;
    public float shoutUpwardsForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerCntrl = this.gameObject.GetComponent<PlayerController1>();
        normalSpeed = playerCntrl.moveSpeed;
        startingSprintAvailable = sprintAvailable;
        startingShoutAvailable = shoutAvailable; 
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Input.GetKeyDown("space"))
        {
            // use currently assigned special
            switch(specialIteration)
            {
                case 0:
                if(sprintAvailable)
                {
                    StartCoroutine(numSprint());
                }
                break;
                case 1:
                if(shoutAvailable)
                {
                    StartCoroutine(numShout());
                }
                break;
            }
        }
        // switch special
        if(Input.GetKeyDown("l"))
        {
            specialIteration += 1;
            if(specialIteration > 1)
            {
                specialIteration = 0;
            }
        } */

        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("SPRINT ACTIVE");
            if(sprintAvailable)
            {
                StartCoroutine(numSprint());
            }
        }
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.C))
        {
            if(shoutAvailable)
            {
                StartCoroutine(numShout());
            }
        }
    }

    private IEnumerator numSprint()
    {
        //shoutAvailable = false;
        sprintAvailable = false;
        playerCntrl.moveSpeed = sprintSpeed;
        yield return new WaitForSeconds(sprintTime);
        playerCntrl.moveSpeed = normalSpeed;
        //shoutAvailable = startingShoutAvailable;
        cooldownTime = cooldownTimeSprint;
        yield return new WaitForSeconds(cooldownTime);
        sprintAvailable = startingSprintAvailable;
        cooldownTime = 0f;
    }

    private IEnumerator numShout()
    {
        //Debug.Log("Shout Initiated");
        shoutAvailable = false;
        //sprintAvailable = false;
        Vector3 playerPosition = this.gameObject.transform.position;
        //Debug.Log(playerPosition);
        Collider[] colliders = Physics.OverlapSphere(playerPosition, shoutRadius);
        foreach (Collider hit in colliders)
        {
            if(hit.gameObject.tag == "Enemy")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                
                if (rb != null)
                {
                    rb.AddExplosionForce(shoutForce, playerPosition, shoutRadius, shoutUpwardsForce, ForceMode.Force);
                    //Debug.Log(rb.transform.position);
                }
            }
            

        }
        // do shout stuff here
        cooldownTime = cooldownTimeShout;
        //sprintAvailable = startingSprintAvailable;
        yield return new WaitForSeconds(cooldownTime);
        cooldownTime = 0f;
        shoutAvailable = startingShoutAvailable;
    }

    public float GetCoolDown()
    {
        return cooldownTime;
    }
}

