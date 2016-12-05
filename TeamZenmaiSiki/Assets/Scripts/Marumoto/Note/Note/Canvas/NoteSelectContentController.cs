using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class NoteSelectContentController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    AnimationCurve curve;

    [SerializeField, Tooltip("移動にかける時間"), Range(0.5f, 5.0f)]
    float moveTime;

    [SerializeField, Tooltip("フリックを有効と判断する指の移動距離"), Range(0.0f, 10.0f)]
    float enableFlickValue;

    private Vector3 moveDistance = new Vector3();
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();
    private float startTime;
    private bool curveIsActive = false;
    private int currentTargetIndex = 0;
    private int noteSelectContentsElem = 0;

    void Start()
    {
        moveDistance = new Vector3(1334.0f, 0.0f, 0.0f);
        startPos = transform.localPosition;
        endPos = transform.localPosition - moveDistance;
        noteSelectContentsElem = GameObject.FindGameObjectsWithTag("NoteSelect").Length;
    }

    void Update()
    {
        if (curveIsActive)
        {
            StartEasing(startTime);
        }
    }

    /// <summary>
    /// エラー回避のためにオーバーライド。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData) { }

    /// <summary>
    /// エラー回避のためにオーバーライド
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData) { }

    /// <summary>
    /// ドラッグ判定
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        PageFlick(eventData);
    }

    /// <summary>
    /// フリックでページをめくる処理の詳細部分。
    /// </summary>
    /// <param name="_eventData"></param>
    private void PageFlick(PointerEventData _eventData)
    {
        //ページ送り
        if (_eventData.delta.x < -enableFlickValue)
        {
            if (curveIsActive) return;
            currentTargetIndex++;
            if (currentTargetIndex >= noteSelectContentsElem)
            {
                currentTargetIndex--;
                return;
            }
            EasingSetup();
            endPos = startPos - moveDistance;
        }

        //ページ戻し
        else if (_eventData.delta.x > enableFlickValue)
        {
            if (curveIsActive) return;
            currentTargetIndex--;
            if (currentTargetIndex < 0)
            {
                currentTargetIndex++;
                return;
            }
            EasingSetup();
            endPos = startPos + moveDistance;
        }
    }

    /// <summary>
    /// ページめくりのイージング処理
    /// </summary>
    /// <param name="_startTime"></param>
    private void StartEasing(float _startTime)
    {
        float diffTime = Time.timeSinceLevelLoad - _startTime;
        if (diffTime > moveTime)
        {
            curveIsActive = false;
        }
        float rate = diffTime / moveTime;
        float pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(startPos, endPos, pos);
    }

    /// <summary>
    /// イージング開始前に有効化、および開始点を設定。
    /// </summary>
    private void EasingSetup()
    {
        curveIsActive = true;
        startTime = Time.timeSinceLevelLoad;
        startPos = transform.localPosition;
    }
}
