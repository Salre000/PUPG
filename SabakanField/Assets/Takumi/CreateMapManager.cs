using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クリエイトマップのマネージャー
public static class CreateMapManager 
{
    public static CreateMap createMap;

    public static GameObject GetFlag(int number) { return createMap.GetFlag(number); }


}
