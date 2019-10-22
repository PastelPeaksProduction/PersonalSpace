using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float followSpeed;
    public float moveBackSpeed;
    public bool movingZone;

    private Vector3 startingPosition;
    private Transform playerPosition;

    void Start()
    {

        // Initialize variables
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startingPosition = transform.position;
    }

    void Update()
    {
        
    }


    //--------------------HELPER METHODS--------------------//

    /**
     *  Helper methods that moves the enemy to the player
     **/
    public void moveTowardsPlayer()
    {
        if (movingZone)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerPosition.position, followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, followSpeed * Time.deltaTime);
        }
    }

    /**
     *  Helper methods that moves the enemy to the thier starting location
     **/
    public void moveBackToStart()
    {
        if (!movingZone)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveBackSpeed * Time.deltaTime);
        }
    }
    
}
