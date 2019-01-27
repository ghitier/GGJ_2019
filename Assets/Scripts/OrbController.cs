using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class OrbController : MonoBehaviour
{
    [SerializeField] private GameObject orbRef;
    [SerializeField] private AudioClip[] foostepSoundsWithOrb = { };
    [SerializeField] private AudioClip[] foostepSoundsWithoutOrb = { };

    // Start is called before the first frame update
    void Start()
    {
        SetFootsteps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasOrb { get => orbRef != null; }

    public void PickupOrb(GameObject orb)
    {
        if (orbRef != null)
            return;

        orbRef = orb;
        Physics.IgnoreCollision(GetComponent<Collider>(), orbRef.GetComponent<Collider>());
        SetFootsteps();
    }

    public void DropOrb()
    {
        if (orbRef == null)
            return;

        Physics.IgnoreCollision(GetComponent<Collider>(), orbRef.GetComponent<Collider>(), false);
        orbRef = null;
        SetFootsteps();
    }

    private void SetFootsteps()
    {
        ThirdPersonCharacter uc = GetComponent<ThirdPersonCharacter>();
        if (HasOrb)
        {
            uc.m_FootstepSounds = foostepSoundsWithOrb;
        }
        else
        {
            uc.m_FootstepSounds = foostepSoundsWithoutOrb;
        }
    }
}
