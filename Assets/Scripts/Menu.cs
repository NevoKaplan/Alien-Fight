using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
}
