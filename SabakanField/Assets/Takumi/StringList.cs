using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/ StringList")]
public class StringList : ScriptableObject
{
    public List<string> objects = new List<string>();   
    public List<int> type = new List<int>();
}
