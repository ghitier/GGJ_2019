using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    // [SerializeField] float pickupDistance;
    // [SerializeField] GameObject characterRef;
    [SerializeField] AudioClip dropSfx;
    [SerializeField] AudioClip rollSfx;
    // [SerializeField] bool startGrounded;

    // private OrbController orbControllerRef;
    private bool isGrounded = false;
    private bool isRolling = false;
    private float lastRoll;

    // Start is called before the first frame update
    void Start()
    {
        lastRoll =  Time.time;
        // orbControllerRef = characterRef.GetComponent<OrbController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (!orbControllerRef.HasOrb)
            {
                float distance = (characterRef.transform.position - transform.position).magnitude;
                if (distance < pickupDistance)
                {
                    AddToCharacter();
                }
            }
            else
            {
                RemoveFromCharacter();
            }
        }
        */
    }

    /*
    private void AddToCharacter()
    {
        // Mabe multiple orbes are in range, so mabe the character might get multiple requests

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        transform.parent = characterRef.transform;
        transform.localPosition = new Vector3(0f, 1f, 0.5f);
        orbControllerRef.PickupOrb(gameObject);
    }

    private void RemoveFromCharacter()
    {

        transform.parent = null;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        orbControllerRef.DropOrb();
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            AudioSource sfxAudioSource = GetComponent<AudioSource>();
            sfxAudioSource.PlayOneShot(dropSfx);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            float t = (2 * Mathf.PI) / rb.angularVelocity.magnitude;

            if (isRolling == false || Time.time > lastRoll + t)
            {
                isRolling = true;
                lastRoll = Time.time;
                AudioSource sfxAudioSource = GetComponent<AudioSource>();
                // Debug.Log("Rock N Roll bb <3");
                sfxAudioSource.PlayOneShot(rollSfx);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isRolling = false;
        }
    }
}
