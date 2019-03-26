using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAns : MonoBehaviour
{
    public static List<Item> ans = new List<Item>();
    public Text bubbleText;
    public Text result;
    string txt;
    string all;

    void Start()
    {
        all = "";
        txt = "";

        for (int i=0;i<ans.Count;i++)
        {
            all = all + ans[i].Name;
            if(ans[i].Name.Equals("\"\""))
            {
                txt = ans[i].condition;
            }
        }

        //프린트 안에 txt 있으면 뽑아는 줌 
        if (all.Equals("print시작\"\"print끝"))
        {
            bubbleText.text = txt;

            if (txt.Length > 14)
            {
                bubbleText.text = txt.Substring(0, 14) + "...";
            }
        }
        
        //그러나 정답은 txt까지 같아야함
        if(all.Equals("print시작\"\"print끝") && txt.Equals("Hello, World!"))
        {
            result.text = "맞았습니다!";
        }
        else
        {
            result.text = "틀렸습니다!";
        }
    }
   


}
