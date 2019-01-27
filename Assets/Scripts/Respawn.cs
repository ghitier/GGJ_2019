using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	//public GameObject player;
	public GameObject checkpoint;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Player" || collision.collider.tag == "Ball")
		{
			GameManager.instance.player.transform.position = checkpoint.transform.position;
			GameManager.instance.SpawnOrbToPlayer();
			Debug.Log("TEST");
		}
	}
}
