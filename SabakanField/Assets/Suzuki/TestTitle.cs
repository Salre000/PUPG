using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTitle : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnButton());
    }

    private void OnButton()
    {
        GameSceneManager.LoadScene(GameSceneManager.flagSceneName);
    }
}
