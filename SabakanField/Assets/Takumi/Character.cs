using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, CharacterInsterface
{
    public void HitAction()
    {
        AIUtility.AddDeathCount();

        //���X�|�[���̏���

        this.transform.position = AIUtility.GetFlagPosition();


    }
}
