using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIMove;

public class AI : MonoBehaviour, CharacterInsterface
{

    AIMove move;

    AIStatus status;

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return _PlayerFaction(); }

    //デバッグ用
    public AIMove.NowMode nowMode;
    public AIMove.NowMode nextMode;


    //


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

    }
    public void SetFlag(GameObject game) { move.SetPlayerFlag(game); }
    public void SetEnemyFlag(GameObject flag) { move.SetFlagAngle(flag); }


    public void ReStart() {  move.ReStart(); }
    public void EndShot() {  move.EndShot(); }
    public void Shot() {  move.Shot(); }
    public void Resurrect() {  move.Resurrect(); } 

}
