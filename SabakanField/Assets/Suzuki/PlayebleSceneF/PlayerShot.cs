using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private float _recoil = 0.05f;
    private bool _recoilFlag = false;
    Quaternion cameraQuate= Quaternion.identity;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        Recoil();

    }

    private void Shot()
    {
        if (!PlayerManager.PlayerBulletMagazinCheck())
            return;
        PlayerManager.PlayerBulletShot();
        BulletManager.RayHitTest(transform.position, transform.forward);
        cameraQuate = transform.localRotation;
        cameraQuate.x -= _recoil;
        _recoilFlag=true;
    }

    private void Recoil()
    {
        if (!_recoilFlag) return;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, cameraQuate, 0.1f);
        float num = transform.localRotation.x - cameraQuate.x;
        num-=LookCamera.recoilNum/2;
        if ((num <= 0.005f)) _recoilFlag = false;
    }


}
