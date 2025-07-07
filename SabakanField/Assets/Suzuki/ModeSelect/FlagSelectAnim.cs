using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static SelectManager;

public class FlagSelectAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool _isAnimating = false;
    [SerializeField] Transform _camera;
    [SerializeField] Transform _farstPosition;
    [SerializeField] Transform _secondPosition;
    [SerializeField] Transform _thirdPosition;
    float _speed = 2f;
    float time = 0.0f;
    float changeTime = 6.5f;
    Vector3 movePosition = Vector3.zero;

    bool isFarst = false;
    bool isSecond = false;
    bool isThird = false;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 120;
        GetComponent<Button>().onClick.AddListener(OnButton);

        _camera.localPosition = movePosition = _farstPosition.localPosition;
        _camera.localRotation = _farstPosition.localRotation;
    }

    private void Update()
    {
        Animation();
    }

    void OnButton()
    {
        Instance.SelectMode(Mode.Flag);
        GameModes.mode = PublicEnum.GameMode.flag;

    }



    private void Animation()
    {
        if (!_isAnimating) return;
        FarstCameraPosition();
        SecondCameraPosition();
        ThirdCameraPosition();
    }

    private void FarstCameraPosition()
    {
        if (!isFarst) return;
        time += Time.deltaTime;
        if (time > changeTime)
        {
            time = 0;
            isFarst = false;
            isSecond = true;
            movePosition = _secondPosition.localPosition;
            _camera.localRotation=_secondPosition.localRotation;
        }
        movePosition.x += Time.deltaTime * (_speed / 15);
        movePosition.z += Time.deltaTime * (_speed / 30);
        _camera.localPosition = movePosition;

    }

    private void SecondCameraPosition()
    {
        if (!isSecond) return;
        time += Time.deltaTime;
        if (time > changeTime)
        {
            time = 0;
            isSecond = false;
            isThird = true;
            movePosition = _thirdPosition.localPosition;
            _camera.localRotation = _thirdPosition.localRotation;
        }

        movePosition.x += Time.deltaTime * (_speed / 30);
        movePosition.z -= Time.deltaTime * (_speed / 10);
        _camera.localPosition = movePosition;
    }


    private void ThirdCameraPosition()
    {
        if (!isThird) return;
        time += Time.deltaTime;
        if (time > changeTime)
        {
            time = 0;
            isThird = false;
            isFarst = true;
            movePosition = _farstPosition.localPosition;
            _camera.localRotation = _farstPosition.localRotation;
        }

        movePosition.x += Time.deltaTime * (_speed / 30);
        movePosition.z -= Time.deltaTime * (_speed / 10);
        _camera.localPosition = movePosition;

        _camera.rotation *= Quaternion.Euler(0, Time.deltaTime * -_speed, 0);
    }

    private void ResetPosition()
    {
        time = 0;
        _isAnimating = false;
        isFarst = false;
        isSecond = false;
        isThird = false;

        _camera.localPosition = movePosition = _farstPosition.localPosition;
        _camera.localRotation = _farstPosition.localRotation;
    }

    // インターフェースから
    // カーソルが対象に重なった時
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isAnimating = true;
        isFarst = true;
    }

    // カーソルが離れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPosition();
    }
}
