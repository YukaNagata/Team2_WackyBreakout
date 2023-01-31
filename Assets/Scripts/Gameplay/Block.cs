using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A block
/// </summary>	
public class Block : MonoBehaviour
{
    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>	
    void Start()
    {

    }

    /// <summary>
	/// Update is called once per frame
	/// </summary>	
    void Update()
    {
        
    }

    private Vector3 speed = new Vector3(20, 20, 20);

    /// <summary>
    /// Destroys the block on collision with a ball
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {

            GetComponent<Renderer>().material.color
                = new Color(Random.value, Random.value, Random.value, 1.0f);

            this.transform.localScale -= speed * Time.deltaTime;

            if (this.transform.localScale.x < 0)
            
                Destroy(gameObject);

        }
    }

    #endregion
}
