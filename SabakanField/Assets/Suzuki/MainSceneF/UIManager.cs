using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI管理
    public static UIManager Instance;
    // 青ゲージ
    private Image _playerSideGageImage;
    // 何%確保できているかの表記
    private TextMeshProUGUI _playerSideGagePaercentText;
    private StringBuilder _stringBuilder = new StringBuilder();
    // パーセント表示
    private const float _MAX_FLAG_GAGE = 1.0f;
    // 何%でクリアかを決める
    private const float _PAERCENT = 10.0f;
    // ゲージの値
    private float _count = 0.0f;
    // 制限時間クラス
    private TimeLimit _timeLimit;
    // ポーズクラス
    private PauseWindow _pauseWindow;


    // ポーズ画面を開く閉じる
    private bool _isPause = false;

    // ゲージ100%でくりあしたか
    private bool _ClearCheckFlag = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        Initialize();
    }

    private void Update()
    {
        Execute();
    }

    private void Initialize()
    {

        _playerSideGageImage = GameObject.Find("PlayerSideGageImage").GetComponent<Image>();
        _playerSideGageImage.fillAmount = 0.0f;
        _stringBuilder.Clear();
        _stringBuilder.AppendFormat("{0:0.0}%", _count);
        _playerSideGagePaercentText = GameObject.Find("PercentText").GetComponent<TextMeshProUGUI>();
        _playerSideGagePaercentText.text = _stringBuilder.ToString();

        // クラス群
        _timeLimit = new TimeLimit();
        _timeLimit.Initialize();
        _pauseWindow = new PauseWindow();
        _pauseWindow.Initialize();

    }

    // クラスの実行処理
    private void Execute()
    {
        _timeLimit.Execute();
        _pauseWindow.Execute();
    }

    public bool GageMaxCheck()
    {
        PercentTextChenge();
        // ゲージが100%になったか
        if (_count >= _MAX_FLAG_GAGE)
        {
            _count = _MAX_FLAG_GAGE;
            CheckClearScene();
            return true;
        }

        return false;
    }

    // 占領ゲージの上昇
    public void FlagCountGage(float count)
    {
        // ゲージが100%もしくはデス状態は増えないようにする
        if (GageMaxCheck() || PlayerManager.IsPlayerDead()) return;
        _count = count / _PAERCENT;
        // ゲージの上昇
        _playerSideGageImage.fillAmount = _count;
    }

    // ゲージをどれだけためたか視認できるように
    private void PercentTextChenge()
    {

        _stringBuilder.Clear();
        float count = _count * _PAERCENT;
        _stringBuilder.AppendFormat("{0:0.0}%", count);
        _playerSideGagePaercentText.text = _stringBuilder.ToString();
    }

    // クリア画面遷移チェック
    private void CheckClearScene()
    {
        if (!_ClearCheckFlag)
            GameManager.Instance.GameClearCheck();
        _ClearCheckFlag = true;
    }



    // 時間切れかどうか返す
    public bool GetOverLimitTime() { return _timeLimit.GetOverLimit(); }
    public float GetTime() { return _timeLimit.GetTime(); }
    // ポーズ画面を開く閉じる
    public void SetPauseWindow() { if (_isPause) _isPause = false; else _isPause = true; }
    // ポーズ画面開閉状態チェック
    public bool IsPause() { return _isPause; }
}
