using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game { 
    public enum Music { Menu, Play };
    public enum Sound { PlayerShot, Explosion, Coin};
    public struct ShipData
    {
        public int id;
        public float damage;
        public string name;
        public string description;
        public int locked;
        public float unlockCoin;

        public ShipData(int id, float damage, string name,  string description, float unlockCoin, int locked =1)
        {
            this.id = id;
            this.damage = damage;
            this.name = name;
            this.description = description;
            this.unlockCoin = unlockCoin;
            this.locked = locked;
        }

        public string GetImageName()
        {
            return "Character/" + id.ToString() + "/0";
        }

        public void Show()
        {
            Debug.Log("id: " + id + " damage : " + damage + " name : " + name + " description :" + description + " unlockCoin :" + unlockCoin + " locked : " + locked);
        }

        public void SetLock(int locked)
        {   
            if(id == 0)
            {
                locked = 0;
            }

            this.locked = locked;
            PlayerPrefs.SetInt("Locked" + id.ToString(), locked);
        }

        public int GetLock()
        {   
            if(id == 0) 
            {
                return 0;  // 해제가 됨
            }
            else
            {
                this.locked = PlayerPrefs.GetInt("Locked" + id.ToString(), 1);
                return this.locked;
            }
           
        }
    }

    [System.Serializable]           // 아래의 내용이 unity 에서 보이게 하는 함수
    public struct EnemyWave
    {
        public int stage;
        public int type;
        public float time;
        public EnemyWave(int stage, int type, float time)
        {
            this.stage = stage;
            this.type = type;
            this.time = time;
        }
        public void Show()
        {
            Debug.Log("stage : " + stage + " type :" + type + "time : " + time);
        }


    }

    [System.Serializable]           // 아래의 내용이 unity 에서 보이게 하는 함수
    public struct Enemy
    {
        public int type;
        public string name;
        public float hp;
        public float speed;
        public float maxShotTime;
        public float shotSpeed;
        public float coin;

        public Enemy(int type, string name, float hp, float speed ,float maxShotTime, float shotSpeed ,float coin)
        {
            this.type = type;
            this.name = name;
            this.hp = hp;
            this.speed = speed;
            this.maxShotTime = maxShotTime;
            this.shotSpeed = shotSpeed;
            this.coin = coin;
        }

        public void Show()
        {
            Debug.Log("type : " + type + " name :" + name + "hp : " + hp + " speed: " + speed   + " maxShotTime : " + maxShotTime + "shotSpeed : " + shotSpeed + "coin : " + coin);
        }


    }



}
