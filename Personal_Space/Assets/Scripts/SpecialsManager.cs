using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsManager : MonoBehaviour
{
    public float sprintTime = 3f;
    public float sprintSpeed = 40f;

    public float cooldownTime = 5f;
    private float normalSpeed;
    private int specialIteration = 0;
    private bool shoutOn = false;
    private bool sprintAvailable = true;
    private bool shoutAvailable = true;
    private bool sprintOn = false;
    private PlayerController playerCntrl;

    public float shoutForce = 10f;
    public float shoutRadius = 40f;
    public float shoutUpwardsForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerCntrl = this.gameObject.GetComponent<PlayerController>();
        normalSpeed = playerCntrl.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
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
        }
    }

    private IEnumerator numSprint()
    {
        shoutAvailable = false;
        sprintAvailable = false;
        playerCntrl.moveSpeed = sprintSpeed;
        yield return new WaitForSeconds(sprintTime);
        playerCntrl.moveSpeed = normalSpeed;
        yield return new WaitForSeconds(cooldownTime);
        sprintAvailable = true;
        shoutAvailable = true;
    }

    private IEnumerator numShout()
    {
        //Debug.Log("Shout Initiated");
        shoutAvailable = false;
        sprintAvailable = false;
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
        yield return new WaitForSeconds(cooldownTime);
        sprintAvailable = true;
        shoutAvailable = true;
    }
}

