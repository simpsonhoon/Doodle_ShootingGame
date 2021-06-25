using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class PlayerScript : MonoBehaviour
{
    public GameObject shot;
    public GameObject explosion;
    public float speed = 5;
    Vector3 min, max;
    Vector2 colSize;
    Vector2 chrSize;
    public float damage;
    public SpriteRenderer spr;
    int select;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    void Start()
    {
       

        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //min = new Vector3(-8, -4.5f, 0);
        //max = new Vector3(8, 4.5f, 0);
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        colSize = GetComponent<BoxCollider2D>().size;
        chrSize = new Vector2(colSize.x / 2, colSize.y / 2);

        select = GameDataScript.instance.select;            // 선택된 캐릭터의 데미지 값을 받아와서 적용
        ShipData shipData = GameDataScript.instance.ships[select];
        damage = shipData.damage;

        spr = GetComponent<SpriteRenderer>();

        spr.sprite = Resources.Load<Sprite>(shipData.GetImageName());
       

    }

    void Update()
    {
        Move();
        PlayerShot();
    }


    public float shotMax = 0.5f;
    public float shotDelay = 0;

    void PlayerShot()       
    {
        shotDelay += Time.deltaTime;
            if (shotDelay > shotMax)
            {
                shotDelay = 0;
                Vector3 vec = new Vector3(transform.position.x + 1.4f, transform.position.y + 0.12f, transform.position.z);
                //GameObject shotObj = Instantiate(shot, vec, Quaternion.identity);   //샷 생성
                GameObject shotObj = ObjectPoolManager.instance.playerShot.Create();        //샷 생성
   
                shotObj.transform.position = vec;
                shotObj.transform.rotation = Quaternion.identity;
                ShotScript shotScript = shotObj.GetComponent<ShotScript>(); 
                shotScript.damage = damage;   // 선택된 캐릭터의 데미지 값을 샷에도 적용
                AudioManagerScript.instance.PlaySound(Sound.PlayerShot);     
            }
        
    }

    void Move()       // 캐릭터 화면 벗어나지 않게 위치 지정
    {
        float newX;
        float newY;
        if (Application.platform == RuntimePlatform.Android)        //android 에서
        {
            float x = SimpleInput.GetAxisRaw(horizontalAxis);
            float y = SimpleInput.GetAxisRaw(verticalAxis);
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
             newX = transform.position.x;
             newY = transform.position.y;

        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
            newX = transform.position.x;
            newY = transform.position.y;
        }

 

        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x - 0.2f);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);
        
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            coinScript coinScript1 = collision.gameObject.GetComponent<coinScript>();   //coin 추가
            GameManager.instance.coinIngame += coinScript1.coinSize;
            GameDataScript.instance.AddCoin(coinScript1.coinSize);
            GameManager.instance.coinText.text = GameDataScript.instance.GetCoin().ToString();   // coin UI 추가 작업
            //Destroy(collision.gameObject);
            coinScript1.DestroyGameObject();
            AudioManagerScript.instance.PlaySound(Sound.Coin);
        }
        else if(collision.gameObject.tag == "Asteroid")    //플레이어 파괴
        {
            GameManager.instance.isAlive = false;       //죽음
            //Destroy(collision.gameObject);          
            ObjectPoolManager.instance.asteroid.Destroy(collision.gameObject);   // Asteroid 파괴
            Destroy(gameObject);                 //플레이어 파괴
            Instantiate(explosion, transform.position, Quaternion.identity);
            
            GameManager.instance.RetryPanelSetActive();
            AudioManagerScript.instance.PlaySound(Sound.Explosion);
        }
        else if ( collision.gameObject.tag == "Enemy")  //플레이어 파괴
        {
            GameManager.instance.isAlive = false;       //죽음
            //Destroy(collision.gameObject);         
            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();    
            enemyScript.DestroyGameObject();                // enemy 파괴
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManagerScript.instance.PlaySound(Sound.Explosion);
            GameManager.instance.RetryPanelSetActive();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            GameManager.instance.isAlive = false;       //죽음
            //Destroy(collision.gameObject);    
            EnemyShotScript enemyShotScript =  collision.GetComponent<EnemyShotScript>();
            enemyShotScript.DestroyGameObject();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManagerScript.instance.PlaySound(Sound.Explosion);
            GameManager.instance.RetryPanelSetActive();
        }
        else if (collision.gameObject.tag == "BossShot")
        {
            GameManager.instance.isAlive = false;       //죽음
            //Destroy(collision.gameObject);    
            BossShotScript bossShotScript = collision.GetComponent<BossShotScript>();
            bossShotScript.DestroyGameObject();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManagerScript.instance.PlaySound(Sound.Explosion);
            GameManager.instance.RetryPanelSetActive();
        }
        else if (collision.gameObject.tag == "Boss")  //플레이어 파괴
        {
            GameManager.instance.isAlive = false;       //죽음
            //Destroy(collision.gameObject);         
            BossScript bossScript = collision.GetComponent<BossScript>();
            bossScript.DestroyGameObject();                // enemy 파괴
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);        //폭파효과
            AudioManagerScript.instance.PlaySound(Sound.Explosion);
            GameManager.instance.RetryPanelSetActive();
        }
    }


}
