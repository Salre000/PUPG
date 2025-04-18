using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UIManager;

public class PauseWindow : UIBase
{
    // ポーズ画面
    private GameObject _pausePanel;

    // 実行UpDate
    public override void Execute()
    {
        // ポーズ画面開閉
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(Instance.IsPause()) _pausePanel.SetActive(false);
            Instance.SetPauseWindow();
        }
        if (!Instance.IsPause()) return;
        OpenWindow();

    }

    public override void Initialize()
    {
        _pausePanel = GameObject.Find("PausePanel").gameObject;
        _pausePanel.SetActive(false);
    }

    // ポーズ画面が開かれる
    private void OpenWindow()
    {
        // ポーズ画面が開かれていない状態なら開く
        if (!_pausePanel.activeSelf) _pausePanel.SetActive(true);


    }
}
