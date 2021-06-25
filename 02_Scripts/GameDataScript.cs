using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


public class GameDataScript : MonoBehaviour
{
    TextAsset shipTextAsset;
    TextAsset enemyWaveTextAsset;
    TextAsset enemyTextAsset;

    public static GameDataScript instance;
    public ShipData[] ships;
    public EnemyWave[] enemyWaves;
    public Enemy[] enemies;
    public float coin;  // 전체 코인
    private int stage;
    private int _select;
    public int select       //select 변수에 값 읽어오고 저장
    {
        get
        {
            _select = PlayerPrefs.GetInt("ChrSelect", 0);
            return _select;
        }
        set
        {
            _select = value;
            PlayerPrefs.SetInt("ChrSelect", _select);
        }
    }

    public int GetStage()
    {
        stage = PlayerPrefs.GetInt("Stage", 1);
        return stage;
    }

    public void AddStage()
    {   
        stage = PlayerPrefs.GetInt("Stage", 1);
        stage++;
        PlayerPrefs.SetInt("Stage", stage);
    }

    private void Awake()
    {
        if(instance == null)            // singleton 처리
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        shipTextAsset = Resources.Load<TextAsset>("chr");
        string[] lines = shipTextAsset.text.Split('\n');
        ships = new ShipData[lines.Length - 2];

        for (int i = 1; i < lines.Length - 2; i++)
        {
            string[] rows = lines[i].Split('\t');
            int id = int.Parse(rows[0]);
            float damage = float.Parse(rows[1]);
            string name = rows[2];
            string description = rows[3];
            float unlockCoin = float.Parse(rows[4]);
            int locked;
            if (i == 1)
            {
                locked = 0;  //첫 캐릭터는 안 잠궈놓음
            }
            else
            {
                locked = PlayerPrefs.GetInt("Locked" + (i - 1).ToString(), 1);    // 그 다음 캐릭터들은 잠궈놓음
            }
            ships[i - 1] = new ShipData(id, damage, name, description, unlockCoin, locked);

           
        }
        LoadEnemy();
        LoadEnemyWave();   

    }

    public void LoadEnemyWave()
    {

        enemyWaveTextAsset = Resources.Load<TextAsset>("enemyWave");
        string[] lines = enemyWaveTextAsset.text.Split('\n');
        enemyWaves = new EnemyWave[lines.Length - 2];


        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int stage = int.Parse(rows[0]);
            int type = int.Parse(rows[1]);
            float time = float.Parse(rows[2]);

            enemyWaves[i - 1] = new EnemyWave(stage, type, time);

        }

        
    }

    public void LoadEnemy()
    {
        enemyTextAsset = Resources.Load<TextAsset>("enemy");
        string[] lines = enemyTextAsset.text.Split('\n');
        enemies = new Enemy[lines.Length - 2];

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int type = int.Parse(rows[0]);
            string name = rows[1];
            float hp = float.Parse(rows[2]);
            float speed = float.Parse(rows[3]);
            float maxShotTime = float.Parse(rows[4]);
            float shotSpeed = float.Parse(rows[5]);
            float coin = float.Parse(rows[6]);


            enemies[i - 1] = new Enemy(type, name, hp, speed, maxShotTime , shotSpeed, coin);

        }


    }

    public float GetCoin()          
    {
        this.coin =  PlayerPrefs.GetFloat("TotalCoin", 0);
        return this.coin;
    }

    public void AddCoinInMenu(float coin)     //coin 추가     //mainmenu 에서
    {
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        MenuManager.instance.coinImage.gameObject.SetActive(true);
        MenuManager.instance.coinText.gameObject.SetActive(true);
    }

    public void AddCoin(float coin)     //coin 추가 // playerscript 에서
    {
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
    }


    public bool CanUnlock(int id)
    {
        if(GetCoin() > ships[id].unlockCoin)
        {
            if (ships[id].GetLock() == 1)       //해제 할 수 있음
            {
                return true;
            }
            else
            {
                print("already unlock");
                return false;
            }
        }
        else
        {
            print("No coin");
            return false;
        }
    }

    public void ExcuteUnlock(int id)        // unlock 실행
    {
        
        AddCoinInMenu(-ships[id].unlockCoin);
        ships[id].SetLock(0);
    }

    public List<EnemyWave> GetStageWave(int stage)
    {
        List<EnemyWave> list = new List<EnemyWave>();
        for(int i = 0; i < enemyWaves.Length; i++)
        {
            if(enemyWaves[i].stage == stage)
            {
                list.Add(enemyWaves[i]);
            }
        }
        if(list.Count == 0)
        {
            for (int i = 0; i < enemyWaves.Length; i++)
            {
                if (enemyWaves[i].stage >=3 )
                {
                    list.Add(enemyWaves[i]);
                }
            }
        }

        return list;
    }

    public float GetEnemyHp(float base_hp,int stage)      
    {
        return base_hp * stage / 2;
    }

    public float GetEnemyCoin(float base_coin, int stage)
    {
        return base_coin * stage;
    }

    public float GetAsteroidHp(int stage)
    {
        return 1 * stage;
    }

    public float GetAsteroidCoin(int stage)
    {
        return 2 * stage;
    }

    public float GetBossHp( int stage)
    {
        return 15 * stage;
    }

    public float GetBossCoin(int stage)
    {
        return 20 * stage;
    }

    public bool CanSelect()
    {
        if (ships[select].GetLock() == 0)
        {
            return true;
        }
        else return false;
    }


}
