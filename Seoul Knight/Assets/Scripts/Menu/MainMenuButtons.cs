using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }

    public void CharacterCustomisation()
    {
        SceneManager.LoadScene(2);
    }

    public void Story()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
