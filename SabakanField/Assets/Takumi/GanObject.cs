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

    //���[�h�A�E�g����̗񋓑�
    public enum ConstancyGanType
    {
        /// <summary>
        /// <see SL_8="�A�T���g"/>
        /// </summary>
        SL_8,
        /// <summary>
        /// <see Classic="�n���h�K��"/>
        /// </summary>
        Classic,
        /// <summary>
        /// <see Stechkin="���{���o�["/>
        /// </summary>
        Stechkin,
        /// <summary>
        /// <see FAR_EYE="�X�i�C�p�["/>
        /// </summary>
        FAR_EYE,
        /// <summary>
        /// <see EyeOfHorus="�V���b�g�K��"/>
        /// </summary>
        EyeOfHorus,
        //�ő�l
        Max
    }
    //������̗񋓑�
    public enum ExtraGunType
    {
        Dominator,
        Magiclean,
        Alice,
        Max
    }



}