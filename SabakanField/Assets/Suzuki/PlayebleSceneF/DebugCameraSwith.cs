using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraSwith : MonoBehaviour
{
    private GameObject _mainCamera = null;
    private GameObject _debugCamera = null;
    private void Awake()
    {
        _debugCamera = GameObject.Find("DebugCamera").gameObject;
        _mainCamera = GameObject.Find("MainCamera").gameObject;
        _debugCamera.SetActive(false);
    }

    private void Update()
    {
        Swithing();
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
}
