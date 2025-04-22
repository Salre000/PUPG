using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTypeSetting : UIBase
{
    // ads���@�̏����ݒ�
    Toggle _toggle;

    public override void Execute()
    {

    }

    public override void Initialize()
    {
        _toggle = GameObject.Find("AdsToggle").GetComponent<Toggle>();
        _toggle.isOn = OptionManager.Instance.GetAdsType();
        // �`�F�b�N���t���O�����ꂽ�Ƃ��ɔ��΂���֐����w��
        _toggle.onValueChanged.AddListener(addListener => OptionManager.Instance.ChangeAdsType());
    }

}
