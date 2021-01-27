using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class spawnscript1 : MonoBehaviour
{
    //gameobject assignment-------------
    public GameObject undead1;
    public GameObject undead2;
    public GameObject undead3;
    public GameObject undead4;
    //----------------------------------
    
    //range asignment-------------------
    public int lowerx = -4;
    public int higherx = 4;
    public int lowery = -4;
    public int highery = 4;
    //----------------------------------
    
    //variable assignment---------------
    private int xpos = 1;
    private int ypos = 1;
    private int enemycount = 0;
    private int enemytype1 = 0;
    private int enemytype2 = 0;
    private int enemytype3 = 0;
    private int enemytype4 = 0;
    private int typeset = 0;
    //----------------------------------
    
    void Update()
    {
    }

    void enemyset()
    {
        //enemy number set and enemy type set
        enemycount = Random.Range(5, 15);
        for (int i = enemycount; i > 0; i--)
        {
            typeset = Random.Range(0, 4);
            if (typeset == 0)
                enemytype1 += 1;
            if (typeset == 1)
                enemytype2 += 1;
            if (typeset == 2)
                enemytype3 += 1;
            if (typeset == 3)
                enemytype4 += 1;
        }
    }

    void enemyspawn()
    {
        //spawns enemy
        for (int i = enemycount; i > 0; i--)
        {
            xpos = Random.Range(lowerx, higherx);
            ypos = Random.Range(lowery, highery);
            if (enemytype1 > 0)
            {
                var enemy = Instantiate(undead1, new Vector3(xpos, ypos, 0), Quaternion.identity) as GameObject;
                enemytype1 -= 1;
            }
            if (enemytype1 > 0)
            {
                var enemy = Instantiate(undead1, new Vector3(xpos, ypos, 0), Quaternion.identity) as GameObject;
                enemytype1 -= 1;
            }
            if (enemytype3 > 0)
            {
                var enemy = Instantiate(undead3, new Vector3(xpos, ypos, 0), Quaternion.identity) as GameObject;
                enemytype3 -= 1;
            }
            if (enemytype4 > 0)
            {
                var enemy = Instantiate(undead4, new Vector3(xpos, ypos, 0), Quaternion.identity) as GameObject;
                enemytype4 -= 1;
            }
        }
    }
}
