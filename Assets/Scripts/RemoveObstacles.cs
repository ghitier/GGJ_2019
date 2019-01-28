using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObstacles : MonoBehaviour
{
    public List<GameObject> obstacles;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
}
