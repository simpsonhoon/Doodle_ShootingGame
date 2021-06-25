using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class ShotScript : MonoBehaviour
{
    public GameObject shotEffect;
    public GameObject coin;
    public GameObject explosion;
    public float speed = 10;
    public float damage;
    public SpriteRenderer spr2;
    public int select;
    void Start()
    {
      select = GameDataScript.instance.select;
    }
    void Update()
    {

       
        if (select == 2)
        {
            spr2 = GetComponent<SpriteRenderer>();
            spr2.sprite = Resources.Load<Sprite>("Character/2/2");
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);        //총알 오른쪽으로 이동
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Asteroid")
        {
            AsteroidScript asteroidScript = collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= damage;
            // Instantiate(shotEffect, transform.position, Quaternion.identity); 
            GameObject shotEffectObj  = ObjectPoolManager.instance.shotEffect.Create();    // shot effect 생성
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (asteroidScript.hp <= 0)
            {
                //Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj  = ObjectPoolManager.instance.explosion.Create();       // explosion effect 생성
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                // GameObject coinObj =  Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();            //coin 생성
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinScript coinScript1 = coinObj.GetComponent<coinScript>();
                coinScript1.coinSize = asteroidScript.coin;
                //Destroy(collision.gameObject);
                asteroidScript.DestroyGameObject();         // asteroid 삭제
                AudioManagerScript.instance.PlaySound(Sound.Explosion);
            }
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);       // playershot 삭제
        }
        else if (collision.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= damage;
            //Instantiate(shotEffect, transform.position, Quaternion.identity); 
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();    // shot effect 생성
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (enemyScript.hp <= 0)
            {
                //  Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();       // explosion effect 생성
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity); 
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();           //coin 생성
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinScript coinScript1 = coinObj.GetComponent<coinScript>();
                coinScript1.coinSize = enemyScript.coin;
                //Destroy(collision.gameObject);
                enemyScript.DestroyGameObject();        // enemy 삭제
                AudioManagerScript.instance.PlaySound(Sound.Explosion);
            }
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);      // playershot 삭제
        }
        else if (collision.tag == "Boss")
        {
            BossScript bossScript = collision.gameObject.GetComponent<BossScript>();
            bossScript.hp -= damage;
            //Instantiate(shotEffect, transform.position, Quaternion.identity); 
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();    // shot effect 생성
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (bossScript.hp <= 0)
            {
                //  Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();       // explosion effect 생성
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity); 
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();           //coin 생성
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinScript coinScript1 = coinObj.GetComponent<coinScript>();
                coinScript1.coinSize = bossScript.coin;
                //Destroy(collision.gameObject);
                bossScript.DestroyGameObject();        // boss 삭제
                AudioManagerScript.instance.PlaySound(Sound.Explosion);
            }
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);      // playershot 삭제
        }
    }

    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }


}
