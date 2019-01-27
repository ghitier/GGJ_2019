using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 2, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Eject(GameObject orb)
    {
        orb.transform.position = GetComponent<Collider>().bounds.center;
        orb.transform.rotation = transform.rotation;
        // orb.GetComponent<Rigidbody>().AddForce(Vector3.forward * 25, ForceMode.Impulse);
        orb.GetComponent<Rigidbody>().velocity = transform.rotation * Vector3.forward * 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Portal collided with: {other.gameObject.name}");
        if (other.gameObject.name.Contains("Orb") && GameManager.instance.hasBall)
        {
            GameManager.instance.EjectBall();
            GameObject orb = GameManager.instance.Ball;
            GameManager.instance.room2Manager.TeleportOrb(orb, gameObject);
            // orb.transform.position = GameObject.Find("DEBUG__ROOM2_SPAWN").transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 p = GetComponent<Collider>().bounds.center;
        Gizmos.DrawLine(p, p + transform.rotation * Vector3.forward * 5);
    }
}
