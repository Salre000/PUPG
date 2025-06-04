using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    private readonly int PREFAB_COUNT;

    private GameObject paintObjectOrigen;

    //�I�u�W�F�N�g�v�[��
    private List<GameObject> paintList = new List<GameObject>(50);

    //���ݎg�p���Ă��Ȃ��I�u�W�F�N�g��n���֐�
    public GameObject GetObject() 
    {
        for(int i = 0; i < paintList.Count; i++) 
        {
            if (paintList[i].activeSelf) continue;

            paintList[i].SetActive(true);

            return paintList[i];

        }

        return null;
    }

    public ObjectPool(GameObject prefab, int prefabCount = 20,string name= "parentObject") 
    {
        paintObjectOrigen = prefab;

        PREFAB_COUNT = prefabCount;

        GameObject parentObject = new GameObject(name);

        for (int i = 0; i < PREFAB_COUNT; i++) 
        {
            GameObject paint=GameObject.Instantiate(paintObjectOrigen, parentObject.transform);

            paint.SetActive(false);

            paintList.Add(paint);
        }


    }


}