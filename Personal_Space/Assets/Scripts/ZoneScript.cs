using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public float zoneThreat = 0;
    public bool playerInZone = false;

    private List<EnemyController> enemies = new List<EnemyController>();
    private List<Transform> childrenTransofrm = new List<Transform>();
    private bool playerMoving;

    public float scale = 1;

    void Start()
    {
        getEnemiesinZone();
        resizeZone(scale);
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
     *  Helper function that resizes the zone
     **/
    private void resizeZone(float size)
    {
        foreach(Transform child in childrenTransofrm)
        {
            child.localScale = new Vector3(child.localScale.x / size, child.localScale.y / size, child.localScale.z / size);
            child.localPosition = new Vector3(child.localPosition.x / size, child.localPosition.y / size, child.localPosition.z / size);
        }
        transform.localScale = new Vector3(size * transform.localScale.x, size * transform.localScale.y, size * transform.localScale.z);
    }
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
                childrenTransofrm.Add(child.gameObject.GetComponent<Transform>());
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
