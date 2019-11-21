using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove = false;
    public bool isMoving = false;
    public bool isBreathing = false;

    public float health = 100;
    public float neutralDamage = -0.01f;
    public float regenCooldown = 10.0f;

    private Rigidbody rigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Quaternion currentRotation;
    private float threatLevel;
    private bool canRegen = false;
    private float nextRegenTime = 0;

    
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        threatLevel = neutralDamage;
    }

    
    void Update()
    {
        calculateMovement();
        checkBreath();
        if (isMoving || isBreathing)
        {
            updateHealth();
        }
        if (isMoving)
        {
            nextRegenTime -= Time.deltaTime;
        }
        if (!canRegen && nextRegenTime <= 0)
        {
            canRegen = true;
        }
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
            bool aswdUsed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D);
            bool arrowsUsed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
            if (aswdUsed || arrowsUsed)
            {
                moveInput = currentRotation * new Vector3(Input.GetAxisRaw("KeyboardHorizontal"), 0f, Input.GetAxisRaw("KeyboardVertical"));
            }
            else
            {
                moveInput = currentRotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            }
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
            rigidBody.velocity = Vector3.ClampMagnitude(moveVelocity, moveSpeed);
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
     *  Checks to see if the player is breathing.
     *      -------May replace later with menus-------
     **/
    private void checkBreath()
    {
        bool twoButton = false;
        if ((Input.GetKey("joystick button 14") && Input.GetKey("joystick button 13")) || (Input.GetKey("joystick button 4") && Input.GetKey("joystick button 5")))
        {
            twoButton = true; 
        }
        float left = Input.GetAxis("Breathe Left");
        float right = Input.GetAxis("Breathe Right");
        if(left >0 && right>0)
        {
            twoButton = true; 
        }
        if (Input.GetKey(KeyCode.B) || twoButton)
        {
            isBreathing = true;
        }
        else if (!Input.GetKey(KeyCode.B) || !twoButton)
        {
            isBreathing = false; 
        }
    }

    /**
     *  Checks the players threat level and updates health
     **/
    private void updateHealth()
    {
        health += threatLevel;
        if(health >= 100)
        {
            health = 100;
        }
        if (canRegen && isBreathing)
        {
            double healthInterval = Math.Truncate(health / 20);
            health = (float)((healthInterval + 1) * 20) + 5;
            nextRegenTime = regenCooldown;
            canRegen = false;
        }
        //if(health <= 0)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {

        // Sets the threat to the level in that zone
        if(other.CompareTag("DangerZone") || other.CompareTag("SafeZone"))
        {
            threatLevel += other.GetComponent<ZoneScript>().zoneThreat;
            other.GetComponent<ZoneScript>().playerInZone = true;
        }
        if(other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Debug.Log(other.name + " collected");
            GetComponent<CollectibleManager>().ItemCollected(other.name);
        }
        if (other.gameObject.CompareTag("Objectives"))
        {
            Debug.Log(other.gameObject.name + " objective fired");
            GetComponent<ObjectivesManager>().OnObjectiveTriggered(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Objectives"))
        {
            Debug.Log(other.gameObject.name + " objective fired");
            GetComponent<ObjectivesManager>().OnObjectiveTriggered(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (!other.gameObject.CompareTag("Objectives"))
        {
            threatLevel -= other.GetComponent<ZoneScript>().zoneThreat;
            other.GetComponent<ZoneScript>().playerInZone = false;
        }
        
    }

    //--------------------PUBLIC METHODS--------------------//
    public bool getIsMoving()
    {
        return isMoving;
    }
}
