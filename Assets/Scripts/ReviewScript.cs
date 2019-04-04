﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviewScript : MonoBehaviour
{
    string[] buttonName;
    GameObject[] seme = new GameObject[9];
    public static int clickedStage;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1;i<=2;i++)
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

    }

    //버튼이 눌리면
    public void ButtonClicked(Button button)
    {
        buttonName = button.name.Split('_');
        clickedStage = Convert.ToInt32(buttonName[1]);
        
        if(clickedStage<UnUseBlockMain.stageTitleIndex)
        {
            //String fullName = PlayerPrefs.GetString("Code"+clickedStage,"");
            //Debug.Log(fullName);
            DataControl.where = 1;
            SceneManager.LoadScene("ReviewScene");
            
        }
        else
        {
            Debug.Log("클리어하고오세요");
        }
    }
    
}