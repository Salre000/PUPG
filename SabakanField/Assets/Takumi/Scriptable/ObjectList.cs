using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/ ObjectList")]
public class ObjectList : ScriptableObject
{
    public List<string>objectName=new List<string>();
    public List<GameObject> objects = new List<GameObject>();
}
