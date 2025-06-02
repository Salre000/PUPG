using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIMove;

public class AI : MonoBehaviour, CharacterInsterface,InvincibleInsterface
{

    [SerializeField]private GameObject gunObject;

    public void SetGanObject(GameObject gameObject) {  gunObject = gameObject; }

    [SerializeField] private GameObject leftHand;
    public GameObject GetLeftHand() { return leftHand; }

    [SerializeField] private GameObject rightHand;
    public GameObject GetRightHand() { return rightHand; }

    private CapsuleCollider capsuleCollider;

    AIMove move;

    AIStatus status;
    public AIStatus GetStatus() {  return status; }

    AIShot shot;
    public AIShot GetIShot() { return shot; }

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return GameModes.mode != PublicEnum.GameMode.deathmatch ? _PlayerFaction() : false; }

    //デバッグ用
    public AIMove.NowMode nowMode;
    public AIMove.NowMode nextMode;

    public bool shotShotFlag;
    public bool moveShotFlag;
    //

    private float randomRenge = 5;
    public void SetRandomRenge(float renge) {  randomRenge = renge; }

    private bool invincible=false;
    private float invincibleTime = 0;

    private int _maxBullet = -1;
    //デバッグ用
    [SerializeField]private int emainingBullet = -1;

    private void ReLood() { emainingBullet = _maxBullet; }
    public void SetBullet(int max) 
    { _maxBullet = max; ReLood(); }

    public void Initialization()
    {
        AICharacterUtility.AddAI(this);

        move=new AIMove();
        status=new AIStatus();
        shot=new AIShot();

        shot.SetGameObject(this.gameObject);
        shot.SetGanObject(gunObject);
        status.Start(this.gameObject);
        move.SetThisGameObject(this.gameObject);



        move.Start();

        shot.SetID(status.GetID());
        move.SetID(status.GetID());

        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void FixedUpdate()
    {

        InvincibleCount();
        MagazineChaeck();
        //  デバッグ用
        nowMode = move.nowMode;
        nextMode = move.nextMode;
        shotShotFlag = shot.shotingFlag;
        moveShotFlag=move.shotingFlag;
        //

        move.FixedUpdate();


    }

    public void OnCollisionStay(Collision collision)
    {
        if (!status.GetISLife()) return;
        
        move.HitObject(collision);
    }

    public void HitAction()
    {
        status.HitAction();
        invincible=true;
        capsuleCollider.enabled=false;
    }
    public void SetFlag(GameObject game) { move.SetPlayerFlag(game); }
    public void SetEnemyFlag(GameObject flag) { move.SetFlagAngle(flag); }

    public void SetShotFlag(bool flag) {  move.SetShotFlag(flag);shot.SetShotFlag(flag); }


    public void ReStart() {  move.ReStart(); }
    public void EndShot() {  move.EndShot(); }
    public void Shot() { shot.Shot(randomRenge); emainingBullet--; }
    public void Resurrect() { move.Resurrect(() => {capsuleCollider.enabled = true;ReLood(); }); }
    public void ReLoodAnime() { ReLood(); nowReLood = false; }
    public bool GetInvincibleFlag()
    {

        return invincible;

    }

    private void InvincibleCount() 
    {
        if (!invincible) return;

        if (!status.GetISLife()) return;


        invincibleTime += Time.deltaTime;
        
        if(invincibleTime>=1)invincible = false;

    }
    private bool nowReLood = false;
    private void MagazineChaeck() 
    {
        if (emainingBullet > 0) return;
        if (!status.GetISLife()) return;
        if (nowReLood) return;
        status.SetAnimatorTrigger("ReLood");
        nowReLood = true;

    }

}
