﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour {

    [SerializeField]
    Fade fade = null;
    private static FadeManager instance;

    public static FadeManager Instance
    {
        get { return instance; }
    }
    private bool isFadeEffect;
    public bool IsFadeEffect()
    {
        return isFadeEffect;
    }
    private bool isFadeIn;
    public bool IsFadeIn()
    {
        return isFadeIn;
    }
    private bool isFadeOut;
    public bool IsFadeOut()
    {
        return isFadeOut;
    }
    private bool isEffectEnd;
    public bool IsEffectEnd()
    {
        return isEffectEnd;
    }
    public void SetisEffectEnd(bool isBool)
    {
        isEffectEnd = isBool;
    }
    void Start()
    {
        instance = this;
        isFadeEffect = false;
        isEffectEnd = false;
        isFadeIn = false;
        isFadeOut = false;
    }
    public void FadeInOut(float fadetime,float waittime)
    {
        isFadeEffect = true;
        isFadeIn = true;
        fade.FadeIn(fadetime, () =>
        {
            fade.Wait(waittime, () =>
            {
                isFadeOut = true;
                fade.FadeOut(fadetime, () =>
                {
                    isFadeEffect = false;
                    isFadeOut = false;
                    isFadeIn = false;
                    isEffectEnd = true;
                });
            });
     
        });
    }
    public void FadeOut(float time)
    {
        isFadeEffect = true;
        isFadeOut = true;
        fade.FadeOut(time, () =>
        {
            isFadeEffect = false;
            isFadeOut = false;
            isEffectEnd = true;
        });

    }
    public void FadeIn(float time)
    {
        isFadeEffect = true;
        isFadeIn = true;
        fade.FadeIn(time, () =>
        {
            isFadeEffect = false;
            isFadeIn = false;
            isEffectEnd = true;
        });

    }
    public void FadeInSceneChange(float fadetime,float wait,string nextScene)
    {
        isFadeEffect = true;
        isFadeIn = true;
        fade.FadeIn(fadetime, () =>
        {
            fade.Wait(wait, () =>
            {
                isFadeIn = false;
                isFadeEffect = false;
                isEffectEnd = true;
                SceneChange(nextScene);
            });
        });

    }
    public void TestFadeInSceneChange(float time, string nextScene)
    {
        fade.FadeIn(time, () =>
        {
            SceneChange(nextScene);
        });

    }
    public void SceneChange(string nextSceneName_)
    {
        SceneManager.LoadScene(nextSceneName_);
    }
    private void setEffectBool()
    {
        isFadeEffect = true;
        isFadeIn = true;
    }
}