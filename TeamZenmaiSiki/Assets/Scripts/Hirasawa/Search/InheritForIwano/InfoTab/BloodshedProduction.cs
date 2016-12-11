using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class BloodshedProduction : MonoBehaviour
{
    //Easingに使う
    /**
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField, Tooltip("目的地までの距離")]
    private Vector3 endPositionDistance = new Vector3();

    [SerializeField, Tooltip("目的地にたどり着くまでの時間"), Range(0.1f, 5.0f)]
    private float moveTime;

    //Easingのエンドポジション
    private Vector3 endPositon = new Vector3();

    //Easingのスタートポジション。0が右目、1が左目用
    private Vector3 startPosition = new Vector3();

    //Easingを始めていいかどうか
    private bool canEasing = false;

    //Easingをするために必要な起動時間
    private float startTime;
    /**/

    [SerializeField]
    SerchSceneChanger sceneChanger;

    Slider slider;

    int productTime = 0;

    bool isProductIn = false;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;

        //Easingを始めるポジションの初期化
        //startPosition = transform.localPosition;
        //エンドポジションの決定
        //endPositon = transform.localPosition + endPositionDistance;
    }

    void Update()
    {
        //Easingに使う
        /**
        if (canEasing)
        {
            StartEasing(startTime);
        }
        /**/

        if (isProductIn)
        {
            slider.value += 1;
        }

        if (slider.value >= slider.maxValue)
        {
            Debug.Log("SE鳴ったよ！");
            //AudioManager.Instance.PlaySe("SE_Name.wav");
            isProductIn = false;

            sceneChanger.SceneChange("Battle");
        }
    }

    public void OnClick()
    {
        isProductIn = true;
        TabManager.Instance.Setisblood(true);
        //Easingに使う
        /**
        if (!canEasing)
        {
            canEasing = true;
            startTime = Time.timeSinceLevelLoad;
        }
        /**/
    }
    
    //Easingに使う
    /**
    //第一引数……動かし始める時間
    //第二引数……シーンを遷移させるまでの時間。兼、Easingで目的地に到着させる時間
    void StartEasing(float startTime_)
    {
        var diff = Time.timeSinceLevelLoad - startTime_;

        if (diff > moveTime)
        {
            transform.position = endPositon;
            canEasing = false;
        }

        var rate = diff / moveTime;
        var pos = curve.Evaluate(rate);

        transform.localPosition = Vector3.Lerp(startPosition, endPositon, pos);

        //Easing終了時の処理を記述する
        if (rate >= 1)
        {
            //AudioManager.Instance.PlaySe("SE_Name.wav");
        }
    }
    /**/
}
