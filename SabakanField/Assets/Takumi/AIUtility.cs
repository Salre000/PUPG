using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtility 
{
    public static  AIManager aIManager;

    public static List<GameObject> GetRelativeEnemy(bool isPlayerTeam) 
    {
        return aIManager.GetRelativeEnemy(isPlayerTeam);
    }

    public static void SaveData(float time) 
    {
        aIManager.DataSave(time);
    }

    public static void AddKillCount(int index=0) 
    {
        aIManager.AdDKillCount(index);
    }

    public static void AddDeathCount(int index=0) 
    {
        aIManager.AddDeathCount(index);
    }
    public static Vector3 GetFlagPosition() { return aIManager.PlayerFlagPosition(); }

}