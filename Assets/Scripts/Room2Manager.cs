using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Manager : MonoBehaviour
{
    [SerializeField] GameObject[] doors = { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportOrb(GameObject orb, GameObject from)
    {
        // Debug.Log($"There is a list of {doors.Length} doors");
        List<GameObject> availableDoors = new List<GameObject>();
        for (int i = 0; i < doors.Length - 1; i++)
            if (from.name != doors[i].name)
                availableDoors.Add(doors[i]);
        // Debug.Log($"There are {availableDoors.Count} doors available");
        int r = Random.Range(0, availableDoors.Count);
        GameObject door = availableDoors[r];
        door.GetComponentInChildren<Portal>().Eject(orb);
    }

}
