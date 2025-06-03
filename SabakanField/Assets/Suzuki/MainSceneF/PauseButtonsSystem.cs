using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonsSystem : MonoBehaviour
{
    // �|�[�Y�N���X�Ɏ�������Ă���{�^���̌��ʂ��g��
    private PauseWindow _pauseWindow;

    private GameObject _settingPanel;
    private Button _settingButton;
    private Button _closeButton;
    private Button _gameCloseButton;
    private void Awake()
    {
        _pauseWindow = new PauseWindow();
        _settingPanel=GameObject.Find("SettingMode").gameObject;
        _settingButton=GameObject.Find("SettingButton").GetComponent<Button>();
        _closeButton=GameObject.Find("CloseButton").GetComponent<Button>();
        _gameCloseButton=GameObject.Find("GameCloseButton").GetComponent<Button>();
        _settingButton.onClick.AddListener(PushSetting);
        _closeButton.onClick.AddListener(PushClosed);
        _gameCloseButton.onClick.AddListener(PushEndGame);
    }

    private void Start()
    {
        _settingPanel.SetActive(false);

    }

    // �ݒ�{�^���������ꂽ�Ƃ�
    public void PushSetting()
    {
        _settingPanel.SetActive(true);  
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
