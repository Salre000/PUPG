using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AICharacterUtility
{

    private static List<AI> characterAI = new List<AI>();
    public static void ClearCharacterAI() { characterAI.Clear(); }

    public static void AddAI(AI ai) { characterAI.Add(ai); }
    public static void ResetAI() { characterAI.Clear(); }

    public static int CharacterCount() { return characterAI.Count; }
    public static List<AI> GetAIS() { return characterAI; }

    public static void ChengeOutLine(int ID,bool flag) 
    {
        characterAI[ID].ChengeOutLIne(flag);
    }

}