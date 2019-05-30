using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviewScript : MonoBehaviour
{
    string[] buttonName;
    GameObject[] grade = new GameObject[9];
    GameObject[] seme = new GameObject[9];
    GameObject[] semeButton = new GameObject[33];
    public static int clickedStage;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1;i<=2;i++)
        {
            seme[i] = GameObject.Find("seme" + i);
            seme[i].SetActive(false);
            grade[i] = GameObject.Find("TextGradeString"+i);
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
