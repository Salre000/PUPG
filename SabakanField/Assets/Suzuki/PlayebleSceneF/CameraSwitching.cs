using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    private GameObject _mainCamera = null;
    private GameObject _deadCamera = null;
    private GameObject _uiCamera = null;

    private void Awake()
    {
        _deadCamera = GameObject.Find("DeadCamera").gameObject;
        _mainCamera = GameObject.Find("MainCamera").gameObject;
        _uiCamera = GameObject.Find("UICamera").gameObject;
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
            }
            else
            {
                _mainCamera.SetActive(true);
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
            return;
        }
        _mainCamera.SetActive(false);
        _uiCamera.SetActive(false);
        _deadCamera.SetActive(true);
    }
}
