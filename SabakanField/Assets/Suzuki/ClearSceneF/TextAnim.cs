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
    private Color32 _defaultColor=new Color32();
    private string _defText = "Defeat";
    // 待ち時間
    private float _dileyTime = 0.05f;

    // テキストのアニメーションがしたいだけならこれを使用

    private void Awake()
    {
        Initialize();
        TextAnimationStartCoroutine();
    }

    private void Initialize()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _defaultColor= _text.color;
        _stringBuilder.Clear();
    }

    private void TextAnimationStartCoroutine()
    {
        _stringBuilder.Clear();
        StartCoroutine(TextAnimation());
    }

    public IEnumerator TextAnimation()
    {
        string resultText=DefaultAnimationSet();

        foreach (var foreachText in resultText)
        {
            _stringBuilder.Append(foreachText);
            _text.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(_dileyTime);
        }

    }

    public string DefaultAnimationSet()
    {
        string resultText = null;


        // 攻め側勝ったとき
        if (!UIManager.Instance.GetOverLimitTime())
        {
            resultText = _winText;
            _text.color = _buleWinColor;
        }
        // 攻め側負けた時
        else
        {
            resultText = _defText;
            _text.color = _redWinColor;
        }

        // 勝ち負け以外でタイピングAnimする時
        if (GameManager.Instance.GetCheckResultScene())
        {
            _text.color=_defaultColor;
            resultText=_text.text;
        }
        return resultText;
    }

}
