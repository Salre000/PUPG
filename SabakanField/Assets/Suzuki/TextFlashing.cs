using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlashing : MonoBehaviour
{
    private TextMeshProUGUI _anyKeyText;
    private Color32 _color=new();
    private byte _alfa = 255;
    private bool isAlfa = false;
    private void Awake()
    {
        _anyKeyText = GetComponent<TextMeshProUGUI>();
        _color=_anyKeyText.color;
    }
    void Update()
    {
        Flashing();
    }
    private void Flashing()
    {
        AlfaAdjustment();
        _anyKeyText.color = _color;
    }
    private void AlfaAdjustment()
    {
        if( _alfa >= 254 )
            isAlfa = true;
        if( _alfa <= 1)
            isAlfa= false;

        if ( isAlfa )
            _alfa -= 2;
        else
            _alfa += 2;

        _color.a = _alfa;
    }
}
