using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFPS : MonoBehaviour
{

    private void FixedUpdate()
    {
        Application.targetFrameRate = 120;
    }
}
