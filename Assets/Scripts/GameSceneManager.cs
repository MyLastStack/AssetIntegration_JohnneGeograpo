using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void EndCredit()
    {
        SceneManager.LoadScene(2);
    }

    public static void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public static void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }
    public static void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
