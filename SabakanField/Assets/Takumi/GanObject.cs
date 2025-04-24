using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GanObject
{
    public static ObjectList constancyGun;
    public static ObjectList extraGan;

    public static void LoodGameObject()
    {
        constancyGun = Resources.Load<ObjectList>("ConstancyGun");
        extraGan = Resources.Load<ObjectList>("ExtraGun");


    }

    public enum ConstancyGanType
    {
        SL_8,
        Classic,
        Stechkin,
        FAR_EYE,
        EyeOfHorus,
        Max
    }
    public enum ExtraGunType
    {
        Dominator,
        Magiclean,
        Alice,
        Max
    }



}