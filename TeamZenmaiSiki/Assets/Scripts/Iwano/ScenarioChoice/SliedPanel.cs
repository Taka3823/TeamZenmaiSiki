using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;

public class SliedPanel : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    AnimationCurve curve;

    [SerializeField, Range(0.0f, 10.0f)]
    float enableFlickValue;

    //Easing中かどうか
    bool isDuaringMove = false;
    float startTime;

    //Vector3 moveDistance = new Vector3(Screen.width,0,0);
    Vector3 moveDistance = new Vector3(Screen.width, 0f, 0f);

    [SerializeField, Tooltip("目的地にたどり着くまでの時間"), Range(0.1f, 5.0f)]
    float moveTime;

    //Easingのエンドポジション
    Vector3 endPos = new Vector3();
    //Easingのスタートポジション
    Vector3 startPos = new Vector3();

    int pageCount = 0;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if(isDuaringMove)
        {
            StartEasing(startTime);
        }
    }

    public void OnDrag(PointerEventData eventData) { }
    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData_)
    {
        if(isDuaringMove)
        {
            return;
        }

        PageFlick(eventData_);
    }

    void PageFlick(PointerEventData eventData_)
    {
        if(eventData_.delta.x < -enableFlickValue &&
           pageCount < SaveManager.Instance.GetClearChapterNum())
        {
            pageCount += 1;

            endPos = transform.localPosition - moveDistance;
            EasingInit();
        }
        else if(eventData_.delta.x > enableFlickValue &&
                pageCount >= 1)
        {
            pageCount -= 1;

            endPos = transform.localPosition + moveDistance;
            EasingInit();
        }
    }

    void EasingInit()
    {
        isDuaringMove = true;
        startPos = transform.localPosition;
        startTime = Time.timeSinceLevelLoad;
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            isDuaringMove = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(startPos, endPos, pos);
    }
}
