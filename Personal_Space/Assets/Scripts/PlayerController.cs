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
        checkBreath();
        if (isMoving || isBreathing)
        {
            updateHealth();
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
        if (Input.GetKey(KeyCode.B))
        {
            isBreathing = true;
        }
        else if (!Input.GetKey(KeyCode.B))
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
            threatLevel = other.GetComponent<ZoneScript>().zoneThreat;
            other.GetComponent<ZoneScript>().playerInZone = true;
        }
        if(other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Debug.Log(other.name + " collected");
            GameObject.Find("Main Camera").GetComponent<CollectibleManager>().ItemCollected(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        threatLevel = neutralDamage;
        other.GetComponent<ZoneScript>().playerInZone = false;
    }

    //--------------------PUBLIC METHODS--------------------//
    public bool getIsMoving()
    {
        return isMoving;
    }
}
