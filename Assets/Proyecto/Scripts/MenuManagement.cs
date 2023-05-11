using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManagement : MonoBehaviour
{

    public void GameStart()
    {
        SceneManager.LoadScene("NVL-1");
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void GameCredits()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
