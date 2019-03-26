using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScript : MonoBehaviour
{
    public Text txtScript;
    GameObject beforePanel;
    GameObject bubble;
    GameObject bubbleTxt;
    GameObject codingImage;
    int routine = 1;
    string[] script = { "친구를 만나면 인사를 해야돼", "말을 하기 위해선 print를 사용해보자!",
                        "문자는 꼭 따옴표(\"\")가 안에 들어가야해!","","그럼 친구에게 인사를 해보자!" };
    // Start is called before the first frame update
    void Start()
    {
        txtScript.text = script[0];
        beforePanel = GameObject.Find("BeforePanel");
        bubble = GameObject.Find("Bubble_nine");
        bubble.SetActive(false);
        bubbleTxt = GameObject.Find("Bubble_txt");
        bubbleTxt.SetActive(false);
        codingImage = GameObject.Find("CodingImage");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 테스트임 터치로 바꿔야함
        {
            if (routine < script.Length)
            {
                for (int i = routine; i < script.Length;i++)
                {
                    if(i == 2)
                    {
                        codingImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Before_2") as Sprite;
                    }
                    if(script[i].Equals(""))
                    {
                        beforePanel.SetActive(false);
                        bubble.SetActive(true);
                        bubbleTxt.SetActive(true);
                    }
                    else
                    {
                        beforePanel.SetActive(true);
                        bubble.SetActive(false);
                        bubbleTxt.SetActive(false);
                    }
                    txtScript.text = script[i];
                    break;
                }
                routine++;
            }
            else
            {
                SceneManager.LoadScene("CodingScene");
            }
        }
    }
}
