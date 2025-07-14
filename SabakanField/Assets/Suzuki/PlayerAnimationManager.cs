using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    // プレイヤーのアニメーション状況を監視する
    public static PlayerAnimationManager instance;

    private bool walking;       // 歩き中
    private bool running;       // 走り中
    private bool aiming;        // ads中
    private bool reloading;     // リロード中
    private bool inspecting;    // 武器眺め中
    private bool holstering;    // 武器入れ替え中


    private void Awake()
    {
        instance = this;
    }

    public void SetWalking(bool flag) { walking = flag; }
    public void SetRunning(bool flag) { running = flag; }
    public void SetAiming(bool flag) { aiming = flag; }
    public void SetReloading(bool flag) { reloading = flag; }
    public void SetInspecting(bool flag) { inspecting = flag; }
    public void SetHolstering(bool flag) { holstering = flag; }

    public bool IsWalking() { return walking; }
    public bool IsRunning() { return running; }
    public bool IsAiming() { return aiming; }
    public bool IsReloading() { return reloading; }
    public bool IsInspecting() { return inspecting; }
    public bool IsHolstering() { return holstering; }

}
