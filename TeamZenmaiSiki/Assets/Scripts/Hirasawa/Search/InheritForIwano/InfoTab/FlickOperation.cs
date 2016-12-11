using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlickOperation : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //Easingに使う
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField, Tooltip("目的地までの距離")]
    private Vector3 endPositionDistance = new Vector3();

    [SerializeField, Tooltip("目的地にたどり着くまでの時間"), Range(0.1f, 5.0f)]
    private float moveTime;

    //Easingのエンドポジション
    private Vector3 endPosition = new Vector3();

    //Easingのスタートポジション。0が右目、1が左目用
    private Vector3 startPosition = new Vector3();

    //二種類のEasing関数を使い分けるための要素番号
    const int GOING_NUM = 0;
    const int RETURN_NUM = 1;

    //Easingを始めていいかどうか
    private bool[] canEasing = new bool[2];

    //Easingをするために必要な起動時間
    private float startTime;

    //フリックのスタートポジションとエンドポジション
    float startPos = 0;
    float endPos = 0;
    //ドラッグがどれほどされたか
    int dragCount = 0;
    bool isDisplay = false;
    bool GetIsDisplay()
    {
        return isDisplay;
    }
    void Start()
    {
        //Easingを始めるポジションの初期化
        startPosition = transform.localPosition;

        //エンドポジションの決定
        endPosition = transform.localPosition + endPositionDistance;
    }

    void Update()
    {
        if (TabManager.Instance.Getisblood()) return;
        StartEasingFunc();
        ReverseEasingFunc();
        OnClickLightIsOutFunc();
        TabManager.Instance.SetIsDisplay(isDisplay);
    }

    //Tabが出ているときに、Tab以外の場所をクリックしたら
    //元の位置に戻す処理
    public void OnClickLightIsOut()
    {
        if (isDisplay && !canEasing[RETURN_NUM] && !RayCast("InfoTab")&&!RayCast2D("Unit"))
        {
            canEasing[RETURN_NUM] = true;
            startTime = Time.timeSinceLevelLoad;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!RayCast("InfoTab")) return;
        if (!RayCast2D("Unit")) return;
        if (isDisplay) return;

        startPos = eventData.position.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPos = eventData.position.x;

        if ((startPos - endPos) < -100)
        {
            if (!canEasing[GOING_NUM])
            {
                canEasing[GOING_NUM] = true;
                startTime = Time.timeSinceLevelLoad;
            }
        }
    }

    //エラーにならないようにオーバーライドしている
    public void OnDrag(PointerEventData eventData) { }

    bool RayCast(string tagName_)
    {
        //カメラの場所からポインタの場所に向かってレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        //レイが何か当たっているかを調べる
        if (Physics.Raycast(ray, out hit))
        {
            //当たったオブジェクトを格納
            GameObject obj = hit.collider.gameObject;

            if (obj.CompareTag(tagName_))
            {
                return true;
            }
        }
        return false;
    }
    bool RayCast2D(string tagName)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit)
        {
            GameObject obj = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Unit")
            {
                return true;
            }
        }
        return false;
    }
    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = endPosition;
            canEasing[GOING_NUM] = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, pos);

        //Easing終了時の処理を記述する
        if (rate >= 1)
        {
            isDisplay = true;
        }
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    //TIPS:元のポジションに戻すために行っているので、StartとEndが逆になっている
    void ReverceEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = startPosition;
            canEasing[RETURN_NUM] = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(endPosition, startPosition, pos);
        //Easing終了時の処理を記述する
        if (rate >= 1)
        {
            isDisplay = false;
        }
    }
    void StartEasingFunc()
    {
        if (canEasing[GOING_NUM] && !isDisplay)
        {
            StartEasing(startTime);
        }
    }
    void ReverseEasingFunc()
    {

        if (canEasing[RETURN_NUM] && isDisplay)
        {
            ReverceEasing(startTime);
        }
    }
    void OnClickLightIsOutFunc()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            OnClickLightIsOut();
        }
    }
}
