using UnityEngine;
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

    private Vector3 moveDistance = new Vector3();
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();
    private float startTime;
    private bool curveIsActive = false;
    private int currentTargetTotalIndex = 0;
    private int currentCharaIndex = 0;
    private int currentNotePerCharaIndex = 0;
    private int totalElem = 0;
    private List<int> notePerCharaIndex; 

    void Start()
    {
        moveDistance = new Vector3(1334.0f, 0.0f, 0.0f);
        startPos = transform.localPosition;
        endPos = transform.localPosition - moveDistance;
        totalElem = CanvasManager.Instance.TotalPageNum;
        notePerCharaIndex = CanvasManager.Instance.ContentsIndex;
    }

    void Update()
    {
        if (curveIsActive)
        {
            StartEasing(startTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

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
            
            if (currentTargetTotalIndex >= totalElem)
            {
                currentTargetTotalIndex--;
                return;
            }

            currentNotePerCharaIndex++;
            if(currentNotePerCharaIndex >= notePerCharaIndex[currentCharaIndex])
            {
                if(currentCharaIndex>= (notePerCharaIndex.Count - 1))
                {
                    currentNotePerCharaIndex--;
                    return;
                }
                else
                {
                    currentCharaIndex++;
                    currentNotePerCharaIndex = 0;
                }
            }


            EasingSetup();
            endPos = startPos - moveDistance;
        }

        //ページ戻し
        else if (_eventData.delta.x > enableFlickValue)
        {
            if (curveIsActive) return;
            currentTargetTotalIndex--;
            if (currentTargetTotalIndex < 0)
            {
                currentTargetTotalIndex++;
                return;
            }
            currentNotePerCharaIndex--;
            if (currentNotePerCharaIndex < notePerCharaIndex[currentCharaIndex])
            {
                if (currentCharaIndex < (notePerCharaIndex.Count - 1))
                {
                    currentNotePerCharaIndex--;
                    return;
                }
                else
                {
                    currentCharaIndex++;
                    currentNotePerCharaIndex = 0;
                }
            }

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

    private void IncreaseIndex()
    {
        
        

        if ()
        {
            if (currentCharaIndex >= (notePerCharaIndex.Count - 1))
            {

            }
        }
    }
}
