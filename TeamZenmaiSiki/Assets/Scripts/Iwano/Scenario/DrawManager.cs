using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    //1が左、2が真ん中、3が右
    public struct CharaDrawPosition
    {
        public Vector2 left;
        public Vector2 center;
        public Vector2 right;
    }

    CharaDrawPosition charaDrawPosition;

    private static DrawManager instance;

    public static DrawManager Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(DrawManager);
                instance = (DrawManager)FindObjectOfType(t);

                if (instance == null)
                {
                    Debug.LogError("DrawManagerのインスタンスがnullです");
                }
            }
            return instance;
        }
    }

    //全キャラクターの画像一覧
    Dictionary<string, Sprite> charaImageDictionary;
    //全背景の画像一覧
    Dictionary<string, Sprite> backGroundImageDictionary;
    //全吹き出し画像一覧
    Dictionary<string, Sprite> baloonImageDictionary;

    [SerializeField]
    GameObject leftImageGameObject;
    [SerializeField]
    GameObject centerImageGameObject;
    [SerializeField]
    GameObject rightImageGameObject;

    Image leftImage;
    Image centerImage;
    Image rightImage;

    Image backGroundImage;

    [SerializeField]
    GameObject baloonGameObject;

    Image baloonImage;

    bool isFirst = false;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);

            return;
        }

        DontDestroyOnLoad(this);

        charaDrawPosition.left = new Vector2(-430f, 100f);
        charaDrawPosition.center = new Vector2(0f, 100f);
        charaDrawPosition.right = new Vector2(430f, 100f);

        backGroundImage = GetComponent<Image>();

        rightImage = rightImageGameObject.GetComponent<Image>();
        leftImage = leftImageGameObject.GetComponent<Image>();
        centerImage = centerImageGameObject.GetComponent<Image>();

        leftImageGameObject.SetActive(false);
        rightImageGameObject.SetActive(false);
        centerImageGameObject.SetActive(false);

        baloonImage = baloonGameObject.GetComponent<Image>();

        charaImageDictionary = new Dictionary<string, Sprite>();
        backGroundImageDictionary = new Dictionary<string, Sprite>();
        baloonImageDictionary = new Dictionary<string, Sprite>();

        var charaList = Resources.LoadAll<Sprite>("Sprits/Scenario/Character");
        var backGroundList = Resources.LoadAll<Sprite>("Sprits/Scenario/BackGround");
        var baloonList = Resources.LoadAll<Sprite>("Sprits/Scenario/UI");

        foreach (Sprite chara in charaList)
        {
            charaImageDictionary.Add(chara.name + ".png", chara);
            //charaImageDictionary[chara.name] = chara;
        }

        foreach (Sprite backGround in backGroundList)
        {
            backGroundImageDictionary.Add(backGround.name + ".png", backGround);
        }

        foreach (Sprite baloon in baloonList)
        {
            baloonImageDictionary.Add(baloon.name + ".png", baloon);
        }
    }

    //キャラクターを変更するときに使用
    public void DrawCharacter(string posName_, string imagePath_)
    {
        if (posName_ == "右")
        {
            rightImage.sprite = charaImageDictionary[imagePath_];
            rightImageGameObject.SetActive(true);
        }
        else if (posName_ == "中央")
        {
            centerImage.sprite = charaImageDictionary[imagePath_];
            centerImageGameObject.SetActive(true);
        }
        else if (posName_ == "左")
        {
            leftImage.sprite = charaImageDictionary[imagePath_];
            leftImageGameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("表示するための位置が指定されていません");
        }
    }

    //指定位置のキャラクターを消すときに使用
    public void EraseTheCharacter(string posName_)
    {
        if (posName_ == "右")
        {
            rightImageGameObject.SetActive(false);
        }
        else if (posName_ == "中央")
        {
            centerImageGameObject.SetActive(false);
        }
        else if (posName_ == "左")
        {
            leftImageGameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("非表示にする位置が指定されていません");
        }
    }

    //吹き出しの表示を変える際に使用
    public void DrawBalloon(string posName_)
    {
        if (posName_ == "右")
        {
            baloonImage.sprite = baloonImageDictionary["comment_right.png"];
        }
        else if (posName_ == "中央")
        {
            baloonImage.sprite = baloonImageDictionary["comment_center.png"];
        }
        else if (posName_ == "左")
        {
            baloonImage.sprite = baloonImageDictionary["comment_left.png"];
        }
        else if (posName_ == "")
        {
            baloonImage.sprite = baloonImageDictionary["comment.png"];
        }
        else if (posName_ == null)
        {
            baloonImage.sprite = baloonImageDictionary["comment.png"];
        }
        else
        {
            Debug.LogError("表示するための位置が指定されていません");
        }
    }

    //背景の画像を変えるときに使用
    public void DrawBackGround(string pathName_,IwanoFade fade)
    {
        /**
        if (isFirst)
        {
            // FadeManager.Instance.FadeInOut(1.5f, 0.5f);
            //FadeManager.Instance.FadeInActionFadeout(1.5f, 0.5f, () => { BackChange(pathName_); });

            if (fade != null)
            {
                fade.FadeInOutInit();
            }

            backGroundImage.sprite = backGroundImageDictionary[pathName_];

            //StartCoroutine(BackChange(1.0f,pathName_));

            return;
        }

        if (!isFirst)
        {
            isFirst = true;
        }
    /**/

        backGroundImage.sprite = backGroundImageDictionary[pathName_];
    }

    //IEnumerator BackChange(float time_,string pathName_)
    //{
    //    Debug.Log(0);

    //    yield return new WaitForSeconds(time_);

    //    Debug.Log(1);
    //    backGroundImage.sprite = backGroundImageDictionary[pathName_];
    //}

    //void BackChange(string pathName_)
    //{
    //    backGroundImage.sprite = backGroundImageDictionary[pathName_];
    //}
}
