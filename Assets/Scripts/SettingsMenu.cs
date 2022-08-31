using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    public void SetVolume(float volume)
    {
        audioManager.volume = volume;
    }

    public void Start()
    {
        SetVolume(1f);
    }
}
