using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Pause;

    private SettingsMenu settings;

    private void Start()
    {
        settings = Pause.GetComponent<SettingsMenu>();
    }

    public void OnPlay()
    {
        ButtonClicked();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuit()
    {
        ButtonClicked();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnHover()
    {
        AudioManager.playSound("HoverSound");
    }

    private void ButtonClicked()
    {
        AudioManager.playSound("ClickSound");
    }

    public void OnOptions()
    {
        ButtonClicked();
        settings.OnVisible();
        Pause.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnOptions();
    }
}
