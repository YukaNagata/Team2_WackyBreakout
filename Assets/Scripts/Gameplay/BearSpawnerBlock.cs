using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpawnerBlock : MonoBehaviour
{
    #region Field

    [SerializeField]
    GameObject prefabTeddyBear;

    //teddy bear spawn location support
    const int SpawnBorderSize = 100;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;

    #endregion

    

    // Start is called before the first frame update
    void Start()
    {
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Destroys the bear spawner block on collision with a ball
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }

        // spawn bear
        // generate random location
        Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX),
            Random.Range(minSpawnY, maxSpawnY),
            -Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);

        // spawn random teddy bear type at location
        GameObject teddyBear;
            Instantiate(prefabTeddyBear,worldLocation, Quaternion.identity);
    }
}
