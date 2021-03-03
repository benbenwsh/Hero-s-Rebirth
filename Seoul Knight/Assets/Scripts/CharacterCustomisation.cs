using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCustomisation : MonoBehaviour
{
    public static GameObject[] broughtWeapons = new GameObject[2];

    public GameObject duelSword;
    public GameObject stormbreaker;

    private void Start()
    {
        broughtWeapons[0] = duelSword;
        broughtWeapons[1] = stormbreaker;
    }

    public void Back()
    {
        SceneManager.LoadScene(1);
    }
}
