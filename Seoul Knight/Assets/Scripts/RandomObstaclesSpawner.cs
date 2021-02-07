using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObstaclesSpawner : MonoBehaviour
{
    public int noOfObstacles;
    public List<GameObject> spawnPool; //allocates prefabs 
    public GameObject quad; //bounds

    void Start()
    {
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 pos;

        for (int i = 0; i < noOfObstacles; i++)
        {
            int randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }

    //private void destroyObjects()
    //{
    //    foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
    //    {
    //        Destroy(o);
    //    }
    //}

    //attach your c# script to the spawner object
    //For our settings we will make the number to spawn as whatever you want. The size is the number of different objects that can be generated
    //to get it to spawn in different rooms click on your rooms in your hierarchy and go click on the plus in the inspector
    //Drag your spawner into the slot and from the drop down select spawnObjects
    //


}

