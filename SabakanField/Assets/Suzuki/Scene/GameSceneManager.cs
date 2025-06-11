using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �V�[���Ǘ�
public static class GameSceneManager
{
    // �V�[���̖��O
    public const string titleScene = "TitleScene";
    public const string flagScene = "GameFlagScene";
    public const string clearScene = "ClearScene";
    public const string resultScene = "ResultScene";
    public const string lobbyScene = "LobbyScene";

    // ���ʂ̃V�[���J��
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ���[�h���܂߂����ʂ̃V�[���J��
    public static void LoadScene(string sceneName,LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName,mode);
    }

    public static void FadeOutLoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName);
    }

}
