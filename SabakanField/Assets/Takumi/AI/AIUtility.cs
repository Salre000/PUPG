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

    public static void SaveData() 
    {
        aIManager.DataSave();
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

    public static List<int> GetKillCount() {  return aIManager.GetKillCount(); }
    public static List <int> GetDeathCount() { return aIManager.GetDeathCount(); }


    public static int GetID() {return aIManager.GetID();}

    public static List<bool> GetPlayersLife() {  return aIManager.GetPlayersLife(); }
    public static List<bool> GetEnemysLife() {return aIManager.GetEnemyLife(); }

}