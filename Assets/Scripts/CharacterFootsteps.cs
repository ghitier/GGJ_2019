using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.Characters.ThirdPerson;

// UNUSED

public class CharacterFootsteps : MonoBehaviour
{
    private ThirdPersonCharacter uc;
    [SerializeField] private AudioClip[] foostepSoundsWithOrb = { };
    [SerializeField] private AudioClip[] foostepSoundsWithoutOrb = { };

    // Start is called before the first frame update
    void Start()
    {
        uc = GameManager.instance.player.GetComponent<ThirdPersonCharacter>();
        GameManager.takeBallDelegate += SetFootstepsWithBall;
        GameManager.dropBallDelegate += SetFootstepsWithoutBall;
        SetFootstepsWithBall();
    }

    private void SetFootstepsWithBall()
    {
        uc.m_FootstepSounds = foostepSoundsWithOrb;
    }

    private void SetFootstepsWithoutBall()
    {
        uc.m_FootstepSounds = foostepSoundsWithoutOrb;
    }

}
