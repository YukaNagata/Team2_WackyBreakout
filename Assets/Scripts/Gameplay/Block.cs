using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A block
/// </summary>	
public class Block : MonoBehaviour
{
    #region Fields

    //[SerializeField]
    ////GameObject prefabExplosion;
    ////爆発エフェクト
    //public GameObject prefabExplosion;
    
    #endregion


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
       //prefabExplosion.SetActive(true);
    }

    /// <summary>
    /// Destroys the block on collision with a ball
    /// </summary>
    /// <param name="coll">Coll.</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.CompareTag("Ball"))
        {
            //Instantiate(explosion_prefab, transform.position, Quaternion.identity);
   
            Destroy(gameObject);
            //Debug.Log("debug comment");
            //エフェクトを発生させる
            //GenerateEffect();
        }
    }

    ////エフェクトを生成する
    //void GenerateEffect()
    //{
    //    //エフェクトを生成する
    //    GameObject effect = Instantiate(prefabExplosion) as GameObject;
    //    //エフェクトが発生する場所を決定する
    //    effect.transform.position = gameObject.transform.position;
    //}

    #endregion
}
