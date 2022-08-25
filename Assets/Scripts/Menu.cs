using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnHover()
    {
        AudioManager.playSound("HoverSound");
    }
}
