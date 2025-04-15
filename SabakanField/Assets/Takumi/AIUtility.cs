using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtility 
{
    public static  AIManager aIManager;

    public static List<AIMove> GetRelativeEnemy(bool isPlayerTeam) 
    {
        return aIManager.GetRelativeEnemy(isPlayerTeam);
    }

}