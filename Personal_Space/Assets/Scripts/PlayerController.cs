using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove = false;
    public static bool isMoving = false;

    public float health = 100;
    public float neutralDamage = -0.01f;

    private Rigidbody rigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Quaternion currentRotation;
    private float threatLevel;

    
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        threatLevel = neutralDamage;
    }

    
    void Update()
    {
        calculateMovement();
        updateHealth();
        checkExit();
    }

    void FixedUpdate()
    {
        updateMovement();
    }

    //--------------------HELPER METHODS--------------------//



    /**
     *  Calculates the necessary rotation and velocities to move the player
     **/
    private void calculateMovement()
    {
        if (canMove)
        {
            currentRotation = transform.rotation;
            moveInput = currentRotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput * moveSpeed;
            if (moveVelocity == Vector3.zero)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
    }


    /**
     *  Sets the player velocity according to what was set in last update call
     **/
    private void updateMovement()
    {
        if (health > 0)
        {
            rigidBody.velocity = moveVelocity;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
    }


    /**
     *  Checks to see if the user has asked to exit the game.
     *      -------May replace later with menus-------
     **/
    private void checkExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    /**
     *  Checks the players threat level and updates health
     **/
    private void updateHealth()
    {
        health += threatLevel;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Sets the threat to the level in that zone
        if(other.tag == "DangerZone" || other.tag == "SafeZone")
        {
            threatLevel = other.GetComponent<ZoneScript>().zoneThreat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        threatLevel = neutralDamage;
    }
}
