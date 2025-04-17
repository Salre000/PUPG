using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIMove;

public class AI : MonoBehaviour, CharacterInsterface,InvincibleInsterface
{

    AIMove move;

    AIStatus status;

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return _PlayerFaction(); }

    //デバッグ用
    public AIMove.NowMode nowMode;
    public AIMove.NowMode nextMode;


    private bool invincible=false;
    private float invincibleTime = 0;

    public void Initialization()
    {

        move=new AIMove();
        status=new AIStatus();

        status.Start(this.gameObject);
        move.SetThisGameObject(this.gameObject);

        move.Start();

        AICharacterUtility.AddAI(status);

        move.SetID(status.GetID());

        move.SetPlayerFaction(PlayerFaction);

    }

    public void FixedUpdate()
    {

        InvincibleCount();

        nowMode = move.nowMode;
        nextMode = move.nextMode;

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


    public void ReStart() {  move.ReStart(); }
    public void EndShot() {  move.EndShot(); }
    public void Shot() {  move.Shot(); }
    public void Resurrect() {  move.Resurrect(); }

    public bool Invincible()
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
