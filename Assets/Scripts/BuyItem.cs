using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public Text CurText;

   public void BuyItem1()
    {
        if (Money.CurMoney >= 30000)
        {
            Money.CurMoney -= 30000;
            CurText.text = Money.CurMoney.ToString();
        }
        else
        {
            //안내 메세지 띄우기
        }
    }
    public void BuyItem2()
    {
        if (Money.CurMoney >= 50000)
        {
            Money.CurMoney -= 50000;
            CurText.text = Money.CurMoney.ToString();
        }
        else
        {
            //안내 메세지 띄우기
        }
    }
    public void BuyItem3()
    {
        if (Money.CurMoney >= 100000)
        {
            Money.CurMoney -= 100000;
            CurText.text = Money.CurMoney.ToString();
        }
        else
        {
            //안내 메세지 띄우기
        }
    }
    public void BuyItem4()
    {
        if (Money.CurMoney >= 300000)
        {
            Money.CurMoney -= 300000;
            CurText.text = Money.CurMoney.ToString();
        }
        else
        {
            //안내 메세지 띄우기
        }
    }
}
