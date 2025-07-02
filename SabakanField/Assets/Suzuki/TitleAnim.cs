using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnim : MonoBehaviour
{
    [SerializeField] private Transform _titles;
    [SerializeField] private Transform _targetTrans;
    [SerializeField] private GameObject _anyKeyText;
    private float _speed = 5.5f;
    private bool isComp = false;
    private void Update()
    {
        TitleMove();
    }
    private void TitleMove()
    {
        if (_anyKeyText.activeSelf) return;
        if (isComp) return;
        Vector3 value = _titles.position;
        float time = Time.deltaTime * _speed;
        value = Vector3.Lerp(_titles.position, _targetTrans.position, time);
        _titles.position = value;
        if ((_titles.position - _targetTrans.position).sqrMagnitude <= 0.3f)
        {
            isComp = true;
            GameSceneManager.LoadScene(GameSceneManager.modeScene, LoadSceneMode.Additive);
        }
    }
}
