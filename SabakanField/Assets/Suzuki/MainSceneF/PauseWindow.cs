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
            WindowCheck();
            Instance.SetPauseWindow();
        }
        if (!Instance.IsPause())
        {
            _pausePanel.SetActive(false);
            return;
        }
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
        // �}�E�X��\��
        Cursor.lockState = CursorLockMode.Confined;

    }
    // �����Ƃ��J����Ă����ԂȂ����
    private void WindowCheck()
    {
        if (Instance.IsPause()) _pausePanel.SetActive(false);
    }

    // �ݒ�{�^���������ꂽ�Ƃ�
    public void PushSetting()
    {

    }
    // ����{�^���������ꂽ�Ƃ�
    public void PushClosed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Instance.SetPauseWindow();
    }
    // ��߂�{�^���������ꂽ�Ƃ�
    public void PushEndGame()
    {

    }
}
