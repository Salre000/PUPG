using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using static UnityEngine.InputSystem.DefaultInputActions;

public class Dominator : MonoBehaviour
{
    private readonly float size = 30;
    Canvas _canvas;
    [SerializeField] List<Image> test=new List<Image>();
    [SerializeField]Image testImage;
    private readonly Vector2 main = new Vector2(Screen.width/2,Screen.height/2);
    [SerializeField] Camera _camera;
    [SerializeField] List<GameObject> targetList = new List<GameObject>();
    public List<GameObject> GetTarget() { return targetList; }
    public void Awake()
    {
    }

    public void OnEnable()
    {
        _canvas=GameObject.Find("UICanvas")?.GetComponent<Canvas>();
        if(_canvas==null)
        _canvas=GameObject.Find("NormalWeponLoadoutCanvas").GetComponent<Canvas>();
        _camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    float time = 0;

    public void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time < 0.3f) return;
        time = 0;
        Test();
        targetList.Clear();

        List<AI> Characters = AIUtility.GetEnemyAI(0);

        if (GameModes.mode != PublicEnum.GameMode.flag) 
        {
            List<AI> enem = AIUtility.GetEnemyAI(1);
            for (int i = 0; i < enem.Count; i++) 
            {
                Characters.Add(enem[i]);
            }



        }
        RaycastHit hit;


        for (int i = 0; i < Characters.Count; i++)
        {

            Vector3 dir= (Characters[i].transform.position+new Vector3(0,1.25f,0))-transform.position;

            if (Physics.Raycast(transform.position, dir, out hit))
            {

                //当たった対象にrayが当ったときの関数を内包したインターフェースクラスが付いている場合取得
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //当たった対象に無敵の関数を内包したインターフェースクラスが付いている場合取得
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();

                //先の二つのインターフェースクラスが両方取得出来たかを判定
                if (hitObject == null || invincible == null) continue;

                if (Vector3.Distance(Characters[i].transform.position,transform.position) > 100) continue;

                Vector3 point = _camera.WorldToScreenPoint(Characters[i].transform.position + new Vector3(0, 1.25f, 0));

                if (Vector2.Distance(main, point) > 500) continue;

                if (point.z < 0) continue;

                if (!hit.transform.gameObject.GetComponent<AI>().GetISLife()) continue;

                Debug.Log("何か" + point.z);

                Debug.DrawLine(transform.position, Characters[i].transform.position, UnityEngine.Color.magenta, 3);

                targetList.Add(Characters[i].gameObject);
            }
        }
    }

    private void Test() 
    {
        for(int i=0;i< test.Count; i++)
        {
            Destroy(test[i].gameObject);

        }
        test.Clear();
        for (int i = 0;i < targetList.Count; i++) 
        {
            Image image = GameObject.Instantiate(testImage, _canvas.transform);

            image.transform.position = _camera.WorldToScreenPoint(targetList[i].transform.position+ new Vector3(0, 1.25f, 0));
            Debug.Log(_camera.WorldToScreenPoint(targetList[i].transform.position) + "座標");

            float Size = size - Vector3.Distance(transform.position, targetList[i].transform.position)/5.0f;
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,Size);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,Size);

            image.transform.position = new Vector3(image.rectTransform.position.x, image.rectTransform.position.y, 0);

            test.Add(image);

        }
    }
}
