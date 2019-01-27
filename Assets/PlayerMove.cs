using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
    private void Start()
    {
        GameManager.dropBallDelegate += DropBool;
        GameManager.takeBallDelegate += TakeBool;

    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            GetComponent<Animator>().SetBool("Running", true);
           
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
    }

    void DropBool()
    {
        GetComponent<Animator>().Play("Armature|pickUp");
        GetComponent<Animator>().SetBool("Orb", false);
    }

    void TakeBool()
    {
        GetComponent<Animator>().Play("Armature|pickUp");
        GetComponent<Animator>().SetBool("Orb", true);
    }

    private void OnDestroy()
    {
        GameManager.dropBallDelegate -= DropBool;
        GameManager.takeBallDelegate -= TakeBool;
    }
}