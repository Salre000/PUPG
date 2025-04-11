using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField]
    private float _deadzoneX = 0.1f;
    [SerializeField]
    private float _deadzoneY = 0.1f;
    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        MouseCameraRotation();
    }

    private void MouseCameraRotation()
    {
        //マウスカーソルで左右視点+横回転
        float mouseCameraX = Input.GetAxis("Mouse X");
        if (Mathf.Abs(mouseCameraX) > _deadzoneX) 
        {
            player.transform.RotateAround(player.transform.position, Vector3.up, mouseCameraX); 

        }
        float mauseCameraY = Input.GetAxis("Mouse Y");
        // 
        if (Mathf.Abs(mauseCameraY) > _deadzoneY)
        {
            transform.RotateAround(transform.position, Vector3.right, -mauseCameraY);
        }
        Quaternion vector3 = transform.rotation;
        vector3.y = 0f;
        vector3.z = 0f;
        transform.localRotation = vector3;
    }

}
