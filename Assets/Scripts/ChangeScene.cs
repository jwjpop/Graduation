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
    public void ChangeSceneResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
    public void ChangeSceneHistory()
    {
        SceneManager.LoadScene("HistoryScene");
    }
    public void ChangeSceneInven()
    {
        SceneManager.LoadScene("InvenScene");
    }
    public void ChangeSceneShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void ChangeSceneCoding()
    {
        SceneManager.LoadScene("CodingScene");
    }
    public void ChangeSceneBeforeCoding()
    {
        SceneManager.LoadScene("BeforeCodingScene");
    }
}
