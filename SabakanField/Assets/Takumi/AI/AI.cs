using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIMove;

public class AI : MonoBehaviour, CharacterInsterface,InvincibleInsterface
{

    [SerializeField]private GameObject gunObject;

    AIMove move;

    AIStatus status;
    public AIStatus GetStatus() {  return status; }

    AIShot shot;

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return _PlayerFaction(); }

    //デバッグ用
    public AIMove.NowMode nowMode;
    public AIMove.NowMode nextMode;

    public bool shotShotFlag;
    public bool moveShotFlag;
    //

    private bool invincible=false;
    private float invincibleTime = 0;

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

    }

    public void FixedUpdate()
    {

        InvincibleCount();

        nowMode = move.nowMode;
        nextMode = move.nextMode;
        shotShotFlag = shot.shotingFlag;
        moveShotFlag=move.shotingFlag;

        move.FixedUpdate();


    }

    public void OnCollisionEnter(Collision collision)
    {
        move.HitObject(collision);
    }

    public void HitAction()
    {
        status.HitAction();
        invincible=true;

    }
    public void SetFlag(GameObject game) { move.SetPlayerFlag(game); }
    public void SetEnemyFlag(GameObject flag) { move.SetFlagAngle(flag); }

    public void SetShotFlag(bool flag) {  move.SetShotFlag(flag);shot.SetShotFlag(flag); }


    public void ReStart() {  move.ReStart(); }
    public void EndShot() {  move.EndShot(); }
    public void Shot() {  shot.Shot(); }
    public void Resurrect() {  move.Resurrect(); }

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


}
