using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAngleObserver : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float offSet=0.0f;
    Vector3 offSetVec = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offSetVec.y = offSet;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.eulerAngles=player.eulerAngles+ offSetVec;
    }
}
