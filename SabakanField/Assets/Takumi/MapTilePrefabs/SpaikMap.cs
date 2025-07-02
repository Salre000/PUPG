using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-101)]
public class SpaikMap : MonoBehaviour
{
    public static SpaikMap Instance;
    public void Awake()
    {
        Instance = this;
    }

    [SerializeField, Header("スタートポイント")] GameObject[] StartPos = new GameObject[2];
    public GameObject GetStartPos(int index) { return StartPos[index]; }
    [SerializeField, Header("確保ポイント")] GameObject[] SpaikArea = new GameObject[2];
    public GameObject GetSpaikArea(int index) { return SpaikArea[index]; }

    [SerializeField, Header("強ポジ")] GameObject point;
    public GameObject GetPoint(int index) { return point; }

}
