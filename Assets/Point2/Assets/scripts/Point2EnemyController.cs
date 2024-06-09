using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point2EnemyController : MonoBehaviour
{
    public float lookRadious = 60;
    Transform target;
    NavMeshAgent agent;
    
    private Point2GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        /* sets the navmesh agent to the agent variable
        sets the players transform to the target variable */
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        gM = GameObject.Find("Game Manager").GetComponent<Point2GameManager>();
        gM.UpdateEnemyCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        // this is responsible for making the enemy move twards the player
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadious)
        {
            agent.SetDestination(target.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        /* draws a sphere whire mesh thats red when you click on the
        enemy in the heirchy */
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadious);
    }
}
