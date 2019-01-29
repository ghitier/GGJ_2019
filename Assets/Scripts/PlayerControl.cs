using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject Cradle;
    public GameObject Chase;
    public GameObject Camera;

    public CharacterController cc;
    Vector3 direction;
    Quaternion targetRotation;


    public float MoveSpeed;
    public float RotSpeed;
    public float CameraDistance;
    public float CameraHeight;
    public float Gravity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 forward = new Vector3 (Cradle.transform.TransformDirection(direction).x,0, Cradle.transform.TransformDirection(direction).z) ;
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(forward), Time.deltaTime * RotSpeed);
            
            if (cc.isGrounded)
            {
                forward.y = 0;
            }
            else
            {
                forward.y -= Gravity;
            }

            cc.Move(forward * Time.deltaTime * MoveSpeed);
        }
       

    
       


    }
}
