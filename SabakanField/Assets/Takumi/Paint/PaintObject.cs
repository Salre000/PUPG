using UnityEngine;

//���Ԍo�߂Ō����Ȃ��Ȃ�I�u�W�F�N�g�̃N���X
public class PaintObject : MonoBehaviour
{
    private�@float _timeCount =0;
    private readonly float _LIMIT_TIME = 4.0f;
    public void FixedUpdate()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount < _LIMIT_TIME) return;

        _timeCount = 0;

        this.gameObject.SetActive(false);
        
    }
}
