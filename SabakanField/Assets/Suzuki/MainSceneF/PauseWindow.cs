using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UIManager;

public class PauseWindow : UIBase
{
    // �|�[�Y���
    private GameObject _pausePanel;

    // ���sUpDate
    public override void Execute()
    {
        // �|�[�Y��ʊJ��
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(Instance.IsPause()) _pausePanel.SetActive(false);
            Instance.SetPauseWindow();
        }
        if (!Instance.IsPause()) return;
        OpenWindow();

    }

    public override void Initialize()
    {
        _pausePanel = GameObject.Find("PausePanel").gameObject;
        _pausePanel.SetActive(false);
    }

    // �|�[�Y��ʂ��J�����
    private void OpenWindow()
    {
        // �|�[�Y��ʂ��J����Ă��Ȃ���ԂȂ�J��
        if (!_pausePanel.activeSelf) _pausePanel.SetActive(true);


    }
}
