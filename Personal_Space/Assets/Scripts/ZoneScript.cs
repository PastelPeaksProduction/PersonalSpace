﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    [Range(0f, 30.0f)]
    public float zoneScaling;

    [HideInInspector]
    public float zoneThreat;
    public bool playerInZone = false;

    public bool basicEnemyType = false;

    private List<GameObject> enemies = new List<GameObject>();
    private List<Transform> childrenTransofrm = new List<Transform>();
    private bool playerMoving;
    private bool playerBreathing;

    public float scale = 1;
    private float dangerScale = 0.01f;
    private float safeScale = 0.5f;

    void Start()
    {
        getEnemiesinZone();
        resizeZone(scale);
        scaleZoneThreat();
    }

    void Update()
    {
        updatePlayerMove();
        if (playerMoving || playerBreathing)
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
            
                stopChildrenMovement();
            
        }
    }

    //--------------------HELPER METHODS--------------------//

    /**
     *  Helper function that resizes the zone
     **/
    private void resizeZone(float size)
    {
         foreach (Transform child in childrenTransofrm)
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
        if (basicEnemyType)
        {
            enemies.Add(this.transform.parent.gameObject);
            childrenTransofrm.Add(this.transform.parent.transform);
        }
        else
        {

            var parent = this.transform.parent.gameObject;

            var children = parent.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {

                if (child.tag == "Enemy")
                {
                    enemies.Add(child.gameObject);
                    childrenTransofrm.Add(child.gameObject.GetComponent<Transform>());
                }
            }
        }
    }

    /**
     *  Helper function that moves all children to player
     **/
    private void moveChildrenToPlayer()
    {
        foreach(GameObject enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.moveTowardsPlayer();
            }
            
        }
    }

    private void stopChildrenMovement()
    {
      

        foreach (GameObject enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.stopMovement();
            }

        }
    }

    /**
     *  Helper function that moves all children to thier starting location
     **/
    private void moveChildrenToStart()
    {
        

        foreach (GameObject enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.moveBackToStart();
            }

        }
    }

    /**
     *  Helper function that updates the playerMoving boolean
     **/
    private void updatePlayerMove()
    {
        playerMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController1>().isMoving;
        playerBreathing = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController1>().isBreathing;
    }

    /**
     *  Helper function that scales the threat based on number of enemies
     **/
    private void scaleZoneThreat()
    {
        float enemyCount = enemies.Count;
        if(enemyCount == 0)
        {
            zoneThreat = zoneScaling * safeScale;
        }
        else
        {
            zoneThreat = -zoneScaling * dangerScale* enemyCount;
        }
    }

    public bool getPlayerInZone()
    {
        return playerInZone;
    }
}
