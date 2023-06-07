using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskRed : Collectables
{
    public override void Consume(Player player)
    {
        if (player.Hp < player.MaxHp)
        {
            CreateFloatingText("HP +1");
            player.SetHp(player.Hp + 1, player.MaxHp);
            Destroy(this.gameObject);
        }
    }
}
