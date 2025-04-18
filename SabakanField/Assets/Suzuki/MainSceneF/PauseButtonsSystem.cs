using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonsSystem : MonoBehaviour
{
    // �|�[�Y�N���X�Ɏ�������Ă���{�^���̌��ʂ��g��
    private PauseWindow _pauseWindow;
    private void Awake()
    {
        _pauseWindow = new PauseWindow();
    }


    // �ݒ�{�^���������ꂽ�Ƃ�
    public void PushSetting()
    {
        _pauseWindow.PushSetting();
    }
    // ����{�^���������ꂽ�Ƃ�
    public void PushClosed()
    {
        _pauseWindow.PushClosed();
    }
    // ��߂�{�^���������ꂽ�Ƃ�
    public void PushEndGame()
    {
        _pauseWindow.PushEndGame();
    }
}
