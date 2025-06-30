using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TestTitle : MonoBehaviour
{
    private UnityEngine.UI.Button _button;
    [SerializeField] private Transform _titles;
    [SerializeField] private Transform _targetTrans;
    private float _speed = 0.1f;
    private bool isClick = false;
    private bool isComp = false;

    private void Awake()
    {
        UnityEngine.Cursor.visible = true;

        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(() => OnButton());
    }

    public void OnButton()
    {
        isClick = true;
    }

    private void Update()
    {
        TitleMove();
        CompisLoad();
    }

    private void TitleMove()
    {
        if(!isClick||isComp) return;
        Vector3 value = _titles.position;
        float time = Time.time * _speed;
        value = Vector3.Lerp(_titles.position, _targetTrans.position, time);
        _titles.position = value;
        if((_titles.position-_targetTrans.position).sqrMagnitude <= 0.1f)
            isComp = true;
    }

    private void CompisLoad()
    {
        if(!isComp) return ;
        GameSceneManager.LoadScene(GameSceneManager.modeScene, LoadSceneMode.Additive);
        isComp = false;
        isClick = false;
        gameObject.SetActive(false);
    }
}
