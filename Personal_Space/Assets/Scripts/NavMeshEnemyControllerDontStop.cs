using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemyControllerDontStop : MonoBehaviour
{
    
    private GameObject player;
    private PlayerController playerController;
    private Vector3 startingPosition;
    private Transform playerPosition;
    private NavMeshAgent agent; 
    private float agentSpeed;

    private bool active = false;
    private bool waiting = false;

    public GameObject zone;

    public float delay;

    private ZoneScript zoneControl;
    
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
        active = false;
        agent.speed = 0;
        zoneControl = zone.GetComponent<ZoneScript>();
        
    }

    void Update()
    {
        if(zoneControl.getPlayerInZone() && !waiting)
        {
            waiting = true;
            StartCoroutine(DelayTime());
        }
        if(active)
        {
            agent.destination = playerPosition.position;
            agent.speed = agentSpeed;
        }
    }

    //--------------------HELPER METHODS--------------------//

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(delay);
        active = true;
        waiting = false;
    }
    
}
