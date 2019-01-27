using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public bool isAlone;
    [SerializeField] float pickupDistance;
    [SerializeField] GameObject characterRef;

    private OrbController orbControllerRef;

    // Start is called before the first frame update
    void Start()
    {
        orbControllerRef = characterRef.GetComponent<OrbController>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

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
}
