using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public float zoneThreat = 0;
    public bool playerInZone = false;

    private List<EnemyController> enemies = new List<EnemyController>();
    private bool playerMoving;


    void Start()
    {
        getEnemiesinZone();
    }

    void Update()
    {
        updatePlayerMove();
        if (playerMoving)
        {
            if (playerInZone)
            {
                moveChildrenToPlayer();
            }
            else
            {
                moveChildrenToStart();
            }
        }
        else
        {
            if (playerInZone)
            {
                stopChildrenMovement();
            }
        }
    }

    //--------------------HELPER METHODS--------------------//

    /**
     *  Helper function to get the enmies that are children of the zone
     **/
    private void getEnemiesinZone()
    {
        var children = this.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            if(child.tag == "Enemy")
            {
                enemies.Add(child.gameObject.GetComponent<EnemyController>());
            }
        }
    }

    /**
     *  Helper function that moves all children to player
     **/
    private void moveChildrenToPlayer()
    {
        foreach(EnemyController enemy in enemies)
        {
            enemy.moveTowardsPlayer();
        }
    }

    private void stopChildrenMovement()
    {
        foreach (EnemyController enemy in enemies)
        {
            enemy.stopMovement();
        }
    }

    /**
     *  Helper function that moves all children to thier starting location
     **/
    private void moveChildrenToStart()
    {
        foreach(EnemyController enemy in enemies)
        {
            enemy.moveBackToStart();
        }
    }

    /**
     *  Helper function that updates the playerMoving boolean
     **/
    private void updatePlayerMove()
    {
        playerMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isMoving;
    }
}
