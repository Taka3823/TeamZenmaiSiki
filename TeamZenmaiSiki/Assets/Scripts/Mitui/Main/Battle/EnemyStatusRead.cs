using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStatusRead : MonoBehaviour
{
    private List<EnemyData.EnemyInternalDatas> list;
    void EnemyRead()
    {
        // エネミーのサーチテンプレ(この場合敵１体目のコアの名前)
        //string str = SearchManager.Instance.getBattleEnemyDatas()[0].coreName[0];
        //Debug.Log(str);
        //DataManager.Instance.EnemyInternalDatas

        // サーチから敵データを読み込む
        for (int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; i++)
        {
            Debug.Log(DataManager.Instance.EnemyInternalDatas[i].name);
        }

    }


    // Use this for initialization
    void Start()
    {
        // 仮置き
        //list = new List<EnemyData.EnemyInternalDatas>();
        //for(int i = 0; i < 3; i++)
        //{
        //    EnemyData.EnemyInternalDatas buf;
        //    buf = new EnemyData.EnemyInternalDatas();
        //    buf.name = "mitsui" + i.ToString();
        //    buf.mainHp = 10 + i;
        //    buf.mainPower = 5 + i;
        //    list.Add(buf);
        //}
        EnemyRead();
    }

    // Update is called once per frame
    void Update()
    {
        //list[0]
    }

}
