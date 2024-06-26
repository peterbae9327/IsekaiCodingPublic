using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider buttonSlider;

    public ClickSound sound;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnSliderBgsound);
        volumeSlider.value = AudioManager.Instance.bgSound.volume;
        buttonSlider.onValueChanged.AddListener(OnSliderSfxSound);
        buttonSlider.value = sound.sfxSound.volume;
    }

    public void OnSliderBgsound(float value)
    {
        float volume = Mathf.Clamp01(value);
        AudioManager.Instance.SetBgSoundVolume(volume);
    }

    public void OnSliderSfxSound(float value)
    {
        float volume = Mathf.Clamp01(value);
        sound.SetPlaySfxVolume(volume);
    }
}
