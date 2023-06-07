using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;

    void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
