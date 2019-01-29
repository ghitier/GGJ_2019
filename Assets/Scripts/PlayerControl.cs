using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject Cradle;

    public GameObject Camera;
    public GameObject Orb;
    public GameObject OrbMarker;


    public CharacterController cc;

    Vector3 direction;
    Vector3 forward;
    Quaternion targetRotation;

    bool hasOrb = true;
    bool isGrounded;
    bool onSlope;
    public bool CanPickUp;


    public float MoveSpeed;
    public float RotSpeed;
    public float CameraDistance;
    public float CameraHeight;
    public float Gravity;

    float slopeDegree;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GroundCheck()
    {

        Vector3 back = transform.TransformDirection(new Vector3(0, -1, -0.5f));
        Ray downRay = new Ray(this.gameObject.transform.position, Vector3.down);
        Ray backRay = new Ray(this.gameObject.transform.position, back);
        RaycastHit surface;
       
        

        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out surface))
        {


            if (surface.normal.y != 1)
            {
               
                onSlope = true;
                slopeDegree = Vector3.Angle(surface.normal, transform.up) - 90;
            }
            else
            {
                onSlope = false;
            }

        }
    }

    void Move()
    {
      
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!onSlope)
        {
            forward = new Vector3(Cradle.transform.TransformDirection(direction).x, 0, Cradle.transform.TransformDirection(direction).z);
        }else if (onSlope)
        {
            forward = new Vector3(Cradle.transform.TransformDirection(direction).x, slopeDegree/100, Cradle.transform.TransformDirection(direction).z);
        }
       
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(forward.x,0, forward.z)), Time.deltaTime * RotSpeed);

        cc.Move(forward * Time.deltaTime * MoveSpeed);

   
    }

    void Fall()
    {
        forward.y -= Gravity;
        cc.Move(forward * Time.deltaTime * MoveSpeed);
    }

    void PickOrb()
    {
        Orb.transform.SetParent(this.gameObject.transform);
        Orb.transform.position = OrbMarker.transform.position;
        Orb.GetComponent<Rigidbody>().useGravity = false;
        Orb.GetComponent<Rigidbody>().isKinematic = true;
        hasOrb = !hasOrb;
    }

    void DropOrb()
    {
      
        Orb.transform.SetParent(null);
        Orb.GetComponent<Rigidbody>().useGravity = true;
        Orb.GetComponent<Rigidbody>().isKinematic = false;
        hasOrb = !hasOrb;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(slopeDegree / 100);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5, Color.blue);
        GroundCheck();
     
        if (Vector3.Distance(this.gameObject.transform.position, Orb.transform.position) < 1)
        {
            CanPickUp = true;
        }
        else
        {
            CanPickUp = false;
        }


        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && cc.isGrounded)
        {
            Move();
        }
        else if (!cc.isGrounded)
        {
            Fall();
        }

        if (Input.GetButtonDown("Interract"))
        {
            if (hasOrb)
            {
                DropOrb();
            }
            else if (!hasOrb && CanPickUp)
            {
                PickOrb();
            }
        }



    }
}
