 using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;


    public class PatrolEnemyController : MonoBehaviour {

        public Transform[] points;
        private int destPoint = 0;
        private NavMeshAgent agent;
        private float agentSpeed;

        public float waitTime;

        private bool waiting = false;
    private GameController gameController;
        void Start () {
            agent = GetComponent<NavMeshAgent>();
            agentSpeed = agent.speed;
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            agent.autoBraking = false;

            GotoNextPoint();
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        }


        void GotoNextPoint() {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }


        void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (gameController.isGamePaused())
        {
            agent.speed = 0;
            return;
        }
        
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    if(!waiting)
                    {
                        agent.speed = 0;
                        waiting = true;
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
        }
    }