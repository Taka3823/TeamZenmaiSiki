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
    Dictionary<string, Image> charaImageDictionary;

    //全背景の画像一覧
    Dictionary<string, Image> backGroundImageDictionary;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);

            return;
        }

        DontDestroyOnLoad(this.gameObject);

        charaDrawPosition.left = new Vector2(-430f, 100f);
        charaDrawPosition.center = new Vector2(0f, 100f);
        charaDrawPosition.right = new Vector2(430f, 100f);

        charaImageDictionary = new Dictionary<string, Image>();
        backGroundImageDictionary = new Dictionary<string, Image>();

        //var charaList = Resources.LoadAll<"Sprits/Scenario">


    }

    //キャラクターを描画するときに使用
    void DrawCharacter(string posName_, string imagePath_)
    {
        if (posName_ == "右")
        {

        }
        else if (posName_ == "中央")
        {

        }
        else if (posName_ == "左")
        {

        }
        else
        {
            Debug.LogError("表示するための位置が指定されていません");
        }
    }

    //指定位置のキャラクターを消すときに使用
    void EraseTheCharacter(string posName_, string imagePath_)
    {

    }
}
