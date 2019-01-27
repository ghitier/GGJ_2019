using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class PNJrunner : MonoBehaviour
{
    NavMeshAgent agent;
    //Transform player;

    public Transform[] waypoints;
    public Transform jumpSpot;
    public Transform spotAuDessusDuVide;

    public GameObject DissapearingWall;
    public GameObject DissapearingWall2;

    public float range = 10;
    public float transitionFromJumpSpotToSpotAuDessusDuVide;

    public float distanceToPlayer;
    public float distanceToNextWaypoint;
    public float distanceBetweenPLayerAndNextWaypoint;
    public float distanceToJumpSpot;

    public int nextWaypoint = 0;
    public bool invertDirection = false;

    public float speedWhenRunning;
    public float speedWhengoingTOJumpSpot;

    bool isGonnaDie = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedWhenRunning;
        //player = GameManager.instance.player.transform;

        if (waypoints.Length ==0)
        {
            Debug.LogError("You forgot to assign waypoint to the pnj ! Drag ang drop your waypoints Transform to the editor");
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        distanceToNextWaypoint = Vector3.Distance(waypoints[nextWaypoint].position, transform.position);
        distanceBetweenPLayerAndNextWaypoint = Vector3.Distance(waypoints[nextWaypoint].position, GameManager.instance.player.transform.position);
        distanceToJumpSpot = Vector3.Distance(transform.position, jumpSpot.position);

        if (!isGonnaDie)
        {
            if (distanceToPlayer < range && GameManager.instance.hasBall)
            {
                RUNMOTHERFUCKER();

                if (distanceToNextWaypoint < 2) // Reached Waypoint
                {
                    goToNextWaypoint();
                }
            }
            else
            {
                agent.isStopped = true;

                if (distanceToPlayer < 3) // player has catch pnj
                {
                    agent.isStopped = false;
                    agent.speed = speedWhengoingTOJumpSpot;
                    agent.SetDestination(jumpSpot.position);
                    DissapearingWall.SetActive(false);
                }
            }

            if (distanceToJumpSpot < 2)
            {
                Debug.Log("DIIIIIIIIE");
                DIEMOTHERFUCKER();
            }
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

    void DIEMOTHERFUCKER()
    {
        agent.enabled = false;
        isGonnaDie = true;
        Debug.Log("Should jump to die");
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Transition(spotAuDessusDuVide));
        GameManager.instance.Ball.SetActive(false);
        DissapearingWall2.SetActive(false);
    }
    /// <summary>
    /// Lerp the camera to the specified transform
    /// </summary>
    IEnumerator Transition(Transform target)
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionFromJumpSpotToSpotAuDessusDuVide);
            transform.position = Vector3.Lerp(startingPos, target.position, t);
            yield return 0;
        }
    }
}