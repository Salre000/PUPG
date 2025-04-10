using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebagMove : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) animator.SetTrigger("Left");
        if (Input.GetKeyDown(KeyCode.RightArrow)) animator.SetTrigger("Right");
        
    }
}
