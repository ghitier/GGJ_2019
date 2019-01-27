using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public Transform FPS_Camera_position;
    public Transform ThirdPerson_Camera_position;
    public GameObject Ball;
    public Canvas GameUI; // le menu pause, ni plus ni moins.
    public Canvas InputField; //Pour l'ajout de nouvelle phrases dans la bouboule
    public GameObject player;

    [SerializeField]
    public GameObject PNJ_runner;
    public GameObject PNJ_runner_Prefab;
    private Vector3 PNJ_runner_initPos;
    public float interactDistance;

    public float transitionDurationBetweenCamera; 
    int camPos = 0;

    //some Texts positionned in the spherical ball canvas to display quotes
    public List<string> ballData;

    public ThirdPersonUserControl userControl;
    bool gameRun = true;
    public bool hasBall = false;

    // Those are used to store the ball movement (To be able to stop the ball at Pause() and give it back when UnPause() ): 
    private Vector3 ballVelocity;
    private Vector3 ballAngularVelocity;


    // GENERATE Event : 
    public delegate void OnDropBallDelegate();
    public static OnDropBallDelegate dropBallDelegate;

    public delegate void OnTakeBallDelegate();
    public static OnTakeBallDelegate takeBallDelegate;

    public delegate void OnPauseDelegate();
    public static OnPauseDelegate pauseDelegate;

    public delegate void OnResumeDelegate();
    public static OnResumeDelegate resumeDelegate;

    void Awake()
    {

        ballData = new List<string>();
        loadBallData();
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
        switchCamPosition();
        GameUI.enabled = false;
        InputField.enabled = false;
        PNJ_runner_initPos = PNJ_runner.transform.position;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // switch camera view (Drop the ball)
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
            Ball.transform.localPosition = new Vector3(0, 1, 0.5f);

            Ball.GetComponent<Collider>().isTrigger = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;

            hasBall = true;

            switchCamPosition(); // switch to 3rd person view
            
            takeBallDelegate(); // trigger event
        }
    }

    /// <summary>
    /// Drop the ball
    /// </summary>
    void dropBall()
    {
        Ball.transform.parent = transform.parent;
        hasBall = false;
        Ball.GetComponent<Collider>().isTrigger = false;
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        switchCamPosition();// switch to 1st person view

        dropBallDelegate(); // trigger Event
    }
   
    /// <summary>
    /// THis load data from txt file to display in the ball
    /// </summary>
    void loadBallData()
    {
        if (!File.Exists("Legacy.txt")) throw new Exception("Load failed : 'Legacy.txt' not exis, something went wrong");

        string[] lines = File.ReadAllLines("Legacy.txt");

        for (int i = 0 ; i < lines.Length; i++)
        {
            ballData.Add(lines[i]);
        }
    }

    /// <summary>
    /// Place new string at the end of the file, and hide input field
    /// </summary>
    public void SaveBallData(string s)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("Legacy.txt", true))
        {
            file.WriteLine(s);
            InputField.enabled = false;
        }
    }

    public void DisplayInputField()
    {
        InputField.enabled = true;
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
        // Store ball velocity
        ballVelocity = Ball.GetComponent<Rigidbody>().velocity;
        ballAngularVelocity = Ball.GetComponent<Rigidbody>().angularVelocity;
        //Stop the ball
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        pauseDelegate();
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
        //give back the velocity the ball had before Pause()
        Ball.GetComponent<Rigidbody>().velocity = ballVelocity;
        Ball.GetComponent<Rigidbody>().angularVelocity = ballAngularVelocity;

        resumeDelegate();
    }

    public void resPawnPnj()
    {
        GameObject newPnj = Instantiate(PNJ_runner);
        newPnj.transform.position = PNJ_runner_initPos;
        newPnj.GetComponent<NavMeshAgent>().enabled = true;
        PNJ_runner = newPnj;

    }

    /// <summary>
    /// Quit app
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
