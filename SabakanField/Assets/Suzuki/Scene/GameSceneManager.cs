using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ÉVÅ[Éìä«óù
public static class GameSceneManager
{
    public const string mainSceneName = "MainScene";
    public const string clearSceneName = "ClearScene";
    public const string resultSceneName = "resultScene";

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(string sceneName,LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName,mode);
    }

    public static void FadeOutLoadScene(string sceneName, LoadSceneMode mode,float time)
    {
        SceneManager.LoadScene(sceneName);
    }

}
