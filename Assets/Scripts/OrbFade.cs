using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OrbFade : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioClip shadow1;
    public AudioClip shadow2;
    public AudioClip shadow3;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float fadePercentage = Mathf.Clamp(GameManager.instance.PercentageLost(), 0f, 0.8f);
        // mixer.SetFloat("fadeVol", -10f + 20f * (fadePercentage / 100f));
        if (!source.isPlaying && fadePercentage > 0f)
        {
            if (source.clip == null)
                source.clip = shadow1;
            else if (source.clip == shadow1)
                source.clip = shadow2;
            else if (source.clip == shadow2)
                source.clip = shadow3;
            source.Play();
        } else if (source.isPlaying && Mathf.Approximately(fadePercentage, 0f))
        {
            source.Stop();
            source.clip = null;
        }
        // perc of 20db
        // Color c = Color.black;
        // c.a = Mathf.Clamp(GameManager.instance.PercentageLost(), 0.5f, 1f);
        // GetComponent<Image>().color = c;
        // Debug.Log($"Fade perc: {fadePercentage}");
        GetComponent<CanvasRenderer>().SetAlpha(fadePercentage);
    }
}
