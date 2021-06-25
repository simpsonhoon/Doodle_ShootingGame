using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public Button unlockButton;
    public int id;
    public Text shipNameText;
    public Text damageText;
    public Text description;
    public Image shipImage;
    public Text unlockCoinText;

    public void SetUI(string shipName, string shipDmg, string description,float unlockCoin, int locked)
    {
        this.shipNameText.text = shipName;
        this.damageText.text = shipDmg;
        this.description.text = description;
        this.unlockCoinText.text = unlockCoin.ToString();
        if(locked == 1)     //잠겨짐
        {
            unlockButton.gameObject.SetActive(true);
            unlockCoinText.gameObject.SetActive(true);
        }
        else
        {
            unlockButton.gameObject.SetActive(false);
            unlockCoinText.gameObject.SetActive(false);
        }
    }

    public void UnlockAction()
    {
        if (GameDataScript.instance.CanUnlock(id))
        {
            GameDataScript.instance.ExcuteUnlock(id);   // unlock 실행
            unlockButton.gameObject.SetActive(false);  // button obj 를 숨김
            unlockCoinText.gameObject.SetActive(false);  // text 숨김
            MenuManager.instance.coinText.text = GameDataScript.instance.GetCoin().ToString();  // 감소한 coin 값 적용

        }
       
        
    }
}
