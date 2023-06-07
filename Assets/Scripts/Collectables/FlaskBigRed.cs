using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBigRed : Collectables
{

    public override void Consume(Player player)
    {
        if (player.Hp < player.MaxHp)
        {
            CreateFloatingText("MAX HP");
            player.SetHp(player.MaxHp, player.MaxHp);
            Destroy(this.gameObject);
        }
    }
}