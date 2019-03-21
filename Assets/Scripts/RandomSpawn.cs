using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    //Floor position coordinates, x: -7.3, Y: 64.3, Z: -102.5
    //corner -4, 4, 4                           corner 4, 4, 4
    //
    //
    //corner 4, 4, -4                           corner -4, 4, 4
    public GameObject pullo_longneck;
    public List<GameObject> spawnObjects;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int counter = 0;
        while (counter < 100)
        {
            Instantiate(pullo_longneck, GeneratePositions(), Quaternion.identity);
            counter++;
        }     
    }

    Vector3 GeneratePositions()
    {
        return new Vector3(Random.Range(-4f, 4f), 4f, Random.Range(-4f, 4f));
    }
}
