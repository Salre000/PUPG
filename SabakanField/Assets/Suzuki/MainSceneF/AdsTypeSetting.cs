using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTypeSetting : MonoBehaviour
{
    // ads���@�̏����ݒ�
    Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        // �`�F�b�N���t���O�����ꂽ�Ƃ��ɔ��΂���֐����w��
        _toggle.onValueChanged.AddListener(addListener=> OptionManager.Instance.ChangeAdsType());
    }
}
