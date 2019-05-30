using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataControl : MonoBehaviour
{
    //돈 관리
    public static int CurMoney = 0 ;
    public Text gradeAll;
    //어디에서 접근하는지 나타냄
    public static int where = 0;

    public Text CurText;
    public Text TutoText;
    
    private GameObject arrow;
    private GameObject panelTuto;
    private GameObject buttonUnderLeft;
    private GameObject buttonUnderRight;
    private GameObject buttonHistory;

    float grades;
    // Start is called before the first frame update
    void Start()
    {
        //UnUseBlockMain에서 0으로 초기화
        //처음 켜졌으면
        if (UnUseBlockMain.stageTitleIndex == 0)
            UnUseBlockMain.stageTitleIndex = PlayerPrefs.GetInt("stage", 1);
        //1~8탄 테스트용
        if (UnUseBlockMain.stageTitleIndex == 9)
            UnUseBlockMain.stageTitleIndex = 1;

        Debug.Log(UnUseBlockMain.stageTitleIndex);
        CurMoney = PlayerPrefs.GetInt("money",300000);
        CurText.GetComponent<Text>().text = CurMoney.ToString();

        buttonUnderLeft = GameObject.Find("ButtonUnderLeft");
        buttonUnderLeft.GetComponent<Button>().interactable = false;
        buttonUnderRight = GameObject.Find("ButtonUnderRight");
        arrow = GameObject.Find("Arrow");
        arrow.SetActive(false);

        buttonHistory = GameObject.Find("ButtonTopHistory");
        if (UnUseBlockMain.stageTitleIndex == 1)
        {
            buttonHistory.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonHistory.GetComponent<Button>().interactable = true;
        }
        panelTuto = GameObject.Find("PanelTuto");
        if (UnUseBlockMain.stageTitleIndex != 1)
        {
            panelTuto.SetActive(false);
        }
        
        PlayerPrefs.SetInt("story", 1);
        PlayerPrefs.Save();

        for (int i = 1; i <= UnUseBlockMain.stageTitleIndex; i++)
        {
            grades += PlayerPrefs.GetFloat("grades" + i, 4.5f);
        }
        gradeAll.text = Convert.ToString(grades/UnUseBlockMain.stageTitleIndex);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 테스트임 터치로 바꿔야함
        {
            if(UnUseBlockMain.stageTitleIndex==1)
            {
                TutoText.text = "이제 OT도 다녀왔고 \n 첫 수업을 들으러 학교에 가볼까?";
                StartCoroutine(ShowClick());
                buttonUnderRight.transform.SetAsLastSibling();
            }
        }
    }

    public void gradeReset()
    {
        for(int i=1;i<=8;i++)
        {
            PlayerPrefs.SetFloat("grades" + i, 4.5f);
        }
        PlayerPrefs.Save();
    }

    public void storyReset()
    {
        PlayerPrefs.SetInt("story", 0);
        PlayerPrefs.Save();
    }

    public void moneyReset()
    {
        CurMoney = 0;
        PlayerPrefs.SetInt("money", CurMoney);
        PlayerPrefs.Save();
        CurText.GetComponent<Text>().text = CurMoney.ToString();
    }

    public void stageReset()
    {
        UnUseBlockMain.stageTitleIndex = 1;
        PlayerPrefs.SetInt("stage", UnUseBlockMain.stageTitleIndex);
        PlayerPrefs.Save();
        Debug.Log(UnUseBlockMain.stageTitleIndex);
    }

    public void whereControl()
    {
        where = 0;
    }
    IEnumerator ShowClick()
    {
        while (true)
        {
            arrow.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            arrow.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
