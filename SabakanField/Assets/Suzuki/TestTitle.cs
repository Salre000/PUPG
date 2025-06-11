using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TestTitle : MonoBehaviour
{
    private UnityEngine.UI.Button _button;

    private void Awake()
    {
        UnityEngine.Cursor.visible = true;

        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(() => OnButton());
    }

    public void OnButton()
    {
        GameSceneManager.LoadScene(GameSceneManager.lobbyScene,LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
}
