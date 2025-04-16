using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    GameObject mainCamera;
    [SerializeField] GameObject weapon;


    [SerializeField, Header("一度取得されてからもう一度取得できるようになるまでの時間")] float _reCastTime = 10;

    //時間経過を伴うタスク
    private List<System.Func<bool>> TimeTask = new List<System.Func<bool>>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        bool returnFlag = false;
        for (int i = 0; i < TimeTask.Count; i++)
        {
            if (TimeTask[i] != null)
            {
                if (!TimeTask[i]()) continue;

                returnFlag = true;
                TimeTask.RemoveAt(i);
                i--;

            }


        }
        if (returnFlag) return;


        WaitingForAcquisition();
    }

    //取得待ちをする関数
    private void WaitingForAcquisition()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.transform.gameObject != this.gameObject) return;

            if (!Input.GetKeyDown(KeyCode.E)) return;

            Debug.Log("武器を入手した");
            GetWeapon();
            TimeTask.Add(Smaller);

        }


    }

    private void GetWeapon()
    {
        weapon.SetActive(false);



    }

    private bool Smaller()
    {
        this.transform.localScale -= new Vector3(0, 0.005f, 0);

        if (this.transform.localScale.y > 0.02f) return false;

        timeCounter = 0;
        TimeTask.Add(TimeCount);

        return true;

    }
    float timeCounter = 0;
    private bool TimeCount()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter < _reCastTime) return false;

        TimeTask.Add(GrowInSize);

        return true;

    }

    private bool GrowInSize()
    {
        this.transform.localScale += new Vector3(0, 0.005f, 0);

        if (this.transform.localScale.y < 1f) return false;

        Vector3 size = this.transform.localScale;
        size.y = 1;
        this.transform.localScale = size;

        weapon.SetActive(true);

        return true;

    }



}
