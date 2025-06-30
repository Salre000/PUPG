using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAnimation : MonoBehaviour
{
    private float _speed = 0.1f;
    [SerializeField,Header("薄暗い背景パネル")]private Transform _backBlackPanel;
    [SerializeField]private Transform _targetBackBlackPanel;
    private bool _isBackBlack = false;
    [SerializeField, Header("GAMEテキスト")] private Transform _gameText;
    [SerializeField] private Transform _targetGameText;
    private bool _isGameText = false;
    [SerializeField, Header("MODEテキスト")] private Transform _modeText;
    [SerializeField] private Transform _targetModeText;
    private bool _isModeText = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackBlackAnim();
    }

    private void BackBlackAnim()
    {
        if (_isBackBlack) return;
        Vector3 value= _backBlackPanel.position;
        float num = Time.time + _speed;
        value=Vector3.Lerp(_backBlackPanel.position,_targetBackBlackPanel.position,num);
        _backBlackPanel.position = value;
        if((_backBlackPanel.position-_targetBackBlackPanel.position).sqrMagnitude<=0.1f)
            _isBackBlack = true;
    }

    private void GameTextAnim()
    {
        if (_isGameText) return;
        Vector3 value = _gameText.position;
        float num = Time.time + _speed;
        value = Vector3.Lerp(_gameText.position, _targetGameText.position, num);
        _gameText.position = value;
        if ((_gameText.position - _targetGameText.position).sqrMagnitude <= 0.1f)
            _isGameText = true;
    }
}
