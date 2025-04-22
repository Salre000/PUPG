using UnityEngine;
using UnityEngine.UI;

public class OptionBottonSetting : MonoBehaviour
{
    private Button _volumeButton;
    private Button _sensitivityButton;
    private Button _etcButton;
    private RectTransform _volumeUnderber;
    private RectTransform _sensitivityUnderber;
    private RectTransform _etcUnderber;

    private readonly Vector2 _RESET_SIZE = new Vector2(70.0f, 2.0f);
    private readonly Vector2 _SELECT_SIZE = new Vector2(100.0f, 2.0f);

    private GameObject _volumesObject;
    private GameObject _sensitivitysObject;
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

        _sensitivityButton = GameObject.Find("SensitivityTitleText").GetComponent<Button>();
        _sensitivityUnderber = GameObject.Find("SensitivityUnderBarImage").GetComponent<RectTransform>();

        _etcButton = GameObject.Find("EtcTitleText").GetComponent<Button>();
        _etcUnderber = GameObject.Find("EtcUnderBarImage").GetComponent<RectTransform>();

        _volumesObject = GameObject.Find("Volumes").gameObject;
        _sensitivitysObject = GameObject.Find("Sensitivitys").gameObject;
        _etcsObject = GameObject.Find("Etcs").gameObject;
        _volumesObject.SetActive(true);
        _sensitivitysObject.SetActive(false);
        _etcsObject.SetActive(false);
        // ƒNƒŠƒbƒN‚µ‚½‚Æ‚«‚ÌŒø‰Ê‚ð’Ç‰Á
        _volumeButton.onClick.AddListener(VolumePanel);
        _sensitivityButton.onClick.AddListener(SensitivityPanel);
        _etcButton.onClick.AddListener(EtcPanel);

        _backButton = GameObject.Find("SettingBackButton").GetComponent<Button>();
        _backButton.onClick.AddListener(SettingBack);
        _settingPanel = GameObject.Find("SettingMode").gameObject;
    }

    private void VolumePanel()
    {
        _volumesObject.SetActive(true);
        _sensitivitysObject.SetActive(false);
        _etcsObject.SetActive(false);

        _volumeUnderber.sizeDelta = _SELECT_SIZE;
        _sensitivityUnderber.sizeDelta = _RESET_SIZE;
        _etcUnderber.sizeDelta = _RESET_SIZE;

    }
    private void SensitivityPanel()
    {
        _volumesObject.SetActive(false);
        _sensitivitysObject.SetActive(true);
        _etcsObject.SetActive(false);

        _volumeUnderber.sizeDelta = _RESET_SIZE;
        _sensitivityUnderber.sizeDelta = _SELECT_SIZE;
        _etcUnderber.sizeDelta = _RESET_SIZE;
    }
    private void EtcPanel()
    {
        _volumesObject.SetActive(false);
        _sensitivitysObject.SetActive(false);
        _etcsObject.SetActive(true);

        _volumeUnderber.sizeDelta = _RESET_SIZE;
        _sensitivityUnderber.sizeDelta = _RESET_SIZE;
        _etcUnderber.sizeDelta = _SELECT_SIZE;
    }

    private void SettingBack()
    {
        _settingPanel.SetActive(false);
    }
}
