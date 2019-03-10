using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static int CurMoney = -1;
    public Text CurText;

    void Awake()
    {
        //가장 처음
        if (CurMoney == -1)
        {

            CurMoney = 400000;

            CurText.GetComponent<Text>().text = CurMoney.ToString();
        }
        //가장 처음은 아닌경우
        else
        {
            CurText.GetComponent<Text>().text = CurMoney.ToString();
        }
    }
}
