using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private Vector2 whereToSpawn;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SpawnerObjet()
    {
        whereToSpawn = new Vector2(transform.position.x, transform.position.y);
        Instantiate(objectToSpawn, whereToSpawn, Quaternion.identity);
    }
}