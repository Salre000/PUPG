using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _hp = 0;
    [SerializeField]
    private TextMeshProUGUI _text;
    private StringBuilder _stringBuilder=new();

    private float _emptySpeed = 1.5f;

    [SerializeField]
    private Color _emptyColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Initialize();
        ColorLerp();
    }

    private void Initialize()
    {
        // 0==プレイヤーのHP
        _hp = (int)ChracterHPManager.instance.GetHp(0);
        if (_hp < 0) _hp = 0;
        _stringBuilder.Clear();
        _stringBuilder.Append(_hp);
        _text.text = _stringBuilder.ToString();
    }

    // 体力が少なくなるほど体力表示が赤くなる
    private void ColorLerp()
    {
        float current = _hp;
        float total = ChracterHPManager.instance.GetMaxHp();

        float colorAlpha = (current / total) * _emptySpeed;
        _text.color = Color.Lerp(_emptyColor, Color.white, colorAlpha);
    }
}
