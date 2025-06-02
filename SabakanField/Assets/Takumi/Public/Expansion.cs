using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Expansion
{
    public static bool GetADSType(this OptionSaveData.adsType adsType) 
    {

        switch (adsType)
        {
            case OptionSaveData.adsType.LongPress:

                return false;
            case OptionSaveData.adsType.Switch:
                return true;
                
        }

        return true;

    }

    public static void SetADSType(ref OptionSaveData.adsType adsType, bool flag) 
    {
        if(flag) adsType=OptionSaveData.adsType.Switch;
        else adsType = OptionSaveData.adsType.LongPress;


    }


}