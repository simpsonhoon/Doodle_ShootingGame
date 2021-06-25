using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game;

public class GameManager : MonoBehaviour
{
    public GameObject bossObj;
    public List<GameObject> enemyWavePrefabs;
    public List<EnemyWave> enemyWaves;
    public int spawnIndex = 0;
    public float spawnTime = 0;
    public GameObject clearPanel;
    public Text stageInClearText;
    public Text coinInClearText;
    public Button nextStageButton;

    public GameObject retryPanel;
    public Text stageInRetryText;
    public Text coinInRetryText;
    public Button retryYesButton;
    public Button retryNoButton;

    public GameObject pausePanel;
    public Text coinText;
    public static GameManager instance;
    public GameObject asteroid;
    public List<GameObject> enemies;
    public float time = 0;
    
    public int spawnCount = 0;      //생성한 적
    public int spawnMax = 4;        //최대로 생상할 적
    public int remainEnemy = 0;         // 남은적
    public float coinIngame;  // 게임 내 코인
    public float maxRight;
    public bool stageClear = false;
    public int stageInGame;
    public bool isAlive = true;
    public bool bossStage = false;
    public bool bossSpawn = false;

    void Awake()            //start 전에 실행 됨
    {
        instance = this;
    }
    
    void Start()
    {
        isAlive = true;
        stageInGame = GameDataScript.instance.GetStage();
        stageClear = false;
        coinIngame = 0;
        coinText.text = GameDataScript.instance.GetCoin().ToString();       //전체 coin 가져와 UI 연결
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        retryYesButton.onClick.AddListener(retryYesAction);
        retryNoButton.onClick.AddListener(retryNoAction);
        nextStageButton.onClick.AddListener(NextStageAction);

        enemyWaves = GameDataScript.instance.GetStageWave(stageInGame);
      
        remainEnemy = 0;
        spawnIndex = 0;
        spawnTime = 0;
        bossSpawn = false;
        bossStage = false;
        if (stageInGame % 4 == 0)
        {
 
            SpawnEnemyWave();
            bossStage = true;
        }
        else
        {
            SpawnEnemyWave();
        }

        AudioManagerScript.instance.PlayMusic(Music.Play);

    }

    public void SpawnEnemyWave()
    {
        int type = enemyWaves[spawnIndex].type;
        spawnTime += enemyWaves[spawnIndex].time;
        int count = enemyWavePrefabs[type].transform.childCount;  // childCount 는 enemy waves prefab의 하위 enemies 들의 갯수
        //Instantiate(enemyWavePrefabs[type], new Vector3(maxRight + 2, Random.Range(-3.0f, 3.0f), 0), Quaternion.identity);
        Vector3 pos = new Vector3(0, Random.Range(-3.0f, 3.0f),0);

        for (int i = 0; i < count; i++)
        {
            Transform tr =  enemyWavePrefabs[type].transform.GetChild(i).transform;     //enemyWaveprefab에 있는 오브젝트 하나
            EnemyScript enemyPrefabScript = tr.GetComponent<EnemyScript>();
           int enemyType =  enemyPrefabScript.type;     //enemyWaveprefab에 있는 오브젝트 하나의 enemy type
            GameObject enemyObj =  ObjectPoolManager.instance.enemies[enemyType].Create();      // enemy Type을 받아 ObjectPoolManager에서 생성
            enemyObj.transform.position = tr.position + pos;
            enemyObj.transform.rotation = Quaternion.identity;

            EnemyScript enemyScript = enemyObj.GetComponent<EnemyScript>();
            Enemy enemy = GameDataScript.instance.enemies[enemyType];
            float cur_hp =  GameDataScript.instance.GetEnemyHp(enemy.hp, stageInGame);
            float cur_coin = GameDataScript.instance.GetEnemyCoin(enemy.coin, stageInGame);
            enemyScript.Init(enemyType, enemy.name, cur_hp, enemy.speed, enemy.maxShotTime, enemy.shotSpeed, cur_coin);
        }

        remainEnemy += count;
        spawnIndex++;
    }

    public float asteroidTime = 0;
    public float asteroidSpawnTime = 3;

    void Update()
    {
        time += Time.deltaTime;
        asteroidTime += Time.deltaTime;
        if (time > spawnTime)
        {   
            if(bossStage == true)
            {
                if(bossSpawn == false)
                {
                    remainEnemy++;
                    GameObject boss = Instantiate(bossObj, new Vector3(10, 0, 0), Quaternion.identity);
                    BossScript bossScript= boss.GetComponent<BossScript>();
                    float hp = GameDataScript.instance.GetBossHp(stageInGame);
                    float coin = GameDataScript.instance.GetBossCoin(stageInGame);
                    bossScript.Init(hp, coin);
                    bossSpawn = true;
                }
                if (remainEnemy <= 0 && stageClear == false && isAlive == true)     // 적 없고 플레이어 살아있으면 스테이지 클리어
                {
                    stageClear = true;
                    ClearPanelSetActiveAfter1Sec();
                }
            }
            else if(spawnIndex < enemyWaves.Count) // 적 생성 
            {
                // 1회 spawn 로직
                SpawnEnemyWave();
            }
            else
            {
                if (remainEnemy <= 0 && stageClear == false && isAlive == true)     // 적 없고 플레이어 살아있으면 스테이지 클리어
                {
                    stageClear = true;
                    ClearPanelSetActiveAfter1Sec();
                }
            }
        }
        else if( asteroidTime > asteroidSpawnTime && spawnIndex < enemyWaves.Count)    
        {
            GameObject obj = ObjectPoolManager.instance.asteroid.Create();
            obj.transform.position = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
            obj.transform.rotation = Quaternion.identity;
            AsteroidScript asteroidScript = obj.GetComponent<AsteroidScript>();
            float hp =  GameDataScript.instance.GetAsteroidHp(stageInGame);
            float coin = GameDataScript.instance.GetAsteroidCoin(stageInGame);
            asteroidScript.Init(hp, coin);
            asteroidTime = 0;
        }

    }



    public void PauseAction()
    {
        Time.timeScale = 0;     //game pause
        pausePanel.SetActive(true);
    }

    public void ResumeAction()
    {
        Time.timeScale = 1;     //game resume
        pausePanel.SetActive(false);
    }

    public void MainMenuAction()
    {
        Time.timeScale = 1;     
        pausePanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }


    void retryYesAction()
    {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }


    public void retryNoAction()
    {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }


 
    void NextStageAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void RetryPanelSetActiveAfter1Sec()
    {
        Invoke("RetryPanelSetActive", 1);
    }


    public void RetryPanelSetActive()
    {
        stageInRetryText.text = stageInGame.ToString();
        coinInRetryText.text = coinIngame.ToString();
        retryPanel.SetActive(true);
    }


    public void ClearPanelSetActiveAfter1Sec()
    {
        GameDataScript.instance.AddStage();
        Invoke("ClearPanelSetActive", 1);       //1초 후에 적용해야 문제 없음
    }


    public void ClearPanelSetActive()           //stage clear panel set
    {
        stageInClearText.text = stageInGame.ToString();
        coinInClearText.text = coinIngame.ToString();
        clearPanel.SetActive(true);
    }


}
