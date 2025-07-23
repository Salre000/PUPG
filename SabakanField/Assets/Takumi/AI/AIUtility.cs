using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtility 
{
    public static  AIManager aIManager;

    public static List<GameObject> GetRelativeEnemy(bool isPlayerTeam) 
    {
        return null;//aIManager.GetRelativeEnemy(isPlayerTeam);
    }

    public static List<GameObject> GetChracterALL() { return aIManager.GetchracterALL(); }
    public static void SaveData() 
    {
        aIManager.DataSave();
    }

    public static void AddKillCount(int index=0) 
    {
        aIManager.AdDKillCount(index);
    }

    public static void AddDeathCount(int index = 0)
    {
        aIManager.AddDeathCount(index);
    }
    public static void AddAssertCount(int index = 0)
    {
        aIManager.AddAssertCount(index);
    }
    public static void AddList(int id, int Enemyid) { aIManager.AddList(id, Enemyid); }
    public static void Assist(int id,int killer) 
    {


        aIManager.Assist(id, killer);


    }
    public static void DamageEffect(GameObject enemy) { aIManager.DamageEffect(enemy); }

    public static Vector3 GetFlagPosition() { return aIManager.PlayerFlagPosition(); }

    public static List<int> GetKillCount() {  return aIManager.GetKillCount(); }
    public static List <int> GetDeathCount() { return aIManager.GetDeathCount(); }


    public static int GetID() {return aIManager.GetID();}

    public static List<bool> GetPlayersLife() {  return aIManager.GetPlayersLife(); }
    public static List<bool> GetEnemysLife() {return aIManager.GetEnemyLife(); }

    public static GameObject GetFlag(int id) { return aIManager.GetFlag(id); }

    public static TeamAIManager GetTeamAIManager(int index) { return aIManager.GetEnemyTime(index); }

    public static List<AI>GetEnemyAI(int id) { return aIManager.GetEnemyAI(id); }

    public static GameObject GetPlayer() {  return aIManager.GetPlayer(); }
}