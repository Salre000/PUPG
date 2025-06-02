using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/SoundList")]
//マップチップのまとめるためのスプリクタブルオブジェクトクラス
public class SoundList : ScriptableObject
{
    public List<AudioClip>SoundLists = new List<AudioClip>();
}
