using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonsSystem : MonoBehaviour
{
    // ポーズクラスに実装されているボタンの効果を使う
    private PauseWindow _pauseWindow;
    private void Awake()
    {
        _pauseWindow = new PauseWindow();
    }


    // 設定ボタンが押されたとき
    public void PushSetting()
    {
        _pauseWindow.PushSetting();
    }
    // 閉じるボタンが押されたとき
    public void PushClosed()
    {
        _pauseWindow.PushClosed();
    }
    // やめるボタンが押されたとき
    public void PushEndGame()
    {
        _pauseWindow.PushEndGame();
    }
}
