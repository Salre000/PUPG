using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// フェードイン、アウト制御
public class ImageFader : MonoBehaviour
{
    private Image _fadeImage;
    private Color32 _color=new Color32();
    private float _time = 10.0f;

    private void Awake()
    {
        _fadeImage = GetComponent<Image>();
        _color=_fadeImage.color;
        _color.a = 0;
        _fadeImage.color = _color;
    }

    public static void FadeIn()
    {
        
    }

    public void FadeOut()
    {

    }

}
