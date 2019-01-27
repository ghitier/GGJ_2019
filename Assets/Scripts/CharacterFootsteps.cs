using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterFootsteps : MonoBehaviour
{
    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private AudioMixerGroup mixerGroup;
    ThirdPersonCharacter thirdPersonRef;
    AudioSource sourceRef;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonRef = GetComponent<ThirdPersonCharacter>();
        sourceRef = GetComponent<AudioSource>();
        // sourceRef = gameObject.AddComponent<AudioSource>();
        // sourceRef.playOnAwake = false;
        sourceRef.clip = footStepClip;
        sourceRef.enabled = true;
        // sourceRef.outputAudioMixerGroup = mixerGroup;
        // sourceRef.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sourceRef.isPlaying && thirdPersonRef.IsGrounded)
        {
            sourceRef.Play();
        } else if (sourceRef.isPlaying && !thirdPersonRef.IsGrounded)
        {
            sourceRef.Stop();
        }
    }
}
