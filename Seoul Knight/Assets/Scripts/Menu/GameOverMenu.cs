using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;
    public GameObject gameOverPanel;

    void Start()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }


}
