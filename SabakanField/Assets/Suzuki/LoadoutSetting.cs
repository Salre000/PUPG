using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutSetting : MonoBehaviour
{
    [SerializeField] private GameObject _loadoutObject;
    private int _loadoutIndex = 8;
    [SerializeField] private GameObject _content;
    private float _set = 400;
    private List<RawImage> _weponImage = new(8);
    [SerializeField] private List<Texture> _weponRawTexture;

    private void Awake()
    {
        for(int i=0;i<_loadoutIndex;i++)
        {
            _weponImage.Add(null);
            GameObject gameObject = Instantiate(_loadoutObject, _content.transform);
            _weponImage[i] = gameObject.transform.GetChild(0).GetComponent<RawImage>();
            _weponImage[i].texture=_weponRawTexture[i];
            if (i==0) continue;
            gameObject.transform.localPosition=
                new Vector3(gameObject.transform.localPosition.x
                            , gameObject.transform.localPosition.y-_set
                            , gameObject.transform.localPosition.z);
            _set += 400.0f;
        }

    }
}
