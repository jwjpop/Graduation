using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeforeCodingScript : MonoBehaviour
{
    // 돈
    public Text CurText;
    //패널 위의 텍스트
    public Text txtScript;
    //말풍선 텍스트
    public Text txtBubble;
    //텍스트가 깔릴 패널
    GameObject beforePanel;
    //말풍선
    GameObject bubble;
    //위쪽 설명 이미지
    GameObject stageImage;
    //아래쪽 코딩 이미지
    GameObject codingImage;
    
    int stage = 0;
    int scriptStartIndex = 0;
    int i;

    //1~8번 인덱스에 스크립트가 시작되는 부분을 넣음
    //그래서 /를 만나면 종료
    //인덱스 대거 수정해야함
    string[] script = { "0번","9","15","16","17",
                        "18","19","20","21",
                        "친구를 만나면 인사를 해야돼", "말을 하기 위해선 print를 사용해보자!","추가된 블록을 누르면 함수 내부로 들어갈 수 있어!","말을 적을 수 있는 공간이 필요해",
                        "추가된 블록을 누르면 내용을 채울 수 있어","내용을 입력해줘","수정을 눌러야 반영이 돼","","그럼 친구에게 인사를 해보자!","/",
                        "2탄 시작",
                        "3탄 시작",
                        "4탄 시작",
                        "5탄 시작",
                        "6탄 시작",
                        "7탄 시작",
                        "8탄 시작"};

    void Start()
    {
        if (DataControl.where == 1)
        {
            //현재 스테이지 받아옴
            stage = ReviewScript.clickedStage;
            //스크립트에 사용할 인덱스로 변환
            scriptStartIndex = Convert.ToInt32(script[stage]);
            //스크립트 시작
            txtScript.text = script[scriptStartIndex];
        }
        else
        {
            stage = UnUseBlockMain.stageTitleIndex;
            scriptStartIndex = Convert.ToInt32(script[stage]);
            txtScript.text = script[scriptStartIndex];
        }


        CurText.text = DataControl.CurMoney.ToString();
        beforePanel = GameObject.Find("BeforePanel");
        bubble = GameObject.Find("Bubble");
        bubble.SetActive(false);
        stageImage = GameObject.Find("StageImage");
        codingImage = GameObject.Find("CodingImage");
        codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + 1) as Sprite;
        i = 2;
        ImageBinding();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 테스트임 터치로 바꿔야함
        {
            ScriptChage();
        }
    }
    void ImageBinding()
    {
        if(stage == 1)
        {
            //1탄 설명에 사용할 이미지 적용
            stageImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("BeforeStage1") as Sprite;
        }
        else if (stage == 2)
        {
            //1탄 설명에 사용할 이미지 적용
            stageImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("BeforeStage2") as Sprite;
        }
    }
    //스테이지별 모든 이미지, 스크립트 변화가 이곳에서 이루어짐
    void ScriptChage()
    {
        scriptStartIndex++;
        if (stage == 1)
        {
            if (script[scriptStartIndex].Equals("/"))
            {
                if(DataControl.where==1)
                {
                    SceneManager.LoadScene("ReviewScene");

                }
                else
                {
                    SceneManager.LoadScene("CodingScene");
                }
            }
            else
            {
                 if (script[scriptStartIndex].Equals(""))
                 {
                     beforePanel.SetActive(false);
                     txtBubble.text = "Hello, World!";
                     bubble.SetActive(true);
                 }
                 else
                 {
                     beforePanel.SetActive(true);
                     bubble.SetActive(false);
                 }

                 //지금 조잡함
                if (i == 9)
                    i=1;
                codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_"+stage +"_"+i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if(stage == 2)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 3)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 4)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 5)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 6)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 7)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
        else if (stage == 8)
        {
            if (DataControl.where == 1)
            {
                SceneManager.LoadScene("ReviewScene");

            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
    }
}
