using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAnimation : MonoBehaviour
{
    private float _speed = 0.1f;
    [SerializeField,Header("薄暗い背景パネル")]private Transform _backBlackPanel;
    [SerializeField]private Transform _targetBackBlackPanel;
    private bool _isBackBlack = false;
    [SerializeField, Header("GAMEテキスト込みのImage")] private Transform _gameText;
    [SerializeField] private Transform _targetGameText;
    private bool _isGameText = false;
    [SerializeField, Header("MODEテキスト込みのImage")] private Transform _modeText;
    [SerializeField] private Transform _targetModeText;
    private bool _isModeText = false;
    [SerializeField, Header("FlagMode")] private Transform _flagMode;
    [SerializeField] private Transform _targetFlagMode;
    private bool _isFlagMode = false;
    [SerializeField, Header("DeathMode")] private Transform _deathMode;
    [SerializeField] private Transform _targetDeathMode;
    private bool _isDeathMode = false;
    [SerializeField, Header("SpikeMode")] private Transform _spikeMode;
    [SerializeField] private Transform _targetSpikeMode;
    private bool _isSpikeMode = false;

    private float _sqrDistance = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // 背景文字などのアニメーション
        GameTextAnim();
        ModeTextAnim();
        BackBlackAnim();
        // 背景文字などのアニメーションが終わったらモードのアニメーションへ
        if(!_isBackBlack) return;
        // モード選択のアニメーション
        FlagModeAnim();
        DeathModeAnim();
        SpikeModeAnim();
    }

    private void BackBlackAnim()
    {
        if(!_isGameText&&!_isModeText) return;
        if (_isBackBlack) return;
        Vector3 value= _backBlackPanel.position;
        float num = Time.deltaTime + _speed*2;
        value=Vector3.Lerp(_backBlackPanel.position,_targetBackBlackPanel.position,num);
        _backBlackPanel.position = value;
        if((_backBlackPanel.position-_targetBackBlackPanel.position).sqrMagnitude<=_sqrDistance)
            _isBackBlack = true;
    }

    private void GameTextAnim()
    {
        if (_isGameText) return;
        Vector3 value = _gameText.position;
        float num = Time.deltaTime + _speed;
        value = Vector3.Lerp(_gameText.position, _targetGameText.position, num);
        _gameText.position = value;
        if ((_gameText.position - _targetGameText.position).sqrMagnitude <= _sqrDistance)
            _isGameText = true;
    }
    private void ModeTextAnim()
    {
        if (_isModeText) return;
        Vector3 value = _modeText.position;
        float num = Time.deltaTime + _speed;
        value = Vector3.Lerp(_modeText.position, _targetModeText.position, num);
        _modeText.position = value;
        if ((_modeText.position - _targetModeText.position).sqrMagnitude <= _sqrDistance)
            _isModeText = true;
    }
    private void FlagModeAnim()
    {
        if (_isFlagMode) return;
        Vector3 value = _flagMode.position;
        float num = Time.deltaTime + _speed;
        value = Vector3.Lerp(_flagMode.position, _targetFlagMode.position, num);
        _flagMode.position = value;
        if ((_flagMode.position - _targetFlagMode.position).sqrMagnitude <= _sqrDistance)
            _isFlagMode = true;
    }
    private void DeathModeAnim()
    {
        if (_isDeathMode) return;
        Vector3 value = _deathMode.position;
        float num = Time.deltaTime + _speed;
        value = Vector3.Lerp(_deathMode.position, _targetDeathMode.position, num);
        _deathMode.position = value;
        if ((_deathMode.position - _targetDeathMode.position).sqrMagnitude <= _sqrDistance)
            _isDeathMode = true;
    }
    private void SpikeModeAnim()
    {
        if (_isSpikeMode) return;
        Vector3 value = _spikeMode.position;
        float num = Time.deltaTime + _speed;
        value = Vector3.Lerp(_spikeMode.position, _targetSpikeMode.position, num);
        _spikeMode.position = value;
        if ((_spikeMode.position - _targetSpikeMode.position).sqrMagnitude <= _sqrDistance)
            _isSpikeMode = true;
    }
}
