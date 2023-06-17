using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;
    private Rigidbody rb;
    private bool grounded = true;
    private float dist;

    public float range = 50f;
    public float rnageOfJumping = 10f;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, playerTransform.position);

        DecideToJump();
    }

    private void Move(bool NavOrRB)
    {
        if (NavOrRB == true)
        {
            if (!agent.isStopped && dist <= range)
            {
                agent.SetDestination(playerTransform.position);
            }
        }else
        {
            if (dist <= range)
            {
                Vector3 movDir = (playerTransform.position - transform.position).normalized;

                rb.velocity = new Vector3(movDir.x * speed, rb.velocity.y, movDir.z * speed);
            }
        }
    }

    private void Jump()
    {
        grounded = false;
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.isStopped = true;
        }
        
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddRelativeForce(new Vector3(0, 3f, 0), ForceMode.Impulse);
    }

    private void DecideToJump()
    {
        if (playerTransform.position.y > transform.position.y + 2.5 && dist <= rnageOfJumping)
        {
            Jump();
            Move(false);
        }else
        {
            Move(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Ground")
        {
            if (!grounded)
            {
                if (agent.enabled)
                {
                    Vector3 tmp = transform.position;
                    agent.updatePosition = true;
                    agent.updateRotation = true;
                    agent.isStopped = false;
                    agent.SetDestination(tmp);
                    transform.position = tmp;
                }

                rb.isKinematic = true;
                rb.useGravity = false;
                grounded = true;
            }
        }
    }
}
