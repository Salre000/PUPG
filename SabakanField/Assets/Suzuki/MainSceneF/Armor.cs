using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    // アーマーを持っていたら一度攻撃を防ぐようにする

    // アーマーを装着した時の音、初めから装着している場合は鳴らさない
    [SerializeField, Header("アーマーを装着した時の音")] private AudioClip _getArmorSound;
    // アーマーが無くなった時の音
    [SerializeField, Header("アーマーが無くなった時の音")] private AudioClip _armorBreakSound;
    // アーマーを現在装着しているか
    [SerializeField] private bool _isArmor = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.SetIsArmor(_isArmor);
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用アーマー付与
        if (Input.GetKeyDown(KeyCode.M))
        {
            _isArmor = true;
        }
        if (PlayerManager.GetIsArmor() != _isArmor)
        {
            PlayerManager.SetIsArmor(_isArmor);
        }
    }

    // 
    private void Exesute()
    {

    }
}
