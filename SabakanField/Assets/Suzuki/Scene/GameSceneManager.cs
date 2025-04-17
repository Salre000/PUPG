using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン管理
public static class GameSceneManager
{
    // シーンの名前
    public const string mainSceneName = "MainScene";
    public const string clearSceneName = "ClearScene";
    public const string resultSceneName = "ResultScene";

    // 普通のシーン遷移
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // モードを含めた普通のシーン遷移
    public static void LoadScene(string sceneName,LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName,mode);
    }

    public static void FadeOutLoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName);
    }

}
