using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ÉQÅ[ÉÄÇÃêiçsä«óù

    public static GameManager Instance;

    private bool _finaleFlag = false;
    private bool _flagGetCheck = false;
    private bool _resultSceneFlag = false;
    private float _time = 0.0f;

    private void Awake()
    {
        Instance = this;
        _finaleFlag = false;
        _flagGetCheck = false;
        BulletManager.Initialize();
        PlayerManager.SetIsPlayerDead(false);
    }

    private void Update()
    {
        if (UIManager.Instance.GetOverTime()&&!_finaleFlag)
        {
            GameClearCheck();
        }
        ResultSceneChange();
    }

    public void GameClearCheck()
    {
        if(_finaleFlag) return;
        GameSceneManager.LoadScene(GameSceneManager.clearScene, LoadSceneMode.Additive);
        _finaleFlag = true;
    }

    public void ResultSceneChange()
    {
        if(!_finaleFlag) return;
        _time += Time.deltaTime;
        if (_time < 3.0f) return;
        _resultSceneFlag=true;
        AIUtility.SaveData();
        SceneManager.LoadScene(GameSceneManager.resultScene);
    }

    public void SetFlagRangeCheck(bool flag) { _flagGetCheck = flag; }
    public bool GetFlagRangeCheck() { return _flagGetCheck; }
    public bool GetCheckResultScene() { return _resultSceneFlag; }
}
