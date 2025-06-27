using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TestManager : MonoBehaviour
{
    public static float takasa = 0;
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
