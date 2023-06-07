using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int hp;
    public int maxHp;

    public GameData(Player player)
    {
        level = LevelManager.instance.Level;
        hp = player.Hp;
        maxHp = player.MaxHp;
    }
}
