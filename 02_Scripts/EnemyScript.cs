using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyShot;
    public int type = 0;
    public float hp = 3;
    public float speed = 4;
    public float coin = 0;
    public float maxShotTime;
    public float shotSpeed;
    public float time;
    public string enemyName;

    public void Init(int type, string name, float hp, float speed, float maxShotTime, float shotSpeed, float coin)
    {
        this.type = type;
        this.enemyName = name;
        this.hp = hp;
        this.speed = speed;
        this.maxShotTime = maxShotTime;
        this.shotSpeed = shotSpeed;
        this.coin = coin;
    }

    
    void Update()
    {
        time += Time.deltaTime; 
        if(time > maxShotTime)
        {
            Vector3 vec = new Vector3(transform.position.x - 0.9f, transform.position.y + 0.12f, transform.position.z);
            //GameObject shotObj = Instantiate(enemyShot, vec, Quaternion.identity);
            GameObject shotObj = ObjectPoolManager.instance.enemyShot.Create();      //enemyshot 생성
            shotObj.transform.position = vec;
            shotObj.transform.rotation = Quaternion.identity;

            EnemyShotScript shotScript = shotObj.GetComponent<EnemyShotScript>();
            shotScript.speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


  

    public void DestroyGameObject()
    {
        GameManager.instance.remainEnemy--;
        // Destroy(gameObject);
        ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
    }


}
