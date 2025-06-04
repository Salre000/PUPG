using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitObject : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject enemyObject;
    [SerializeField] GameObject aliceObject;
    public void Awake()
    {
        BulletMoveFunction.SetPaint(enemyObject, playerObject, aliceObject);
    }
}
