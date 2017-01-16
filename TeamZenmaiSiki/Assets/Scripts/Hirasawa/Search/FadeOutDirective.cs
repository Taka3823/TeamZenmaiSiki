using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FadeOutDirective : MonoBehaviour
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

    //Easingを始めていいかどうか
    private bool canEasing = false;
    private bool isstart;
    //Easingをするために必要な起動時間
    private float startTime;
    private bool firsttouch;
    void Start()
    {
        //Easingを始めるポジションの初期化
        startPosition = transform.localPosition;

        //エンドポジションの決定
        endPosition = transform.localPosition + endPositionDistance;
        if (DataManager.Instance.KillNames.Count >= 1)
        {
            firsttouch = false;
            transform.localPosition = endPosition;
            ChangeText();
        }
        else
        {
            firsttouch = true;
        }
    }

    void Update()
    {
        if(canEasing)
        {
            StartEasing(startTime);
        }
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = endPosition;
            canEasing = false;
            if (firsttouch)
            {
                firsttouch = false;
                ChangeText();
            }
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);
        if (isstart)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, pos);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(endPosition, startPosition, pos);
        }
        

        //Easing終了時の処理を記述する
        //if (rate >= 1){}
    }

    public void OnClick()
    {
        if (!canEasing)
        {
            canEasing = true;
            startTime = Time.timeSinceLevelLoad;
            isstart = true;
        }
    }

    public void OnClickReturn()
    {
        if (!canEasing)
        {
            canEasing = true;
            startTime = Time.timeSinceLevelLoad;
            isstart = false;
        }
    }
    private void ChangeText()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Button")
            {
                Sprite sprite = new Sprite();
                sprite = Resources.Load<Sprite>("Sprits/Search/back_to_HQ_button");
                child.gameObject.GetComponent<Image>().sprite = sprite;
                //foreach (Transform gchild in child.transform)
                //{
                //    if (gchild.name == "Text")
                //    {
                //        gchild.GetComponent<Text>().text = "戻る";
                //    }
                //}
            }
        }
    }
}
