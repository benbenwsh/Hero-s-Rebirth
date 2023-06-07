using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBigBlue : Collectables
{

    public override void Consume(Player player)
    {
        CreateFloatingText("CD -0.1");

        Weapon weapon = player.gameObject.GetComponentInChildren<Weapon>();
        weapon.SetReloadTime(weapon.ReloadTime * 0.9f);

        Destroy(this.gameObject);
    }
}