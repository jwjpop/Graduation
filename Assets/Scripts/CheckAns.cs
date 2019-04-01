using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAns : MonoBehaviour
{
    public static List<Item> ans = new List<Item>();
    public static int stage = 0;
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

    void Start()
    {
        //초기화
        all = "";
        txt = "";
        X = "";
        Y = "";
        txts = new List<string>();
        answer = false;

        Checker();
        Result();
    }

    public void Result()
    {
        if (answer)
        {
            result.text = "맞았습니다!";
        }
        else
        {
            result.text = "틀렸습니다!";
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
                    txts.Add(ans[i].condition);
                }
            }
            else if (stage == 5)
            {
            }
            else if (stage == 6)
            {
            }
            else if (stage == 7)
            {
            }
            else if (stage == 8)
            {
            }
        }

        //정답인지 체크하는 부분
        if (stage == 1)
        {
            if (all.Equals("print시작\"\"print끝"))
            {
                if (txt.Equals("Hello, World!"))
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
        else if (stage == 2)
        {
            if (all.Equals("print시작Subjectprint끝"))
            {
                if (txt.Equals("Hello, World!"))
                {
                    bubbleText.text = txt;
                    answer = true;
                }
            }
        }
        else if (stage == 3)
        {
            if (all.Equals("print시작X+Y,\"\"print끝"))
            {
                if(X.Equals("3") && Y.Equals("4") && txt.Equals("명인거지!"))
                {
                    bubbleText.text = X + Y + txt;
                    answer = true;
                }
            }
        }
        else if (stage == 4)
        {
            if (all.Equals("print시작\"\",X+Y,\"\"print끝"))
            {
                if (X.Equals("1000") && Y.Equals("2000"))
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
            if (all.Equals("if시작모자==빨간색조건print시작\"\"print끝if끝"))
            {
                //로직
            }
        }
        else if (stage == 6)
        {
            if (all.Equals("if시작문자==1조건print시작\"\"print끝if끝else시작print시작\"\"print끝else끝"))
            {
                //로직
            }
        }
        else if (stage == 7)
        {
            if (all.Equals("if시작체크카드 잔액>=10000조건메뉴=치킨if끝" +
                           "elif시작체크카드 잔액>=8000조건메뉴=떡볶이elif끝" +
                           "elif시작체크카드 잔액>=6000조건메뉴=짜장면elif끝" +
                           "elif시작체크카드 잔액>=4000조건메뉴=학식elif끝" +
                           "elif시작체크카드 잔액>=2000조건메뉴=라면elif끝" +
                           "else시작메뉴=굶기else끝" +
                           "print시작메뉴print끝"))
            {
                //로직
            }
        }
        else if (stage == 8)
        {
            if (all.Equals("if시작과제==0&&벚꽃==1조건print시작\"\"print끝if끝"))
            {
                //로직
            }
        }
    }
}
