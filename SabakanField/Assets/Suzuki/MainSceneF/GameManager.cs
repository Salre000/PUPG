using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ÉQÅ[ÉÄÇÃêiçsä«óù

    public static GameManager Instance;

    private bool _DebugOver = false;
    private bool _flagGetCheck { get; set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (UIManager.Instance.GetOverLimitTime()&&!_DebugOver)
        {
            GameClearCheck();
        }
            
    }

    public void GameClearCheck()
    {
        if(_DebugOver) return;
        SceneManager.LoadScene("ClearScene", LoadSceneMode.Additive);
        _DebugOver = true;

    }

    public void SetFlagRangeCheck(bool flag) { _flagGetCheck = flag; }
    public bool GetFlagRangeCheck() { return _flagGetCheck; }
}
