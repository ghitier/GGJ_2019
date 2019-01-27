using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField] private AudioClip[] music = { };
    private AudioSource musicSource;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        AudioSource[] sources = GetComponents<AudioSource>();
        musicSource = sources[0];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMusic(int roomNumber = 1)
    {
        musicSource.clip = music[roomNumber - 1];
        musicSource.Play();
    }
}
