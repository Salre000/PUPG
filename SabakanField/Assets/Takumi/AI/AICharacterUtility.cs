using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AICharacterUtility 
{

    private static List<AI> characterAI = new List<AI>();

    public static AIStatus GetCharacterAI(int id) {return characterAI[id].GetStatus(); }
    public static void ClearCharacterAI() {  characterAI.Clear(); }

    public static void AddAI(AI ai) {  characterAI.Add(ai); }

    public static void SetShotFlag(int id, bool flag) { characterAI[id].SetShotFlag(flag); }

    public static bool GetPlayerFaction(int id) {return characterAI[id].PlayerFaction(); }

}