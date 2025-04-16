using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    // クリアテキストのための動きを実装
    private TextMeshProUGUI _text;
    private StringBuilder _stringBuilder = new StringBuilder();
    private string _winText = "Bule team Win!!";
    private Color32 _buleWinColor= new Color32(43, 69, 221, 255);
    private Color32 _redWinColor= new Color32(221, 43, 48, 255);
    private string _defText = "Defeat";
    // 待ち時間
    private float _dileyTime = 0.05f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _stringBuilder.Clear();
        WinAnimation();

    }

    private void WinAnimation()
    {
        _stringBuilder.Clear();
        StartCoroutine(TextAnimation());
    }

    public IEnumerator TextAnimation()
    {
        string _resultText=null;
        // 攻め側勝ったとき
        if (!UIManager.Instance.GetOverLimitTime())
        {
            _resultText=_winText;
            _text.color = _buleWinColor;
        }
        // 攻め側負けた時
        else
        {
            _resultText=_defText;
            _text.color = _redWinColor;
        }

        foreach (var foreachText in _resultText)
        {
            _stringBuilder.Append(foreachText);
            _text.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(_dileyTime);
        }

    }

}
