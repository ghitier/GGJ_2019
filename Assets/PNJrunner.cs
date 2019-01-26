using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PNJrunner : MonoBehaviour
{

    NavMeshAgent agent;
    Transform player;

    int multiplier = 1; // or more
    float range = 10;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 runTo = transform.position + ((transform.position - player.position) * multiplier);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < range && GameManager.instance.hasBall)
        {
            agent.SetDestination(runTo);
        }
    }
}