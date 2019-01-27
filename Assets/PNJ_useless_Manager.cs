using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PNJ_useless_Manager : MonoBehaviour
{
    public Transform spawnPlace;
    //public Transform destroyPlace;
    public GameObject PNJ_useless;

    public float interval;

    List<GameObject> pnjs;
    void Start()
    {
        InvokeRepeating("SpawnPNJ",0, interval);
        pnjs = new List<GameObject>();
    }
    
    void SpawnPNJ()
    {
        GameObject newPnj = Instantiate(PNJ_useless,transform);
        newPnj.transform.position = spawnPlace.position;
        //newPnj.GetComponent<PNJ_useless>().destroyPlace = destroyPlace;
        newPnj.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        pnjs.Add(newPnj);
    }

    private void Update()
    {
        GameObject currentPnj = null;
        for (int i = 0; i < pnjs.Count; i++)
        {
            currentPnj = pnjs[i];
            if (Vector3.Distance(currentPnj.transform.position, transform.position) < 2)
            {
                pnjs.Remove(currentPnj);
                Destroy(currentPnj);
            }
        }
    }
}
