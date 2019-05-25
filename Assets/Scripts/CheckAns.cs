using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAns : MonoBehaviour
{
    //돈
    public Text CurText;
    public static List<Item> ans = new List<Item>();
    int stage = 0;
    //보여지는 말풍선 텍스트
    public Text bubbleText;
    //보여지는 맞음,틀림 결과 텍스트
    public Text result;
    //모든 원소의 이름을 담을 그릇
    string all;
    //문자열 블록에 있는 내용 뽑아오기
    string txt;
    //문자열 블록이 여러번 나올 경우
    List<string> txts;
    // X와 Y 변수
    string X, Y;
    //결과
    bool answer;

    //위쪽 설명 이미지
    GameObject explainImage;

    void Start()
    {
        //초기화
        all = null;
        txt = null;
        X = null;
        Y = null;
        txts = new List<string>();
        answer = false;

        CurText.text = DataControl.CurMoney.ToString();
        if(DataControl.where==1)
        {
            stage = ReviewScript.clickedStage;
        }
        else
        {
            stage = UnUseBlockMain.stageTitleIndex;
        }
        explainImage = GameObject.Find("Stage");

        ImageBinding();
        Checker();
        Result();
    }
    void ImageBinding()
    {
        if (stage == 1)
        {
            //1탄 설명에 사용할 이미지 적용
            explainImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BeforeStage1") as Sprite;
        }
    }

    public void Result()
    {
        if (answer)
        {
            result.text = "맞았습니다!";
            if (DataControl.where == 1)
            {
                result.text = "복습중!";
            }
            else
            {
                //정답시 스테이지 증가
                UnUseBlockMain.stageTitleIndex++;
                //돈증가 테스트
                DataControl.CurMoney++;
                CurText.text = DataControl.CurMoney.ToString();
                //저장 테스트
                PlayerPrefs.SetInt("stage", UnUseBlockMain.stageTitleIndex);
                PlayerPrefs.SetInt("money", DataControl.CurMoney);
                PlayerPrefs.Save();
            }
        }
        else
        {
            result.text = "틀렸습니다!";
            if (DataControl.where == 1)
            {
                result.text = "복습중!";
            }
            else
            {
                //테스트를 위해 틀려도 스테이지 증가
                UnUseBlockMain.stageTitleIndex++;
                //돈증가 테스트
                DataControl.CurMoney++;
                CurText.text = DataControl.CurMoney.ToString();
                //저장 테스트
                PlayerPrefs.SetInt("stage", UnUseBlockMain.stageTitleIndex);
                PlayerPrefs.SetInt("money", DataControl.CurMoney);
                PlayerPrefs.Save();
            }
            
        }
    }

    public void Checker()
    {
        //모든 배열의 네임을 한줄로 만든다
        for (int i = 0; i < ans.Count; i++)
        {
            all = all + ans[i].Name;

            //정답 체크할 답안 만드는 부분
            if (stage == 1)
            {
                if (ans[i].Name.Equals("\"\""))
                {
                    txt = ans[i].condition;
                }
            }
            else if (stage == 2)
            {
                if (ans[i].Name.Equals("Subject"))
                {
                    txt = ans[i].condition;
                }
            }
            else if (stage == 3)
            {
                if (ans[i].Name.Equals("X"))
                {
                    X = ans[i].condition;
                }
                else if (ans[i].Name.Equals("Y"))
                {
                    Y = ans[i].condition;
                }
                else if (ans[i].Name.Equals("\"\""))
                {
                    txt = ans[i].condition;
                }
            }
            else if (stage == 4)
            {
                if (ans[i].Name.Equals("X"))
                {
                    X = ans[i].condition;
                }
                else if (ans[i].Name.Equals("Y"))
                {
                    Y = ans[i].condition;
                }
                else if (ans[i].Name.Equals("\"\""))
                {
                    //인덱스 오류를 막기 위해 null이 넘어올 경우 ""로 대체
                    if(ans[i].condition == null)
                        txts.Add("");
                    else
                        txts.Add(ans[i].condition); 
                }
            }
            else if (stage == 5)
            {
                if (ans[i].Name.Equals("\"\""))
                {
                    txt = ans[i].condition;
                }
            }
            else if (stage == 6)
            {
                 if (ans[i].Name.Equals("\"\""))
                {
                    //인덱스 오류를 막기 위해 null이 넘어올 경우 ""로 대체
                    if (ans[i].condition == null)
                        txts.Add("");
                    else
                        txts.Add(ans[i].condition);
                }
            }
            else if (stage == 7)
            {
                //받아올 것 없음
            }
            else if (stage == 8)
            {
                if (ans[i].Name.Equals("\"\""))
                {
                    txt = ans[i].condition;
                }
            }
        }

        //정답인지 체크하는 부분
        if (stage == 1)
        {
            if (all != null && all.Equals("print시작\"\"print끝"))
            {
                if (txt != null && txt.Equals("Hello, World!"))
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
        else if (stage == 2)
        {
            if (all != null && all.Equals("print시작Subjectprint끝"))
            {
                if (txt != null)
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
        else if (stage == 3)
        {
            if (all != null && all.Equals("print시작X+Y,\"\"print끝"))
            {
                if(X != null && Y != null && txt != null 
                    && X.Equals("3") && Y.Equals("4") && txt.Equals("명인거지!"))
                {
                    bubbleText.text = X + Y + txt;
                    answer = true;
                }
            }
        }
        else if (stage == 4)
        {
            if (all != null && all.Equals("print시작\"\",X+Y,\"\"print끝"))
            {
                if (X != null && Y != null && X.Equals("1000") && Y.Equals("2000"))
                {
                    if (txts[0].Equals("친구야") && txts[1].Equals("원이 필요해!"))
                    {
                        int value = Convert.ToInt32(X) + Convert.ToInt32(Y);
                        bubbleText.text = txts[0] + value + txts[1];
                        answer = true;
                    }
                }
            }
        }
        else if (stage == 5)
        {
            if (all != null && all.Equals("if시작모자==빨간색조건print시작\"\"print끝if끝"))
            {
                if (txt != null && txt.Equals("안녕"))
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
        else if (stage == 6)
        {
            if (all != null && 
                all.Equals("if시작문자==1조건print시작\"\"print끝if끝else시작print시작\"\"print끝else끝"))
            {
                if (txts[0].Equals("수업 듣기") && txts[1].Equals("교실로 가기"))
                {
                    //정답일 때 뿌릴거 고민해야함
                    bubbleText.text = txts[0] + txts[1];
                    answer = true;
                }
            }
        }
        else if (stage == 7)
        {
            if (all != null && all.Equals("if시작체크카드 잔액>=10000조건메뉴=치킨if끝" +
                           "elif시작체크카드 잔액>=8000조건메뉴=떡볶이elif끝" +
                           "elif시작체크카드 잔액>=6000조건메뉴=짜장면elif끝" +
                           "elif시작체크카드 잔액>=4000조건메뉴=학식elif끝" +
                           "elif시작체크카드 잔액>=2000조건메뉴=라면elif끝" +
                           "else시작메뉴=굶기else끝" +
                           "print시작메뉴print끝"))
            {
                int money = DataControl.CurMoney;
                if (money >= 10000)
                {
                    bubbleText.text = "오늘 저녁은 치킨이닭!";
                }
                else if(money >= 8000)
                {
                    bubbleText.text = "떡볶이가 땡기는데?";
                }
                else if (money >= 6000)
                {
                    bubbleText.text = "짜장면은 역시 간짜장!";
                }
                else if (money >= 4000)
                {
                    bubbleText.text = "학식 돈까스가 최고지";
                }
                else if (money >= 2000)
                {
                    bubbleText.text = "오늘도 라면인가,,,";
                }
                else
                {
                    bubbleText.text = "굶자...";
                }
                answer = true;
            }
        }
        else if (stage == 8)
        {
            if (all != null && all.Equals("if시작과제==0&&벚꽃==1조건print시작\"\"print끝if끝"))
            {
                if (txt != null && txt.Equals("놀자"))
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
    }
}
