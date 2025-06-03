using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GanObject;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LoadoutSetting : MonoBehaviour
{
    [SerializeField] private GameObject _loadoutCamera;
    [SerializeField] private GameObject _loadoutCanvas;
    [SerializeField] private GameObject _loadoutObject;
    private int _loadoutIndex = (int)ConstancyGanType.Max;
    [SerializeField] private GameObject _content;
    private float _set = 400;
    // �ʏ핐��
    private List<RawImage> _weponImage = new((int)ConstancyGanType.Max);
    [SerializeField] private List<Texture> _weponRawTexture;

    [SerializeField] private GameObject _player;
    [SerializeField, Header("P_LPSP_Inventory<-����̐����ꏊ")]
    private Transform _weponInventory;

    // �{�^�����ꂼ��Ɏ��������Ԃ���������
    private List<Button> _weponButton=new((int)ConstancyGanType.Max);

    private void Start()
    {
        GanObject.LoodGameObject();

        for (int i = 0; i < _loadoutIndex; i++)
        {
            _weponImage.Add(null);
            _weponButton.Add(null);
            GameObject gameObject = Instantiate(_loadoutObject, _content.transform);
            _weponButton[i] = gameObject.transform.GetChild(0).GetComponent<Button>();

            // �ʏ핐���objects�ɓ����Ă���̂̏ォ������Ă���
            _weponButton[i].onClick.AddListener(() => WeponIndex(GetGameObject(i)));
            _weponImage[i] = gameObject.transform.GetChild(1).GetComponent<RawImage>();
            _weponImage[i].texture = _weponRawTexture[i];
            if (i == 0) continue;
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x
                            , gameObject.transform.localPosition.y - _set
                            , gameObject.transform.localPosition.z);
            _set += 400.0f;
        }
    }

    private GameObject GetGameObject(int i)
    {
        _player.SetActive(true);
        return Instantiate(constancyGun.objects[0], _weponInventory);
    }

    private void WeponIndex(GameObject gameObject)
    {
        _loadoutCamera.SetActive(false);
        // ���[�h�A�E�g�����
        _loadoutCanvas.SetActive(false);
    }
}
