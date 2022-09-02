using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] Slider MusicSlider, SFXSlider;

    public void SetVolume(float volume)
    {
        audioManager.Ivolume = volume;
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    public void SetThemeVolume(float volume)
    {
        audioManager.change(volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    private void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);
    }
}
