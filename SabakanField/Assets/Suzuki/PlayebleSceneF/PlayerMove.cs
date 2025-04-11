using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float currentSpeed = 30.0f;

    void Update()
    {
        // Playerの前後左右の移動
        float xMovement = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime; // 左右の移動
        float zMovement = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime; // 前後の移動
        transform.Translate(xMovement, 0, zMovement); // オブジェクトの位置を更新



    }
}
