using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotSpeed = 30;
    public float speed = 5;
    public float hp = 4;
    public float coin = 2;
   public void Init(float hp, float coin)
    {
        this.hp = hp;
        this.coin = coin;
    }

    void Update()
    {
       
        transform.Translate(Vector3.left * speed * Time.deltaTime,Space.World);
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));
    }

   
    public void DestroyGameObject()
    {
        //GameManager.instance.remainEnemy--;
        //Destroy(gameObject);
        ObjectPoolManager.instance.asteroid.Destroy(gameObject);
    }

}
