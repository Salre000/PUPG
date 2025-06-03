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

    //ロードアウト武器の列挙体
    public enum ConstancyGanType
    {
        /// <summary><see SL_8="アサルトライフル"/> </summary>
        SL_8,
        /// <summary><see Classic="ハンドガン"/> </summary>
        Classic,
        /// <summary><see Stechkin="リボルバー"/> </summary>
        Stechkin,
        /// <summary><see FAR_EYE="スナイパーライフル"/> </summary>
        FAR_EYE,
        /// <summary><see EyeOfHorus="ショットガン"/> </summary>
        EyeOfHorus,
        //最大値
        Max
    }

    public static int[] GanBulletCount = { 25, 12, 6, 5, 4 };
    public static int[] ExtraGunBulletCount = { 255, 400 / 5, 3 };

    //強武器の列挙体
    public enum ExtraGunType
    {
        Dominator,
        Magiclean,
        Alice,
        Max
    }



}