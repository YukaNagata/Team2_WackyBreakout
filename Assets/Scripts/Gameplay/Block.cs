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
    ////�����G�t�F�N�g
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
            //�G�t�F�N�g�𔭐�������
            //GenerateEffect();
        }
    }

    ////�G�t�F�N�g�𐶐�����
    //void GenerateEffect()
    //{
    //    //�G�t�F�N�g�𐶐�����
    //    GameObject effect = Instantiate(prefabExplosion) as GameObject;
    //    //�G�t�F�N�g����������ꏊ�����肷��
    //    effect.transform.position = gameObject.transform.position;
    //}

    #endregion
}
