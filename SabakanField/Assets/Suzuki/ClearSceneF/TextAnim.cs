using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    //  �ꕶ�����̓���������
    private TextMeshProUGUI _text;
    private StringBuilder _stringBuilder = new StringBuilder();
    private string _buleWinText = "Bule team Win!!";    // ���������Ƃ�
    private string _defText = "Defeat";             // ��������
    private Color32 _buleWinColor= new Color32(43, 69, 221, 255);
    private Color32 _redWinColor= new Color32(221, 43, 48, 255);
    private Color32 _defaultColor=new Color32();
    // ���̕����ւ̑҂�����
    private float _dileyTime = 0.02f;

    // �e�L�X�g�̃A�j���[�V�����������������Ȃ炱����g�p

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


        // �U�ߑ��������Ƃ�
        if (!UIManager.Instance.GetOverLimitTime())
        {
            resultText = _buleWinText;
            _text.color = _buleWinColor;
        }
        // �U�ߑ���������
        else
        {
            resultText = _defText;
            _text.color = _redWinColor;
        }

        // ���������ȊO�Ń^�C�s���OAnim���鎞
        if (GameManager.Instance.GetCheckResultScene())
        {
            _text.color=_defaultColor;
            resultText=_text.text;
        }
        return resultText;
    }

}
