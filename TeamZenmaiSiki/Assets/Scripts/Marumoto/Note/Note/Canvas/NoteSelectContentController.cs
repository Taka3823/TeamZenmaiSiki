using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class NoteSelectContentController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    AnimationCurve curve;

    [SerializeField, Tooltip("移動にかける時間"), Range(0.5f, 5.0f)]
    float moveTime;

    [SerializeField, Range(0.0f, 10.0f)]
    float enableFlickValue;

    [SerializeField]
    Text totalPages;

    [SerializeField]
    Text notePerCharaPages;

    [SerializeField]
    Text messengerName;

    private Vector3 moveDistance = new Vector3();
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();
    private float startTime;
    private bool curveIsActive = false;
    private int currentTargetTotalIndex = 0;
    private int currentCharaIndex = 0;
    private int currentNotePerCharaIndex = 0;
    private int totalElem = 0;
    private List<int> pagePerCharaIndex; 

    void Start()
    {
        moveDistance = new Vector3(1334.0f, 0.0f, 0.0f);
        startPos = transform.localPosition;
        endPos = transform.localPosition - moveDistance;
        totalElem = CanvasManager.Instance.TotalPageNum;
        pagePerCharaIndex = CanvasManager.Instance.ContentsIndex;
        TextUpdate();
    }

    void Update()
    {
        if (curveIsActive)
        {
            StartEasing(startTime);
        }
    }

    /// <summary>
    /// インターフェイスのため定義のみ。
    /// </summary>
    public void OnDrag(PointerEventData eventData) { }

    /// <summary>
    /// インターフェイスのため定義のみ
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData) { }

    /// <summary>
    /// ドラッグ終了時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        PageFlick(eventData);
    }

    private void PageFlick(PointerEventData _eventData)
    {
        //ページ送り
        if (_eventData.delta.x < -enableFlickValue)
        {
            if (curveIsActive) return;

            currentTargetTotalIndex++;
            if (TotalIndexOverflow()) return;

            currentNotePerCharaIndex++;
            PageIndexClamping();

            TextUpdate();
            EasingSetup();
            endPos = startPos - moveDistance;
        }

        //ページ戻し
        else if (_eventData.delta.x > enableFlickValue)
        {
            if (curveIsActive) return;

            currentTargetTotalIndex--;
            if (TotalIndexOverflow()) return;

            currentNotePerCharaIndex--;
            PageIndexClamping();

            TextUpdate();
            EasingSetup();
            endPos = startPos + moveDistance;
        }
    }

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

    private void EasingSetup()
    {
        curveIsActive = true;
        startTime = Time.timeSinceLevelLoad;
        startPos = transform.localPosition;
    }

    private void TextUpdate()
    {
        messengerName.text = CanvasManager.Instance.NoteDatas[currentCharaIndex].MessengerName[0];
        totalPages.text = (currentTargetTotalIndex + 1).ToString() + "/" + totalElem;
        notePerCharaPages.text = "No." + (currentNotePerCharaIndex + 1).ToString() + "/" + pagePerCharaIndex[currentCharaIndex];
    }

    /// <summary>
    /// currentTargetTotalIndexが限界値を踏み外しているかどうか。
    /// </summary>
    /// <returns></returns>
    private bool TotalIndexOverflow()
    {
        if (currentTargetTotalIndex < 0)
        {
            currentTargetTotalIndex++;
            return true;
        }
        else if (currentTargetTotalIndex >= totalElem)
        {
            currentTargetTotalIndex--;
            return true;
        }
        return false;
    }

    /// <summary>
    /// ページ数表記用のインデックスが限界値を踏み抜かないようにClamp
    /// </summary>
    private void PageIndexClamping()
    {
        if (currentNotePerCharaIndex >= pagePerCharaIndex[currentCharaIndex])
        {
            if (currentCharaIndex >= (pagePerCharaIndex.Count - 1))
            {
                currentNotePerCharaIndex--;
            }
            else
            {
                currentCharaIndex++;
                currentNotePerCharaIndex = 0;
            }
        }
        else if (currentNotePerCharaIndex < 0)
        {
            if (currentCharaIndex <= 0)
            {
                currentNotePerCharaIndex++;
            }
            else
            {
                currentCharaIndex--;
                currentNotePerCharaIndex = pagePerCharaIndex[currentCharaIndex] - 1;
            }
        }
    }
}