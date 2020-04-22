using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SiliconDroid;

//using XInputDotNetPure;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove = false;
    public bool isMoving = false;
    public bool isBreathing = false;

    public float health = 100;
    public float neutralDamage = -0.01f;
    public float regenCooldown = 10.0f;
    public float healthPackAmount = 10.0f;

    private Rigidbody rigidBody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Quaternion currentRotation;
    private float threatLevel;
    private bool canRegen = true;
    private OnScreenJoystickController onScreen;

    private GameObject mainCamera;

    //private PlayerIndex playerIndex = 0;
    //GamePadState state;
    //GamePadState prevState;
    public float vibrationIntensity = 1f;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        threatLevel = neutralDamage;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        onScreen = this.GetComponent<OnScreenJoystickController>();
    }


    void Update()
    {
        calculateMovement();
        checkExit();
    }

    void FixedUpdate()
    {
        updateMovement();
        updateHealth();
        updateRumble();

    }

    //--------------------HELPER METHODS--------------------//



    /**
     *  Calculates the necessary rotation and velocities to move the player
     **/
    private void calculateMovement()
    {
        if (canMove)
        {
            currentRotation = mainCamera.transform.rotation;
            bool aswdUsed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D);
            bool arrowsUsed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
            if (aswdUsed || arrowsUsed)
            {
                moveInput = currentRotation * new Vector3(Input.GetAxisRaw("KeyboardHorizontal"), 0f, Input.GetAxisRaw("KeyboardVertical"));
            }
            else
            {
                if (onScreen.androidTesting)
                {
                    Vector3 input = onScreen.Movement();
                    input *= 40f;
                    moveInput = currentRotation * input;
                }
                else
                {
                    moveInput = currentRotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                }
                
            }
            if (moveInput != Vector3.zero)
            {
                Quaternion temp = Quaternion.LookRotation(moveInput * 1f * Time.deltaTime);
                Vector3 temp_euler = temp.eulerAngles;
                temp_euler.x = 0;
                temp_euler.z = 0;
                transform.localRotation = Quaternion.Euler(temp_euler);

            }
            //transform.Translate(moveInput * moveSpeed * Time.deltaTime, Space.World);
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

    /// <summary>
    /// Helper functions that restors percentage of previous health
    /// </summary>
    /// <param name="previousHealth"></param>
    public void useHealthPack()
    {
        canRegen = false;
        health += healthPackAmount;
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
            //GamePad.SetVibration(playerIndex, 0f, 0f);
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


        if (health >= 100)
        {
            health = 100;
        }
    }

    private void updateRumble()
    {
        if(threatLevel < 0)
        {
            // Turn on vibration
            //GamePad.SetVibration(playerIndex, vibrationIntensity, vibrationIntensity);
            // Vibration for andriod but I don't have a way to test it
            //Handheld.Vibrate();
        }
        else
        {
            // Turn off vibration
            //GamePad.SetVibration(playerIndex, 0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // Sets the threat to the level in that zone
        if (other.CompareTag("DangerZone") || other.CompareTag("SafeZone"))
        {
            
            threatLevel += other.GetComponent<ZoneScript>().zoneThreat;
            other.GetComponent<ZoneScript>().playerInZone = true;
        }

        if (other.CompareTag("SightZone"))
        {
            threatLevel += other.GetComponent<ZoneScript>().zoneThreat;
            Debug.Log("Enter Sight Cone");
        }

        if (other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Debug.Log(other.name + " collected");
            GetComponent<CollectibleManager>().ItemCollected(other.name);
        }
        if (other.gameObject.CompareTag("Objectives"))
        {
            Debug.Log(other.gameObject.name + " objective fired");
            if (SceneManager.GetActiveScene().name == "01GroceryStore")
            {
                GameObject.Find("OneTimeDialogController").GetComponent<OneTimeDialogController>().OnObjectiveTriggered(other.gameObject);
                GetComponent<ObjectivesManager>().OnObjectiveTriggered(other.gameObject);

            }
            else
            {
                GetComponent<ObjectivesManager>().OnObjectiveTriggered(other.gameObject);
            }
            // CHRIS CODE
            // Turn off the objective after the player hits it
            other.gameObject.SetActive(false);
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
            if (other.CompareTag("SightZone"))
            {
                Debug.Log("Exit Sight Cone");

            }
        }



    }

    //--------------------PUBLIC METHODS--------------------//
    public bool getIsMoving()
    {
        return isMoving;
    }
}
