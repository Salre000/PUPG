using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    // �N���A�e�L�X�g�̂��߂̓���������
    private TextMeshProUGUI _text;
    private StringBuilder _stringBuilder = new StringBuilder();
    private string _winText = "Bule team Win!!";
    private Color32 _buleWinColor= new Color32(43, 69, 221, 255);
    private Color32 _redWinColor= new Color32(221, 43, 48, 255);
    private string _defText = "Defeat";
    // �҂�����
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
        // �U�ߑ��������Ƃ�
        if (!UIManager.Instance.GetOverLimitTime())
        {
            _resultText=_winText;
            _text.color = _buleWinColor;
        }
        // �U�ߑ���������
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
