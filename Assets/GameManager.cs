using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public Transform FPS_Camera_position;
    public Transform ThirdPerson_Camera_position;
    public GameObject Ball;
    public Canvas GameUI;
    public GameObject player;
    public float interactDistance;

    public float transitionDurationBetweenCamera; 
    int camPos = 0;

    //some Texts positionned in the spherical ball canvas to display quotes
    public List<Text> ballData;

    public ThirdPersonUserControl userControl;
    bool gameRun = true;
    public bool hasBall = false;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        loadBallData();
        switchCamPosition();
        GameUI.enabled = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // switch camera view (Drop the ball)
        {
            if (hasBall)
            {
                dropBall();
            }
            else
            {
                takeBall();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Pause menu  
        {
            if (gameRun)
            {
                Pause();
            }
            else
            {
               UnPause();
            } 
        }
    }

    /// <summary>
    /// Switch game view (3rd person / Fps)
    /// </summary>
    public void switchCamPosition()
    {
        if (camPos == 0)
        {
            // lerp from 3rd person cam to fps cam
            StartCoroutine(Transition(FPS_Camera_position));
            camPos = 1;
        }
        else
        {
            // lerp from fps cam to 3rd person cam
            StartCoroutine(Transition(ThirdPerson_Camera_position));
            camPos = 0;
        }
    }

    /// <summary>
    /// Lerp the camera to the specified transform
    /// </summary>
    IEnumerator Transition(Transform target)
    {
        Camera.main.transform.parent = target.transform;
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        Quaternion startingRot = Camera.main.transform.rotation;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDurationBetweenCamera);
            Camera.main.transform.position = Vector3.Lerp(startingPos, target.position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startingRot, target.rotation, t);
            yield return 0;
        }
    }

    /// <summary>
    /// Take the ball if player is near enought
    /// </summary>
    void takeBall()
    {
        if (Vector3.Distance(Ball.transform.position, player.transform.position) < interactDistance)
        {
            Ball.transform.parent = player.transform;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
            hasBall = true;
            switchCamPosition(); // switch to 3rd person view
        }
    }

    /// <summary>
    /// Drop the ball
    /// </summary>
    void dropBall()
    {
        Ball.transform.parent = transform.parent;
        hasBall = false;
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        switchCamPosition();// switch to 1st person view
    }
   
    /// <summary>
    /// THis load data from txt file to display in the ball
    /// </summary>
    void loadBallData()
    {
        if (!File.Exists("Legacy.txt")) throw new Exception("Load failed : 'Legacy.txt' not exis, something went wrong");

        string[] lines = File.ReadAllLines("Legacy.txt");

        if (lines.Length >= ballData.Count)
        {
            for (int i = 0; i < ballData.Count; i++)
            {
                ballData[i].text = lines[i];
            }
        }
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    private void Pause()
    {
        //Make the ball rotate
        Ball.GetComponent<Ball>().rotate = true;
        //Lerp the camera to the center of the ball
        StartCoroutine(Transition(Ball.transform));
        // revert the ball normals
        Ball.GetComponent<Ball>().revert();
        //prevent the camera from rotating with the ball
        Camera.main.transform.parent = transform.parent;
        //prevent the character from moving
        userControl.enabled = false;
        //update boolean
        gameRun = false;
        //display ui
        GameUI.enabled = true;
    }

    /// <summary>
    /// return to the game
    /// </summary>
    public void UnPause()
    {
        //Stop the ball rotation
        Ball.GetComponent<Ball>().rotate = true;
        // revert the ball normals
        Ball.GetComponent<Ball>().revert();
        //allow character control
        userControl.enabled = true;
        //Lerp the camera to the last position
        if(camPos == 0)
        {
            StartCoroutine(Transition(ThirdPerson_Camera_position));
        }
        else
        {
            StartCoroutine(Transition(FPS_Camera_position));
        }
        //update boolean
        gameRun = true;
        //hide ui
        GameUI.enabled = false;
    }

    /// <summary>
    /// Quit app
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
