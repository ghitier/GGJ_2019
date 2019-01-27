using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject FinalBall;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GameManager.instance.Transition(FinalBall.transform));
        // show input field
        GameManager.instance.DisplayInputField();
    }
}
