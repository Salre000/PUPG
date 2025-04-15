using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ClearTextAnim : MonoBehaviour
{
    // �N���A�e�L�X�g�̂��߂̓���������
    private TextMeshProUGUI _clearText;
    private StringBuilder _stringBuilder = new StringBuilder();
    private string _winText = "Bule team Win!!";
    // �҂�����
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
        Debug.Log("�Ăяo��");

    }

}
