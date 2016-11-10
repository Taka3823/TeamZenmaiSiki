using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStatusRead : MonoBehaviour
{
    public GameObject EnemyModel1;
    public GameObject EnemyModel2;
    public GameObject EnemyModel3;

    void EnemySpawn()
    {
        // エネミーの数に応じて敵表示
        if (DataManager.Instance.EnemyInternalDatas.Count == 1)
        {
            Instantiate(EnemyModel1, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (DataManager.Instance.EnemyInternalDatas.Count == 2)
        {
            Instantiate(EnemyModel1, new Vector3(-2, 0, 0), Quaternion.identity);
            Instantiate(EnemyModel2, new Vector3(2, 0, 0), Quaternion.identity);
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 3)
        {
            Instantiate(EnemyModel1, new Vector3(-3, 0, 0), Quaternion.identity);
            Instantiate(EnemyModel2, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(EnemyModel3, new Vector3(3, 0, 0), Quaternion.identity);
        }
    }

    void EnemyRead()
    {
        // エネミーのサーチテンプレ(この場合敵１体目のコアの名前)
        //string str = SearchManager.Instance.getBattleEnemyDatas()[0].coreName[0];
        //Debug.Log(str);
        //DataManager.Instance.EnemyInternalDatas


        // サーチから敵データを読み込む
        for (int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; i++)
        {
            int a = DataManager.Instance.EnemyInternalDatas[0].age;
            Debug.Log(a);
            Debug.Log(DataManager.Instance.EnemyInternalDatas[i].age);
        }

    }


    // Use this for initialization
    void Start()
    {
        EnemySpawn();
        EnemyRead();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
