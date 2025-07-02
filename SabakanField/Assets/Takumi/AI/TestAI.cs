using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIJobBase;

public class TestAI : MonoBehaviour
{
    public int i=0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = (GameObject.Find("Tage").transform.position) - (transform.position);
        vec.Normalize();

        AIIK aiIK = GetComponent<AIIK>();
        aiIK.SetIK(i);
      
        aiIK.SetRightPos(vec / 2f + transform.position + offSet + transform.right/15f);
        aiIK.SetLeftPos(vec / 2f + transform.position + offSet);

        ////YÇæÇØÇçlÇ¶ÇÈ
        aiIK.SetLeftRotate(new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, 90));
        aiIK.SetRightRotate(new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, -90));

    }
}
