using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{
    public bool isAlone;
    [SerializeField] float pickupDistance;
    [SerializeField] GameObject characterRef;

    private float characterColliderRadius;

    // Start is called before the first frame update
    void Start()
    {
        characterColliderRadius = characterRef.GetComponent<CapsuleCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (isAlone)
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
        Physics.IgnoreCollision(GetComponent<Collider>(), characterRef.GetComponent<Collider>());
        transform.parent = characterRef.transform;
        transform.localPosition = new Vector3(0f, 1f, 0.5f);
        isAlone = false;
    }

    private void RemoveFromCharacter()
    {

        transform.parent = null;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Physics.IgnoreCollision(GetComponent<Collider>(), characterRef.GetComponent<Collider>(), false);
        isAlone = true;
    }
}
