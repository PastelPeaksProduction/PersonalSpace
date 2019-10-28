 using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;


    public class EnemyGaggleFollower : MonoBehaviour {

        private GameObject leader;
        private NavMeshAgent agent;

        private Transform leaderPosition;
        void Start () 
        {
            //leader = GetComponentInParent<GameObject>();
            leaderPosition = this.transform.parent;
            agent = GetComponent<NavMeshAgent>();
            //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            agent.autoBraking = false;
            agent.destination = leaderPosition.position;
        }


        void Update () 
        {
           agent.destination = leaderPosition.position; 
        }

    }