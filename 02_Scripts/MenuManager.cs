using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game;


public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public static MenuManager instance;
    public GameObject item;
    public GameObject content;
    public GameObject addButtonObj;
    public GameObject clearButtonObj;
    public Text coinText;
    public Image coinImage;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startButton.onClick.AddListener(GoGameScene);
        int shipLength = GameDataScript.instance.ships.Length;
        for(int i = 0; i < shipLength-1; i++)
        {
            ShipData ship = GameDataScript.instance.ships[i];  // shipData 의  ship 구조체 하나를 가져옴
            GameObject obj = Instantiate(item, transform.position, Quaternion.identity);    // item 생성
            MenuItemScript curItem = obj.GetComponent<MenuItemScript>();

            curItem.SetUI(ship.name, ship.damage.ToString(), ship.description, ship.unlockCoin, ship.locked);  // SetUI
            curItem.id = ship.id;                               // Set id

            obj.name = i.ToString();
            obj.transform.SetParent(content.transform, false);      //   현재 obj의 부모를 설정. 컨텐트로 obj 를 삽입하는 작업
            obj.SetActive(true);
            curItem.shipImage.sprite = Resources.Load<Sprite>(ship.GetImageName());

            GetComponent<ScrollViewSnap>().item.Add(obj);
        }
        coinText.text = GameDataScript.instance.GetCoin().ToString();
        AudioManagerScript.instance.PlayMusic(Music.Menu);
    }

    public void AddTestCoin()
    {
        GameDataScript.instance.AddCoinInMenu(10000);
        coinText.text = GameDataScript.instance.GetCoin().ToString();   // UI에도 coin 변경 사항 적용 처리
    }

    public void ClearPrefAction()
    {
        PlayerPrefs.DeleteAll();
    }



   public void GoGameScene()
    {
        if (GameDataScript.instance.CanSelect())
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            GameDataScript.instance.select = 0;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


}
