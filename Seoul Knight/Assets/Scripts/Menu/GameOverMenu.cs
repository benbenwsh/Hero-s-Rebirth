using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
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
