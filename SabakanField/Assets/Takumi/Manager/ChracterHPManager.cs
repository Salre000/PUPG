using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChracterHPManager : MonoBehaviour
{
    private readonly float MAXHP = 100;
    public static ChracterHPManager instance;

    [SerializeField]private List<float>HPList=new List<float>();    

    public void AddHP(float hp){ HPList.Add(hp); }
    public float GetHp(int id) { if (HPList.Count <= id||id<0) return 100; return HPList[id]; }
    public void GetDamage(int id,float damage) { HPList[id]-=damage; }

    public void ResetHP(int id) { HPList[id] = MAXHP; }
    public float GetMaxHp() { return MAXHP; }


    public void Awake()
    {
        instance = this;
    }



}
