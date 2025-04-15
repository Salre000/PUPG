using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ClearTextAnim : MonoBehaviour
{
    // クリアテキストのための動きを実装
    private TextMeshProUGUI _clearText;
    private StringBuilder _stringBuilder = new StringBuilder();
    private string _winText = "Bule team Win!!";
    // 待ち時間
    private float _dileyTime = 0.05f;

    private void Awake()
    {
        _clearText = GetComponent<TextMeshProUGUI>();
        _stringBuilder.Clear();
        WinAnimation();

    }

    private void Update()
    {

    }


    private void WinAnimation()
    {
        _stringBuilder.Clear();
        StartCoroutine(TextAnimation());
    }

    public IEnumerator TextAnimation()
    {
        foreach (var text in _winText)
        {
            _stringBuilder.Append(text);
            _clearText.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(_dileyTime);
        }
        Debug.Log("呼び出し");

    }

}
