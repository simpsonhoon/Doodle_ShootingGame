using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;
    public GameObject playerShotPrefab;
    public GameObject asteroidPrefab;
    public List<GameObject> enemyPrefab;
    public GameObject enemyShotPrefab;
    public GameObject coinPrefab;
    public GameObject shotEffectPrefab;
    public GameObject explosionPrefab;
    public GameObject bossShotPrefab;

    public ObjectPool playerShot;
    public ObjectPool asteroid;
    public List<ObjectPool> enemies;
    public ObjectPool enemyShot;
    public ObjectPool coin;
    public ObjectPool shotEffect;
    public ObjectPool explosion;
    public ObjectPool bossShot;

    public class ObjectPool
    {
        List<GameObject> list;
        public GameObject prefab;
        public ObjectPool(GameObject prefab)
        {
            this.prefab = prefab;
            list = new List<GameObject>();      //list 생성
        }

        public GameObject Create()          //없을때 Instantiate 하고 있으면 첫번째 항목 꺼내씀
        {
            if(list.Count > 0)              // 있을때
            {
                GameObject obj = list[0];       //처음꺼를 받아옴
                obj.SetActive(true);            //활성화
                list.Remove(obj);               // 풀에서 삭제
                return obj;                     // 반환
            }
            else                                  //없을ㄸ때              
            {
                GameObject obj = Instantiate(prefab);   // 새로 생성
                obj.SetActive(true);                    
                return obj;
            }
        }
        public void Destroy(GameObject obj)     //사용하지 않을 때 숨기고 오브젝트풀에 추가
        {
            obj.SetActive(false);           //숨김
            list.Add(obj);              // 다시 object 풀에 추가
        }

    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerShot = new ObjectPool(playerShotPrefab);      
        asteroid = new ObjectPool(asteroidPrefab);
        enemies = new List<ObjectPool>();
        for(int i=0;i< enemyPrefab.Count; i++)
        {
            ObjectPool pool = new ObjectPool(enemyPrefab[i]);
            enemies.Add(pool);
        }
        enemyShot = new ObjectPool(enemyShotPrefab);
        coin = new ObjectPool(coinPrefab);
        shotEffect = new ObjectPool(shotEffectPrefab);
        explosion = new ObjectPool(explosionPrefab);
        bossShot = new ObjectPool(bossShotPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
