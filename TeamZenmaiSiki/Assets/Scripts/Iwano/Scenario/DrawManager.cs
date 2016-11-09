using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

public class DrawManager : MonoBehaviour
{
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

}
