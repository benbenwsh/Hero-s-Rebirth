using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: MonoBehaviour
{
    [SerializeField] private LevelLabel levelLabel;

    public static LevelManager instance;

    public int Level { get; private set; }



    private void Awake()
    {
        instance = this;
        LoadGameData();
    }



    private void Start()
    {
        
        StartCoroutine(levelLabel.Animate(Level));
    }



    public void NextLevel(Player player)
    {
        Level++;
        SaveSystem.SaveGameData(player);
        GameOverMenu.instance.PlayAgain();
    }



    private void LoadGameData()
    {
        GameData data = SaveSystem.LoadGameData();

        if (data == null)
        {
            Level = 0;
        }
        else
        {
            Level = data.level;
        }
    }
}
