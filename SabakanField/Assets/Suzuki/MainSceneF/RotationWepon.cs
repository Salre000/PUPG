using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWepon : MonoBehaviour
{
    float time = 30.0f;
    float rotate=0.0f;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotate += time * Time.deltaTime;
        rotate %= 360.0f;
        transform.rotation = Quaternion.Euler(0.0f, rotate, 0.0f);
    }
}
