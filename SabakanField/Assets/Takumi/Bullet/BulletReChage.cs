using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReChage : MonoBehaviour
{
    GameObject player;
    [SerializeField] float renge = 5;

    // Start is called before the first frame update
    private void Awake()
    {
    
        int i=0;
        
    }
    void Start()
    { }

    private void FixedUpdate()
    {
        if (player == null) { player = GameObject.FindWithTag("Player"); return; }

        if (Vector3.Distance(player.transform.position, this.transform.position) < renge)
        {
            if (BulletManager.GetMagazin()>= InfimaGames.LowPolyShooterPack.Character.character.equippedWeapon.GetAmmunitionTotal() * 3) return;
            BulletManager.SetMagazin(InfimaGames.LowPolyShooterPack.Character.character.equippedWeapon.GetAmmunitionTotal() * 3);
            SoundSEManager.instance.PlayReChege();

        }
    }
}
