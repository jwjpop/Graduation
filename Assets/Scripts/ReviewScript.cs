using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviewScript : MonoBehaviour
{
    string[] buttonName;
    public Text[] grade = new Text[9];
    GameObject[] seme = new GameObject[9];
    GameObject[] semeButton = new GameObject[33];
    public static int clickedStage;
    float[] gradeAvg = new float[9];
    float[] grades = new float[33];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 33; i++)
        {
            grades[i] = PlayerPrefs.GetFloat("grades" + i, 4.5f);
        }

        for (int i = 1;i<=2;i++)
        {
            seme[i] = GameObject.Find("seme" + i);
            seme[i].SetActive(false);
        }
        
        if (UnUseBlockMain.stageTitleIndex >= 1)
        {
            seme[1].SetActive(true);
        }
        if (UnUseBlockMain.stageTitleIndex >= 5)
        {
            seme[2].SetActive(true);
        }
        
        for (int i = 1; i <= 8; i++)
        {
            gradeAvg[i] = (grades[i*4-3] + grades[i*4-2] + grades[i*4-1] + grades[i*4])/4;
            if (grade[i] != null)
            {
                if (gradeAvg[i] == 4.5)
                {
                    grade[i].text = "A+";
                }
                else if (gradeAvg[i] < 4.5 && gradeAvg[i] >= 4)
                {
                    grade[i].text = "A";
                }
                else if (gradeAvg[i] < 4 && gradeAvg[i] >= 3.5)
                {
                    grade[i].text = "B+";
                }
                else if (gradeAvg[i] < 3.5 && gradeAvg[i] >= 3)
                {
                    grade[i].text = "B";
                }
                else if (gradeAvg[i] < 3 && gradeAvg[i] >= 2.5)
                {
                    grade[i].text = "C+";
                }
                else
                {
                    grade[i].text = "C";
                }
            }
            semeButton[i] = GameObject.Find("ButtonSem_" + i);
            if (semeButton[i] != null)
            {
                semeButton[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                break;
            }
        }
        
        for(int i=1;i<= UnUseBlockMain.stageTitleIndex;i++)
        {
            semeButton[i].GetComponent<Button>().interactable = true;
        }
    }

    //버튼이 눌리면
    public void ButtonClicked(Button button)
    {
        buttonName = button.name.Split('_');
        clickedStage = Convert.ToInt32(buttonName[1]);
        
        if(clickedStage<=UnUseBlockMain.stageTitleIndex)
        {
            String fullName = PlayerPrefs.GetString("Code"+clickedStage,"");
            Debug.Log(fullName);
            DataControl.where = 1;
            SceneManager.LoadScene("ReviewScene");
            
        }
    }
    
}
