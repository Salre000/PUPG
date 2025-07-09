using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    public static DeathCamera Instance;

    public static GameObject traget;

    public void Awake()
    {
        Instance = this;
    }
    public void OnDisable()
    {
        traget?.GetComponent<AI>()?.ChengeOutLIne(false);
        
    }
    public void OnEnable()
    {
        traget?.GetComponent<AI>()?.ChengeOutLIne(true);
    }
    private void FixedUpdate()
    {
        transform.LookAt(traget.transform.position+new Vector3(0,1,0));

        if (Vector3.Distance(traget.transform.position, transform.position) < 5) return;

        transform.position += transform.forward/10f;


    }

}
