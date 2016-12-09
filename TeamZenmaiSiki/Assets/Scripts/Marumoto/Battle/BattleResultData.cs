using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleResultData : MonoBehaviour{
    private static BattleResultData instance;
    public static BattleResultData Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    //***********************************************
    //戦闘結果データ。
    //***********************************************
    private int deadEnemyNum = 0;
    private List<string> deadEnemyName;
}
