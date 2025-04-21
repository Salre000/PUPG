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
            Instance.ChangePauseWindow();
        }
        if (!Instance.IsPause())
        {
            _pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

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
        // マウスカーソルを非表示にしてポーズ画面を閉じる
        Cursor.lockState = CursorLockMode.Locked;
        Instance.ChangePauseWindow();
    }
    // やめるボタンが押されたとき
    public void PushEndGame()
    {
        // ゲームを終了
        Application.Quit();
    }
}
