using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] Slider MusicSlider, SFXSlider;
    [SerializeField] GameObject Pause;
    [SerializeField] TMP_Text Resume, Quit;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle toggle;

    private static Resolution mainResolution;
    private bool foundResolution = false;
    private static bool opened = false;

    Resolution[] resolutions;

    public void SetVolume(float volume)
    {
        if (audioManager != null)
            audioManager.Ivolume = volume;
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    public void SetThemeVolume(float volume)
    {
        if (audioManager != null)
            audioManager.change(volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.65f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);
        
        
        resolutions = Screen.resolutions;
        if (!opened)
        {
            mainResolution = new Resolution();
            mainResolution.width = 1920;
            mainResolution.height = 1080;
            mainResolution.refreshRate = 60;
            opened = true;
        }
        toggle.isOn = Screen.fullScreen;
        qualityDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution) && !foundResolution)
            {
                currentResolutionIndex = i;
            }

            if (resolutions[i].Equals(mainResolution))
            {
                foundResolution = true;
                currentResolutionIndex = i;
            }
        }
        qualityDropdown.AddOptions(options);
        qualityDropdown.value = currentResolutionIndex;
        qualityDropdown.RefreshShownValue();
    }

    public void OnQuit()
    {
        ButtonClicked();
        Debug.Log("Quit");
        ExitQuit();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            Application.Quit();
        }
    }

    private void ButtonClicked()
    {
        AudioManager.playSound("ClickSound");
    }

    public void OnHover()
    {
        AudioManager.playSound("HoverSound");
    }

    public void OnResume()
    {
        ButtonClicked();
        Time.timeScale = 1;
        ExitResume();
        Pause.SetActive(false);
    }

    public void HoverResume()
    {
        Resume.text = "<b><i><u>Resume</u></i></b>";
    }

    public void ExitResume()
    {
        Resume.text = "<b>Resume</b>";
    }

    public void HoverQuit()
    {
        Quit.text = "<b><i><u>Quit</u></i></b>";
    }

    public void ExitQuit()
    {
        Quit.text = "<b>Quit</b>";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitQuit();
            ExitResume();
            OnResume();
        }
    }

    public void OnVisible()
    {
        Time.timeScale = 0;
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        mainResolution = resolution;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
