using UnityEngine;
using System.Collections;

public class IwanoFade : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.RawImage fadePlate;

    private bool isFadeIn = false;
    public bool IsFadeIn
    {
        get { return isFadeIn; }
        set { isFadeIn = value; }
    }

    private bool isFadeOut = false;

    public bool IsFadeOut
    {
        get { return isFadeOut; }
        set { isFadeOut = value; }
    }

    //FadeIn -> FadeOut用の変数
    private bool canFadeInOut;
    public bool CanFadeInOut
    {
        get { return canFadeInOut; }
        set { canFadeInOut = value; }
    }

    //FadeOut -> FadeIn用の変数
    private bool canFadeOutIn;
    public bool CanFadeOutIn
    {
        get { return canFadeOutIn; }
        set { canFadeOutIn = value; }
    }


    readonly Vector4 fillBlack = new Vector4(0f, 0f, 0f, 1f);
    readonly Vector4 fillClear = new Vector4(0f, 0f, 0f, 0f);

    const float defaultSpeed = 0.01f;

    private bool duringFade = false;
    public bool DuringFade
    {
        set { duringFade = value; }
        get { return duringFade;  }
    }

    void Start()
    {
        //fadePlate.color = fillBlack;
    }

    public void FadeOut(float fadeSpeed_ = defaultSpeed)
    {
        fadePlate.color = new Vector4(0f, 0f, 0f, fadePlate.color.a - fadeSpeed_);
        Time.timeScale = 0;

        if (fadePlate.color.a <= 0f)
        {
            duringFade = false;
            Time.timeScale = 1;

            fadePlate.color = fillClear;
            isFadeOut = false;
            canFadeInOut = false;
            this.GetComponent<Canvas>().sortingOrder = -1;
        }
    }

    public void FadeIn(float fadeSpeed_ = defaultSpeed)
    {
        fadePlate.color = new Vector4(0f, 0f, 0f, fadePlate.color.a + fadeSpeed_);
        Time.timeScale = 0;

        if(fadePlate.color.a >= 1f)
        {
            duringFade = false;
            Time.timeScale = 1;

            fadePlate.color = fillBlack;
            isFadeIn = false;
            CanFadeOutIn = false;
            this.GetComponent<Canvas>().sortingOrder = -1;
        }
    }

    public void FadeInOutInit()
    {
        IsFadeIn = true;
        DuringFade = true;
        CanFadeInOut = true;
        GetComponent<Canvas>().sortingOrder = 999;
    }

    public void FadeOutInInit()
    {
        IsFadeOut = true;
        DuringFade = true;
        CanFadeOutIn = true;
        GetComponent<Canvas>().sortingOrder = 999;
    }

    void Update()
    { 
        if(IsFadeIn)
        {
            FadeIn();
        }

        if(IsFadeOut)
        {
            FadeOut();
        }
    }
}
