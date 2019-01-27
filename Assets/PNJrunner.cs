using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class PNJrunner : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public Transform[] waypoints;
    public float range = 10;

    public float distanceToPlayer;
    public float distanceToNextWaypoint;
    public float distanceBetweenPLayerAndNextWaypoint;
    public int nextWaypoint = 0;
    public bool invertDirection = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (waypoints.Length ==0)
        {
            Debug.LogError("You forgot to assign waypoint to the pnj ! Drag ang drop your waypoints Transform to the editor");
        }
    }

    void Update()
    {
         distanceToPlayer = Vector3.Distance(transform.position, player.position);
         distanceToNextWaypoint = Vector3.Distance(waypoints[nextWaypoint].position, transform.position);
         distanceBetweenPLayerAndNextWaypoint = Vector3.Distance(waypoints[nextWaypoint].position, player.transform.position);


         if (distanceToPlayer < range && GameManager.instance.hasBall)
         {
            RUNMOTHERFUCKER();
         }
         else
         {
             agent.isStopped = true;
         }


         if (distanceToNextWaypoint < 2)
         {
             goToNextWaypoint();
         }

    }

    void RUNMOTHERFUCKER()
    {
        if(distanceToNextWaypoint > distanceBetweenPLayerAndNextWaypoint)
        {
            switchDirection();
            goToNextWaypoint();
        }
        agent.isStopped = false;
        agent.SetDestination(waypoints[nextWaypoint].position);
    }

    void switchDirection()
    {
        invertDirection = !invertDirection;
    }
    void goToNextWaypoint()
    {
        if (!invertDirection)
        {
            if (nextWaypoint + 1 >= waypoints.Length)
                nextWaypoint = 0;
            else
                nextWaypoint++;
        }
        else
        {
            if (nextWaypoint - 1 < 0)
                nextWaypoint = waypoints.Length-1;
            else
                nextWaypoint--;
        }
        agent.isStopped = false;
        agent.SetDestination(waypoints[nextWaypoint].position);
    }
}