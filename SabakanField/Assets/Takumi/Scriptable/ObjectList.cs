using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/ ObjectList")]
public class ObjectList : ScriptableObject
{
    public List<string>objectName=new List<string>();
    public List<GameObject> objects = new List<GameObject>();
    public List<float>reloadTime=new List<float>();
    public List<int> ÇçagazineBulletCount = new List<int>();
    public List<int> ALLBulletCount = new List<int>();
    public List<float> renge = new List<float>();
    public List<string> Explanation = new List<string>();
}
