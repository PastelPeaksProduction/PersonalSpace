using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemyController : MonoBehaviour
{
    public float followSpeed;
    public float moveBackSpeed;
    
    private GameObject player;
    private PlayerController playerController;
    private Vector3 startingPosition;
    private Transform playerPosition;
    private NavMeshAgent agent; 
    private float agentSpeed;

    void Start()
    {

        // Initialize variables
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerPosition = player.GetComponent<Transform>();
        startingPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        agent.autoBraking = false;
    }

    void Update()
    {
          agent.destination = playerPosition.position; 
          if(playerController.isMoving || playerController.isBreathing)
          {
              agent.speed = agentSpeed;
          }
          else
          {
              agent.speed = 0f;
          }
    }


    //--------------------HELPER METHODS--------------------//

    /**
     *  Helper methods that moves the enemy to the player
     **/
    public void moveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, followSpeed * Time.deltaTime);
    }

    /**
     *  Helper methods that moves the enemy to the thier starting location
     **/
    public void moveBackToStart()
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveBackSpeed * Time.deltaTime);
    }
    
}
