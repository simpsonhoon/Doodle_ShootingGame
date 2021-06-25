using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    public float speed = 4;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.enemyShot.Destroy(gameObject);
    }


    
}
