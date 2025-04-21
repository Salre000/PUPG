using UnityEngine;

public static class BulletMoveFunction 
{

    private static PaintObjectPool enemyPaintObjectPool;
    private static PaintObjectPool playerPaintObjectPool;
    public static void SetPaint(GameObject enemy,GameObject player) 
    {
        //�e���������Ƃ��ɏo���v���n�u�̃I�u�W�F�N�g
        enemyPaintObjectPool=new PaintObjectPool(enemy,20, "ParentEnemyPaint");
        playerPaintObjectPool=new PaintObjectPool(player,20,"ParentPlayerPaint");
    }

    //�ˌ��̏����̊֐��E���������ΏۂɃC���^�[�t�F�[�X�N���X���t���Ă��鎖���O��
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir,bool playerFaction=true,int ID=0)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {
          

            //���������Ώۂ�ray���������Ƃ��̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
            CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

            //���������Ώۂɖ��G�̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
            InvincibleInsterface invincible =hit.transform.gameObject.GetComponent<InvincibleInsterface>();

            //��̓�̃C���^�[�t�F�[�X�N���X�������擾�o�������𔻒�
            if (hitObject == null && invincible == null) { SetPaintObject(hit.point, hit.normal, dir.normalized, playerFaction); return Vector3.zero; }

            //���������Ώۂ����G�Ȃ̂��𔻒�
            if (invincible.GetInvincibleFlag()) return Vector3.zero;



            //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
            if (hitObject.PlayerFaction()== playerFaction) hitObject.HitActionFriendlyFire();

            //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
            else hitObject.HitAction();

            //�w���ID�̃L�����N�^�[�̃L���J�E���g�𑝂₷
            AIUtility.AddKillCount(ID);

            return hit.point;
        }

        return Vector3.zero;
    }

    private static void SetPaintObject(Vector3 pos, Vector3 normal,Vector3 normalVec,bool playerFaction)
    {
        GameObject paintObject;
        if (playerFaction) paintObject = playerPaintObjectPool.GetpaintObject();
        else paintObject = enemyPaintObjectPool.GetpaintObject();

        //�y�C���g�̃v���n�u�𐶐�



            //ray�����������@�����p�x�ɕύX
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //�ǂɖ��܂�Ȃ��悤�ɏ���������O�ɏo��
        paintObject.transform.position = pos- normalVec/100.0f;


    }

}