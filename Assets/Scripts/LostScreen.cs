using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LostScreen : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] Text highScore;
    [SerializeField] GameObject highScoreSet, Home, Quit, Replay;
    public int gameScore;
    private float currentSum, timer, duration;
    private int highScoreNum;
    private Animator animator;

    void Start()
    {
        // make highscore equal the high score...
        duration = 2f;
        highScoreNum = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = "HighScore: " + highScoreNum;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            timer += Time.deltaTime;
            if (timer > duration || currentSum == gameScore)
            {
                timer = duration;
                Home.SetActive(true);
                Quit.SetActive(true);
                Replay.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            currentSum = (timer / duration) * gameScore;
            score.text = "Score: " + currentSum.ToString("0");
            if (currentSum > highScoreNum)
            {
                if (!highScoreSet.activeInHierarchy)
                {
                    highScoreSet.SetActive(true);
                    PlayerPrefs.SetInt("HighScore", gameScore);
                }
                highScore.text = "HighScore: " + currentSum.ToString("0");
            }
        }
    }

    public void OnQuit()
    {
        ButtonClicked();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnHome() 
    {
        ButtonClicked();
        SceneManager.LoadScene(0);
    }

    public void OnReplay() 
    {
        ButtonClicked();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
