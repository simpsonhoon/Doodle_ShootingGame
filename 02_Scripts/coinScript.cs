using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    public float speed = 1.5f;
    public float coinSize = 1;
    Transform playerTr;



    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }


    void Update()
    {
        if (playerTr == null)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerTr.position, Time.deltaTime * 3);
        }
       
    }
    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.coin.Destroy(gameObject);
    }



}
