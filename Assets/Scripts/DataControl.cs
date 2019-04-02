using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataControl : MonoBehaviour
{
    public static int CurMoney = 0 ;
    public Text CurText;
    // Start is called before the first frame update
    void Start()
    {
        if (UnUseBlockMain.stageTitleIndex == 0)
            UnUseBlockMain.stageTitleIndex = PlayerPrefs.GetInt("stage", 1);
        //1~8탄 테스트용
        if (UnUseBlockMain.stageTitleIndex == 9)
            UnUseBlockMain.stageTitleIndex = 1;

        Debug.Log(UnUseBlockMain.stageTitleIndex);
        CurMoney = PlayerPrefs.GetInt("money",300000);
        CurText.GetComponent<Text>().text = CurMoney.ToString();
    }
    
}
