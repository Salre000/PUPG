using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInsterface
{
    virtual public bool PlayerFaction() { return true; }


    //�e���������Ƃ��̏���
    abstract public void HitAction();

    //��������e�𓖂Ă�ꂽ���̏����i�������Ȃ���Βe�����������Ɠ����j
    virtual public void HitActionFriendlyFire() { HitAction(); }

}
