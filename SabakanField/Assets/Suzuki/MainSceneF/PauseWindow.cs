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
            WindowCheck();
            Instance.SetPauseWindow();
        }
        if (!Instance.IsPause())
        {
            _pausePanel.SetActive(false);
            return;
        }
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
        // マウスを表示
        Cursor.lockState = CursorLockMode.Confined;

    }
    // 閉じたとき開かれている状態なら閉じる
    private void WindowCheck()
    {
        if (Instance.IsPause()) _pausePanel.SetActive(false);
    }

    // 設定ボタンが押されたとき
    public void PushSetting()
    {

    }
    // 閉じるボタンが押されたとき
    public void PushClosed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Instance.SetPauseWindow();
    }
    // やめるボタンが押されたとき
    public void PushEndGame()
    {

    }
}
