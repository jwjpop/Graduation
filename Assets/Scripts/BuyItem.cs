using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private GameObject panelNoMoney;
    public Text CurText;

    void Start()
    {
        CurText.text = DataControl.CurMoney.ToString();
        panelNoMoney = GameObject.Find("PanelNoMoney");
        panelNoMoney.SetActive(false);
    }

   public void BuyItem1()
    {
        if (DataControl.CurMoney >= 1)
        {
            DataControl.CurMoney -= 1;
            PlayerPrefs.SetInt("money", DataControl.CurMoney);
            PlayerPrefs.Save();
            CurText.text = DataControl.CurMoney.ToString();
        }
        else
        {
            panelNoMoney.SetActive(true);
        }
    }
    public void BuyItem2()
    {
        if (DataControl.CurMoney >= 2)
        {
            DataControl.CurMoney -= 2;
            PlayerPrefs.SetInt("money", DataControl.CurMoney);
            PlayerPrefs.Save();
            CurText.text = DataControl.CurMoney.ToString();
        }
        else
        {
            panelNoMoney.SetActive(true);
        }
    }
    public void BuyItem3()
    {
        if (DataControl.CurMoney >= 3)
        {
            DataControl.CurMoney -= 3;
            PlayerPrefs.SetInt("money", DataControl.CurMoney);
            PlayerPrefs.Save();
            CurText.text = DataControl.CurMoney.ToString();
        }
        else
        {
            panelNoMoney.SetActive(true);
        }
    }
    public void BuyItem4()
    {
        if (DataControl.CurMoney >= 5)
        {
            DataControl.CurMoney -= 5;
            PlayerPrefs.SetInt("money", DataControl.CurMoney);
            PlayerPrefs.Save();
            CurText.text = DataControl.CurMoney.ToString();
        }
        else
        {
            panelNoMoney.SetActive(true);
        }
    }

    public void NoMoneyOk()
    {
        panelNoMoney.SetActive(false);
    }
}
