using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundaryScript : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)          // 물체를 안 죽이고 지나갔을 경우에도 자동으로 파괴되어야함
    {
        if(collision.tag == "Asteroid")
        {
           AsteroidScript asteroidScript =  collision.GetComponent<AsteroidScript>();
            asteroidScript.DestroyGameObject();
        }
        else if (collision.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
            enemyScript.DestroyGameObject();
        }
        else if (collision.tag == "Item")
        {
            coinScript coinScript1= collision.GetComponent<coinScript>();
            coinScript1.DestroyGameObject();
        }
        else if (collision.tag == "BossShot")
        {
            BossShotScript bossShotScript = collision.GetComponent<BossShotScript>();
            bossShotScript.DestroyGameObject();
        }
    }
}
