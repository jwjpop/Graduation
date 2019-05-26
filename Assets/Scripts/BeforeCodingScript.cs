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
    string[] script = { "0번","9","19","25","32",
                        "40","52","62","82",
                        "친구를 만나면 인사를 해야돼", "말을 하기 위해선 print를 사용해보자!","추가된 블록을 누르면 함수 내부로 들어갈 수 있어!","말을 적을 수 있는 공간이 필요해",
                        "추가된 블록을 누르면 내용을 채울 수 있어","내용을 입력해줘","수정을 눌러야 반영이 돼","","그럼 친구에게 인사를 해보자!","/",
                        "Stage 1에서 \"\"에 Hello, World!를 담았더니","아래처럼 변했지?","이번엔 Subject라는 변수에","듣고싶은 과목을 넣어보고","\"\"와 어떤 차이가 있는지 확인해보자!","/",
                        "다른 과랑 미팅이 잡혔어!","그런데 저쪽은 3명이고 우리는 4명이야","아래와 같은 구조를 활용해서","다음에 나오는 말풍선처럼 표현해야해!","","이제 친구에게 알려주자!","/",
                        "Stage 3에선 X와 Y를 문자형 변수로 인식해서 그래","이번엔 X와 Y가 정수형 변수라고 생각하고","1000원짜리 김밥과 2000원짜리 라면을 살 때","총 얼마가 필요한지 다음처럼 표현해보자","",
                        "이를 통해 문자형 변수와 정수형 변수의","차이점을 이해해보자!","/",
                        "모자가 유행해서 학교에 모자를 쓴 사람이 많네?","친구는 빨간색 모자를 쓰고 왔대!","이처럼 특정한 조건을 판단해야할 때 조건문을 써야해","if를 추가해서 if시작을 누르게 되면","조건을 만족할 때의 행동을 정할 수 있어!",
                        "if끝을 누르게 되면","조건을 만족할 때의 행동을 정할 수 있어!","편의를 위해서 조건을 누를 경우에는","어떤 부분을 채울지 위 아래를 선택할 수 있어","그럼 이제 같음을 의미하는 ==을 이용해서 조건을 만족시키고","안녕 이라고 인사할 수 있도록 행동을 정해보자!","/",
                        "비가 너무 많이 오네!","휴강이 될 것 같은데 휴강 문자가 오면 집에서 쉬고","아니면 교실로 가보자!","Stage5와는 반대로 조건을 만족하지 않을 때의 행동을 정해볼거야","조건을 만족할 때와 만족하지 않을 때는 아래처럼","if와 else를 이용해 만들 수 있어",
                        "그리고 블록 1과 0은 조건에서 쓰일 때 yes와 no를 의미해","문자가 yes라면 쉬기 를 출력하고","그게 아니라면 교실로 가기 를 출력해줘!","/",
                        "밥을 먹어야하는데 체크카드에 얼마가 있는지 모르겠어!","잔액을 확인할 수 없으니까 비싼 순서대로 한 번 결제해보자!","Stage6에서 if와 else를 이용해서","조건을 만족하는 경우와 그렇지 않은 경우를 알아봤지?","이번엔 조건을 만족하지 않을 때 새 조건을 주는 방법을 알아볼거야!",
                        "아래와 같이 if 다음에 elif를 추가해서","if의 조건을 만족하지 않을 때 elif로 새 조건을 검사할 수 있어!","아래의 그림처럼 모든 조건을 만족하지 않을 경우에는","else의 동작을 해라~ 라고도 할 수 있지","그리고 변수에 값을 넣을때는 아래 그림처럼","=을 이용해 변수 = 값 형태로 나타내야해",
                        "그럼 이제 조건에 따라 메뉴라는 변수에 값을 넣고 출력까지 해보자!","만약 체크카드에 10000원 이상이 있다면 메뉴라는 변수에 치킨을 넣고","그게 아니라 8000원 이상이 있다면 메뉴에 떡볶이를 넣고","그게 아니라 6000원 이상이 있다면 메뉴에 짜장면을 넣고","그게 아니라 4000원 이상이 있다면 메뉴에 학식을 넣고",
                        "그게 아니라 2000원 이상이 있다면 메뉴에 라면을 넣고","모두 아니면 메뉴에 굶기를 넣은 다음","최종적으로 메뉴에 무엇이 담겨있는지 출력해보자!","/",
                        "이번 주말 한강에 벚꽃이 예쁘게 필거래!","놀러가서 꽃놀이도 하고 저녁에 치맥도 하고싶어 ㅠㅠ","그런데 과제가 있을지도 몰라!","그럼 과제도 없고! 벚꽃도 만개했다면 놀러가자!","아래처럼 2가지 조건을 동시에 만족해야할 때",
                        "if문 안에 if문을 써서 해결할 수도 있지만","&&(AND)를 사용하면","하나의 if문에서 여러가지 조건을 모두 만족하는지 검사할 수 있어!","그럼 Stage6에서 배웠던 1, 0에 대한 개념과","if문과 && 연산을 통해서 조건을 구성하고","조건을 만족하면 print문을 통해 놀자 를 출력해보자!","/"};

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

                if (i < 9)
                codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_"+stage +"_"+i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if(stage == 2)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (i < 4)
                codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 3)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (script[scriptStartIndex].Equals(""))
                {
                    beforePanel.SetActive(false);
                    txtBubble.text = "7명이야";
                    bubble.SetActive(true);
                }
                else
                {
                    beforePanel.SetActive(true);
                    bubble.SetActive(false);
                }

                if (scriptStartIndex==27)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 4)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (script[scriptStartIndex].Equals(""))
                {
                    beforePanel.SetActive(false);
                    txtBubble.text = "친구야____원이 필요해!";
                    bubble.SetActive(true);
                }
                else
                {
                    beforePanel.SetActive(true);
                    bubble.SetActive(false);
                }
                
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 5)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (scriptStartIndex == 43)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 45)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 47)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 6)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (scriptStartIndex == 56)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 58)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 60)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 7)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (scriptStartIndex == 67)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 69)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 71)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
        else if (stage == 8)
        {
            if (script[scriptStartIndex].Equals("/"))
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
            else
            {
                if (scriptStartIndex == 86)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                else if (scriptStartIndex == 88)
                    codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_" + stage + "_" + i++) as Sprite;
                txtScript.text = script[scriptStartIndex];
            }
        }
    }
}
