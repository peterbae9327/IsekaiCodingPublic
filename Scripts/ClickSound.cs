using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource sfxSound;
    public AudioClip buttonClickSound;

    private void Awake()
    {
        sfxSound = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySfx()
    {
        sfxSound.PlayOneShot(buttonClickSound);
    }

    public void SetPlaySfxVolume(float volume)
    {
        sfxSound.volume = volume;
    }
}
