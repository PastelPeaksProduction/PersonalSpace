using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public float zoneThreat = 0;
    public bool playerInZone = false;

    private List<EnemyController> enemies = new List<EnemyController>();

    void Start()
    {
        getEnemiesinZone();
    }

    void Update()
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
}
