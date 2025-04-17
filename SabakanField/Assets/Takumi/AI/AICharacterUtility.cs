using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AICharacterUtility 
{
    private static List<AIStatus> characterAI=new List<AIStatus>();

    public static AIStatus GetCharacterAI(int id) { Debug.Log(id); return characterAI[id]; }

    public static void AddAI(AIStatus ai) {  characterAI.Add(ai); }

}