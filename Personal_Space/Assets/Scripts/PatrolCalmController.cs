 using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;


    public class PatrolCalmController : MonoBehaviour {

        public Transform[] points;
        private int destPoint = 0;
        private NavMeshAgent agent;
        private float agentSpeed;

        public float waitTime;
        public GameObject zone;
        private ZoneScript zoneControl;

        private bool waiting = false;
        private bool waitingForKid = false;
        private PlayerController playerController;
        void Start () 
        {
            agent = GetComponent<NavMeshAgent>();
            agentSpeed = agent.speed;
            //agent.autoBraking = false;
            zoneControl = zone.GetComponent<ZoneScript>();
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            //GotoNextPoint();
        }


        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }


        void Update () 
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if(!waiting)
                {
                    agent.speed = 0;
                    waiting = true;
                }
                    
            }
            if(!waiting)
            {
                
                    agent.speed = agentSpeed;
                
                
            }
            else
            {
                agent.speed = 0;
                if(zoneControl.getPlayerInZone() && !waitingForKid)
                {
                    waitingForKid = true;
                    StartCoroutine(Loiter());
                }
            }
        }

        private IEnumerator Loiter()
        {
            yield return new WaitForSeconds(waitTime);
            agent.speed = agentSpeed;
            GotoNextPoint();
            waiting = false;
            waitingForKid = false;
        }
    }