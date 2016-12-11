using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

    void Start()
    {
        //Easingを始めるポジションの初期化
        startPosition = transform.localPosition;

        //エンドポジションの決定
        endPosition = transform.localPosition + endPositionDistance;
    }

    void Update()
    {
        if (canEasing[GOING_NUM] && !isDisplay)
        {
            StartEasing(startTime);
        }

        if (canEasing[RETURN_NUM] && isDisplay)
        {
            ReverceEasing(startTime);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            OnClickLightIsOut();
        }
    }

    //Tabが出ているときに、Tab以外の場所をクリックしたら
    //元の位置に戻す処理
    public void OnClickLightIsOut()
    {
        if (isDisplay && !canEasing[RETURN_NUM] && !RayCast("InfoTab"))
        {
            canEasing[RETURN_NUM] = true;
            startTime = Time.timeSinceLevelLoad;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDisplay) return;
        if (!RayCast("InfoTab")) return;

        startPos = eventData.position.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPos = eventData.position.x;

        if ((startPos - endPos) < -10)
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
}


//using UnityEngine;
//using System.Collections;

//public class TabControl : MonoBehaviour
//{

//    private Vector3 touchStartPos;
//    private Vector3 touchEndPos;
//    public GameObject target = null;

//    Direction direction = new Direction();

//    public enum Direction
//    {
//        right,
//        left,
//        touch
//    }

//    // ターゲットの情報取得
//    public void setTarget()
//    {
//        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Collider2D collider = Physics2D.OverlapPoint(tapPoint);

//        // 何かのColliderに当たってる
//        if (collider != null)
//        {
//            Debug.Log("hit");

//            // そのColliderのタグがInfoTabであるなら処理
//            if (collider.gameObject.name == "InfoTab")
//            {
//                //GameObject obj = collider.transform.gameObject;
//                //Debug.Log("Info");
//                //obj.transform.position= new Vector3(transform.localPosition.x-2.0f, transform.localPosition.y, 0);
//            }
//        }
//    }


//    // フリック操作
//    void Flick()
//    {
//        // タッチ開始位置
//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
//            setTarget();
//        }
//        // 指を放した位置
//        if (Input.GetKeyUp(KeyCode.Mouse0))
//        {
//            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
//            GetDirection();
//        }
//    }

//    // フリックの方向
//    void GetDirection()
//    {
//        float directionX = touchEndPos.x - touchStartPos.x;
//        float directionY = touchEndPos.y - touchStartPos.y;

//        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
//        {
//            // 右フリック
//            if (30 < directionX)
//            {
//                direction = Direction.right;
//                Debug.Log("right");

//            }
//            // 左フリック
//            else if (-30 > directionX)
//            {
//                direction = Direction.left;
//                Debug.Log("left");

//            }
//        }
//        else //タッチ
//        {
//            direction = Direction.touch;
//            Debug.Log("touch");
//        }
//    }

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Flick();

//        if (Input.GetKeyDown(KeyCode.Mouse0))
//        {
//        }
//        switch (direction)
//        {
//            case Direction.right:
//                break;

//            case Direction.left:

//                break;

//            case Direction.touch:
//                break;
//        }

//    }
//}
