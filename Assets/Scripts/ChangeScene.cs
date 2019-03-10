using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    
    public void ChangeSceneHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

public void ChangeSceneHomeWork()
    {
        SceneManager.LoadScene("HomeWorkScene");
    }
    public void ChangeSceneSchool()
    {
        SceneManager.LoadScene("SchoolScene");
    }
    public void ChangeSceneHistory()
    {
        SceneManager.LoadScene("HistoryScene");
    }
    public void ChangeSceneReview()
    {
        SceneManager.LoadScene("ReviewScene");
    }
    public void ChangeSceneShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    
}
