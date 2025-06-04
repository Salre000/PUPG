using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public static class BulletMoveFunction
{

    private static ObjectPool enemyPaintObjectPool;
    private static ObjectPool playerPaintObjectPool;

    private static ObjectPool aliceBulletObject;
    public static void SetPaint(GameObject enemy, GameObject player, GameObject Alice)
    {
        //�e���������Ƃ��ɏo���v���n�u�̃I�u�W�F�N�g
        enemyPaintObjectPool = new ObjectPool(enemy, 50, "ParentEnemyPaint");
        playerPaintObjectPool = new ObjectPool(player, 50, "ParentPlayerPaint");
        aliceBulletObject = new ObjectPool(Alice, 10, "Alicepaint");
    }

    //�ˌ��̏����̊֐��E���������ΏۂɃC���^�[�t�F�[�X�N���X���t���Ă��鎖���O��
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {


            //���������Ώۂ�ray���������Ƃ��̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
            CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

            //���������Ώۂɖ��G�̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
            InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();


            MIssSoundPlay(new Ray(startPosition, dir), ID);


            //��̓�̃C���^�[�t�F�[�X�N���X�������擾�o�������𔻒�
            if (hitObject == null && invincible == null) { SetPaintObject(hit.point, hit.normal, dir.normalized, playerFaction); return Vector3.zero; }

            //���������Ώۂ����G�Ȃ̂��𔻒�
            if (invincible.GetInvincibleFlag()) return Vector3.zero;



            //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
            if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

            //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
            else hitObject.HitAction();

            //�w���ID�̃L�����N�^�[�̃L���J�E���g�𑝂₷
            AIUtility.AddKillCount(ID);

            return hit.point;
        }

        return Vector3.zero;
    }
    private const int SHOTGAN_PELLET_COUNT = 8;

    //�V���b�g�K���̒e�̋���
    static public Vector3 ShotgunRayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {
        Vector3 LoodAngle = dir;

        for (int i = -SHOTGAN_PELLET_COUNT / 2; i < SHOTGAN_PELLET_COUNT / 2; i++)
        {
            dir = LoodAngle;
            RaycastHit hit;
            dir.y += i / 70.0f;

            float angle = Mathf.Atan2(dir.x, dir.z) + BulletManager.GetRandomAngle(SHOTGAN_PELLET_COUNT / 2, SHOTGAN_PELLET_COUNT / 2);

            dir = new Vector3(Mathf.Sin(angle), dir.y, Mathf.Cos(angle));


            if (Physics.Raycast(startPosition, dir, out hit))
            {


                //���������Ώۂ�ray���������Ƃ��̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //���������Ώۂɖ��G�̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();


                MIssSoundPlay(new Ray(startPosition, dir), ID);

                //��̓�̃C���^�[�t�F�[�X�N���X�������擾�o�������𔻒�
                if (hitObject == null && invincible == null) { SetPaintObject(hit.point, hit.normal, dir.normalized, playerFaction); continue; }

                //���������Ώۂ����G�Ȃ̂��𔻒�
                if (invincible.GetInvincibleFlag()) continue;



                //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
                if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

                //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
                else hitObject.HitAction();

                //�w���ID�̃L�����N�^�[�̃L���J�E���g�𑝂₷
                AIUtility.AddKillCount(ID);

                return hit.point;
            }

        }
        return Vector3.zero;
    }

    private const int PENETRARIONCOUNT = 4;
    //�A���X�̒e�̋���
    static public Vector3 AliceRayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {
        RaycastHit hit;

        for (int i = 0; i < PENETRARIONCOUNT; i++)
        {
            if (Physics.Raycast(startPosition, dir, out hit))
            {

                Debug.DrawLine(startPosition, hit.point, Color.red,10);


                //���������Ώۂ�ray���������Ƃ��̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //���������Ώۂɖ��G�̊֐��������C���^�[�t�F�[�X�N���X���t���Ă���ꍇ�擾
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponentInParent<InvincibleInsterface>();


                MIssSoundPlay(new Ray(startPosition, dir), ID);


                //��̓�̃C���^�[�t�F�[�X�N���X�������擾�o�������𔻒�
                if (hitObject == null && invincible == null)
                {
                    SetAlphaObject(hit.point, hit.normal, dir.normalized);

                    startPosition = hit.point + dir.normalized/10.0f; ;

                }
                else
                {
                    //���������Ώۂ����G�Ȃ̂��𔻒�
                    if (invincible.GetInvincibleFlag()) return Vector3.zero;



                    //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
                    if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

                    //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
                    else hitObject.HitAction();

                    //�w���ID�̃L�����N�^�[�̃L���J�E���g�𑝂₷
                    AIUtility.AddKillCount(ID);

                    return Vector3.zero;
                }

            }
            else
            {
                new GameObject("�ŏI�n�_").transform.position = startPosition;
                return Vector3.zero;
            }


        }
        return Vector3.zero;
    }


    private static void SetPaintObject(Vector3 pos, Vector3 normal, Vector3 normalVec, bool playerFaction)
    {
        GameObject paintObject;
        if (playerFaction) paintObject = playerPaintObjectPool.GetObject();
        else paintObject = enemyPaintObjectPool.GetObject();

        //�y�C���g�̃v���n�u�𐶐�



        //ray�����������@�����p�x�ɕύX
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //�ǂɖ��܂�Ȃ��悤�ɏ���������O�ɏo��
        paintObject.transform.position = pos - normalVec / 100.0f;


    }
    private static void SetAlphaObject(Vector3 pos, Vector3 normal, Vector3 normalVec)
    {
        GameObject paintObject;

        //���߃I�u�W�F�N�g�̃v���n�u�𐶐�
        paintObject = aliceBulletObject.GetObject();



        //ray�����������@�����p�x�ɕύX
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //�ǂɖ��܂�Ȃ��悤�ɏ���������O�ɏo��
        paintObject.transform.position = pos - normalVec / 50.0f;


    }

    //�e���߂���ʂ����G�ɉ����Ȃ炷
    private static void MIssSoundPlay(Ray ray, int ID)
    {


        List<GameObject> list = AIUtility.GetChracterALL();

        for (int j = 0; j < list.Count; j++)
        {
            //���𕷂��K�v�̖���AI�͖�������
            if (j != 0) continue;

            //�ˌ������{�l�ɂ͕������Ȃ��悤��
            if (j == ID) continue;

            //�߂���ʂ������ǂ����̔���
            if (DistanceToLine(ray, list[j].transform.position) > 2) continue;

            //�����Ȃ炷
            //���݂̃v���C�l����1�l�̈׃v���C���[�����ŗǂ��������l�ɂȂ�����ύX������

            SoundManager.StartSound(list[j].transform.position
                , SoundManager.GetInGameSoundList(SoundEnum.SoundEnumType._bulletSoundMissShot));



        }

    }

    public static float DistanceToLine(Ray ray, Vector3 point)
    {
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }

}