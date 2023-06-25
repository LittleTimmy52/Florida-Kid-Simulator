using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float dist;

    public float range = 50f;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // See how close/far the player is
        dist = Vector3.Distance(transform.position, playerTransform.position);

        // Move twards the player if in range
        if (!agent.isStopped && dist <= range)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    /* In the future add in multiplayer function to movement
    IE just make the cop go after the closest player */

    /* I wanted to make the enemy jump, but it was just not working, the enemy would
    just not jump, when it did on occasion, it just stayed still. Later try maybe to
    make it jump, possibly re look into MLAgents (If you use MLAgents, have 2 AI, one
    renning away from enemies and one being the enimies themselves) */
}
