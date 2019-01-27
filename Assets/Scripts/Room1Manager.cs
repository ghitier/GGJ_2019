using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SpawnPlayer(spawnPoint.position, spawnPoint.rotation);
        GameManager.instance.SpawnOrbToPlayer();

        //lancer musique
        SoundManager.instance.SwitchMusic(1);
    }

}
