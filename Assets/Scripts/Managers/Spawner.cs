using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    Vector2 whereToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnerObjet()
    {
        whereToSpawn = new Vector2(transform.position.x, transform.position.y);
        Instantiate(objectToSpawn, whereToSpawn, Quaternion.identity);
    }
}
