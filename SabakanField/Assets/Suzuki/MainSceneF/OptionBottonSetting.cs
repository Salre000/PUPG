using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionBottonSetting : MonoBehaviour
{
    private Button _volumeButton;
    private Button _kandButton;
    private Button _etcButton;
    private RectTransform _volumeUnderber;
    private RectTransform _kandUnderber;
    private RectTransform _etcUnderber;

    private readonly Vector2 _RESET_SIZE = new Vector2(70.0f, 2.0f);
    private readonly Vector2 _SELECT_SIZE = new Vector2(100.0f, 2.0f);

    private GameObject _volumesObject;
    private GameObject _kandsObject;
    private GameObject _etcsObject;

    private Button _backButton;
    private GameObject _settingPanel;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _volumeButton = GameObject.Find("VolumeTitleText").GetComponent<Button>();
        _volumeUnderber = GameObject.Find("VolUnderBarImage").GetComponent<RectTransform>();

        _kandButton = GameObject.Find("KandTitleText").GetComponent<Button>();
        _kandUnderber = GameObject.Find("KanUnderBarImage").GetComponent<RectTransform>();

        _etcButton = GameObject.Find("EtcTitleText").GetComponent<Button>();
        _etcUnderber = GameObject.Find("EtcUnderBarImage").GetComponent<RectTransform>();

        _volumesObject = GameObject.Find("Volumes").gameObject;
        _kandsObject = GameObject.Find("Kands").gameObject;
        _etcsObject = GameObject.Find("Etcs").gameObject;
        _volumesObject.SetActive(true);
        _kandsObject.SetActive(false);
        _etcsObject.SetActive(false);
        // ƒNƒŠƒbƒN‚µ‚½‚Æ‚«‚ÌŒø‰Ê‚ð’Ç‰Á
        _volumeButton.onClick.AddListener(VolumePanel);
        _kandButton.onClick.AddListener(KandPanel);
        _etcButton.onClick.AddListener(EtcPanel);

        _backButton = GameObject.Find("SettingBackButton").GetComponent<Button>();
        _backButton.onClick.AddListener(SettingBack);
        _settingPanel = GameObject.Find("SettingMode").gameObject;
    }

    private void VolumePanel()
    {
        _volumesObject.SetActive(true);
        _kandsObject.SetActive(false);
        _etcsObject.SetActive(false);

        _volumeUnderber.sizeDelta = _SELECT_SIZE;
        _kandUnderber.sizeDelta = _RESET_SIZE;
        _etcUnderber.sizeDelta = _RESET_SIZE;

    }
    private void KandPanel()
    {
        _volumesObject.SetActive(false);
        _kandsObject.SetActive(true);
        _etcsObject.SetActive(false);

        _volumeUnderber.sizeDelta = _RESET_SIZE;
        _kandUnderber.sizeDelta = _SELECT_SIZE;
        _etcUnderber.sizeDelta = _RESET_SIZE;
    }
    private void EtcPanel()
    {
        _volumesObject.SetActive(false);
        _kandsObject.SetActive(false);
        _etcsObject.SetActive(true);

        _volumeUnderber.sizeDelta = _RESET_SIZE;
        _kandUnderber.sizeDelta = _RESET_SIZE;
        _etcUnderber.sizeDelta = _SELECT_SIZE;
    }

    private void SettingBack()
    {
        _settingPanel.SetActive(false);
    }
}
