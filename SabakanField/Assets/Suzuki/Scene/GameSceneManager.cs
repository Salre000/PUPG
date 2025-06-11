using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン管理
public static class GameSceneManager
{
    // シーンの名前
    public const string titleScene = "TitleScene";
    public const string flagScene = "GameFlagScene";
    public const string clearScene = "ClearScene";
    public const string resultScene = "ResultScene";
    public const string lobbyScene = "LobbyScene";

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
