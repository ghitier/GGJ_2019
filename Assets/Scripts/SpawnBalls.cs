using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{

    [SerializeField] private int numberOfBalls;
    [SerializeField] private GameObject ball;
    private Bounds planeBounds;
    private Bounds ballBounds;

    // Start is called before the first frame update
    void Start()
    {
        planeBounds = GetComponent<MeshCollider>().bounds;
        ballBounds = ball.GetComponent<SphereCollider>().bounds;
        // for (int i = 0; i < numberOfBalls; i++)
        //    SpawnBall(i);
        InitiateSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int maxFailStreak = 10;
    List<Vector3> spawnedPositions = new List<Vector3>();

    private bool CanSpawnAt(Vector3 p)
    {
        foreach (Vector3 v in spawnedPositions)
        {
            // Debug.Log($"Checking v {v}");
            Bounds a = new Bounds(p, ballBounds.size);
            Bounds b = new Bounds(v, ballBounds.size);
            if (b.Intersects(a))
                return false;
        }
        return true;
    }

    private void InitiateSpawn()
    {
        int failStreak = 0;
        for (int i = numberOfBalls; i >= 0;)
        {
            float x = Random.Range(planeBounds.min.x, planeBounds.max.x);
            float z = Random.Range(planeBounds.min.z, planeBounds.max.z);
            Vector3 p = new Vector3(x, transform.position.y + (ballBounds.size.y / 2) + 0.05f, z);
            if (CanSpawnAt(p))
            {
                GameObject ballInstance = Instantiate(ball, p, transform.rotation);
                ballInstance.name = $"Spawned ball {i}";
                spawnedPositions.Add(p);
                failStreak = 0;
                i--;
            }
            else
            {
                Debug.Log($"Cannot spawn at {p}");
                failStreak++;
            }
        }
    }

    // Spawn a ball at a random position on the plane
    private void SpawnBall(int i = 0)
    {
        GameObject ballInstance = Instantiate(ball, new Vector3(
            Random.Range(planeBounds.min.x, planeBounds.max.x),
            transform.position.y + (ballBounds.size.y / 2) + 0.05f,
            Random.Range(planeBounds.min.z, planeBounds.max.z)
        ), transform.rotation);
        ballInstance.name = $"Spawned ball {i}";
    }
}
