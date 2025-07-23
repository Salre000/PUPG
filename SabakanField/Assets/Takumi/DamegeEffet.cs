using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeEffet : MonoBehaviour
{
    float time = 0;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        time = 0;
    }
    private void OnDisable()
    {
        time = 0;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 3) return;
        gameObject.SetActive(false);
    }
}
