using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    //Easingに使う
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField, Tooltip("目的地までの距離。０が右目で、1が左目")]
    private Vector3[] endPositionDistance = new Vector3[2];

    [SerializeField, Tooltip("目的地にたどり着くまでの時間"), Range(0.1f, 5.0f)]
    private float moveTime;

    [SerializeField, Tooltip("右目のイラスト")]
    private GameObject rightEye;
    [SerializeField, Tooltip("左目のイラスト")]
    private GameObject leftEye;

    //Easingのエンドポジション
    private Vector3[] endPositon = new Vector3[2];

    //Easingのスタートポジション。0が右目、1が左目用
    private Vector3[] startPosition = new Vector3[2];

    //Easingを始めていいかどうか
    private bool canEasing = false;

    //Easingをするために必要な起動時間
    private float startTime;

    [SerializeField]
    GameObject startButton;

    void Start()
    {
        //Easingを始めるポジションの初期化
        startPosition[0] = rightEye.transform.localPosition;
        startPosition[1] = leftEye.transform.localPosition;

        //エンドポジションの決定
        endPositon[0] = rightEye.transform.localPosition + endPositionDistance[0];
        endPositon[1] = leftEye.transform.localPosition + endPositionDistance[1];
    }

    void Update()
    {
        if (canEasing)
        {
            StartEasing(startTime);
        }
        if (DataManager.Instance.IsAppAwake)
        {
            Destroy(rightEye);
            Destroy(leftEye);
        }
    }

    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            rightEye.transform.position = endPositon[0];
            leftEye.transform.position = endPositon[1];
            canEasing = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        rightEye.transform.localPosition = Vector3.Lerp(startPosition[0], endPositon[0], pos);
        leftEye.transform.localPosition = Vector3.Lerp(startPosition[1], endPositon[1], pos);

        if (rate >= 1)
        {
            DataManager.Instance.IsAppAwake = true;
        }
    }

    public void OnClick()
    {
        if (!canEasing)
        {
            canEasing = true;
            startTime = Time.timeSinceLevelLoad;

            Destroy(startButton);
        }
    }
}