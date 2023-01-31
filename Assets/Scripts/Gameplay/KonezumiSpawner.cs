using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A konezumi spawner
/// </summary>	
public class KonezumiSpawner : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject prefabKonezumi;

    // spawn support
    Timer spawnTimerKone;
    float spawnRangeKone;

    // collision-free support
    bool retrySpawn = false;
    Vector2 spawnLocationMinKone;
    Vector2 spawnLocationMaxKone;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>	S
    void Start()
    {
        // spawn and destroy ball to calculate
        // spawn location min and max
        GameObject tempBall = Instantiate<GameObject>(prefabKonezumi);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;
        spawnLocationMinKone = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);
        spawnLocationMaxKone = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);
        Destroy(tempBall);

        // initialize and start spawn timer
        spawnRangeKone = ConfigurationUtils.MaxSpawnSeconds -
            ConfigurationUtils.MinSpawnSeconds;
        spawnTimerKone = gameObject.AddComponent<Timer>();
        spawnTimerKone.Duration = GetSpawnDelay();
        spawnTimerKone.Run();

        // spawn first ball in game
        SpawnKonezumi();
    }

    /// <summary>
	/// Update is called once per frame
	/// </summary>	
    void Update()
    {
        // spawn ball and restart timer as appropriate
        if (spawnTimerKone.Finished)
        {
            // don't stack with a spawn still pending
            retrySpawn = false;
            SpawnKonezumi();
            spawnTimerKone.Duration = GetSpawnDelay();
            spawnTimerKone.Run();
        }

        // try again if spawn still pending
        if (retrySpawn)
        {
            SpawnKonezumi();
        }
    }

    #endregion

    #region Public methods


    /// <summary>
    /// Spawns a ball
    /// </summary>
    public void SpawnKonezumi()
    {
        // make sure we don't spawn into a collision
        if (Physics2D.OverlapArea(spawnLocationMinKone, spawnLocationMaxKone) == null)
        {
            retrySpawn = false;
            Instantiate(prefabKonezumi);
        }
        else
        {
            retrySpawn = true;
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Gets the spawn delay in seconds for the next ball spawn
    /// </summary>
    /// <returns>spawn delay</returns>
    float GetSpawnDelay()
    {
        return ConfigurationUtils.MinSpawnSeconds +
            Random.value * spawnRangeKone;
    }

    #endregion
}
