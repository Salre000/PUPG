using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    private GameObject _mainCamera = null;
    private GameObject _debugCamera = null;
    private GameObject _deadCamera = null;
    private GameObject _uiCamera = null;

    private void Awake()
    {
        _debugCamera = GameObject.Find("DebugCamera").gameObject;
        _deadCamera = GameObject.Find("DeadCamera").gameObject;
        _mainCamera = GameObject.Find("MainCamera").gameObject;
        _uiCamera = GameObject.Find("UICamera").gameObject;
        _debugCamera.SetActive(false);
        _deadCamera.SetActive(false);
        _mainCamera.SetActive(true);
        _uiCamera.SetActive(true);
    }

    private void Update()
    {
        Swithing();
        PlayerIsDeadCamera();
    }

    private void Swithing()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_mainCamera.activeSelf)
            {
                _mainCamera.SetActive(false);
                _debugCamera.SetActive(true);
            }
            else
            {
                _mainCamera.SetActive(true);
                _debugCamera.SetActive(false);
            }
        }
    }

    private void PlayerIsDeadCamera()
    {
        if (!PlayerManager.IsPlayerDead())
        {
            if (_mainCamera.activeSelf) return;
            _mainCamera.SetActive(true);
            _uiCamera.SetActive(true);
            _deadCamera.SetActive(false);
            _debugCamera.SetActive(false);
            return;
        }
        _mainCamera.SetActive(false);
        _uiCamera.SetActive(false);
        _deadCamera.SetActive(true);
    }
}
