using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStatusRead : MonoBehaviour
{
    [SerializeField]
    Text[] enemyName;
    [SerializeField]
    Text[] age;
    [SerializeField]
    Text[] bloodType;
    [SerializeField]
    Text[] mainHp;
    [SerializeField]
    Text[] mainPower;
    [SerializeField]
    Text[] defence;

    void EnemyRead()
    {
        List<EnemyData.EnemyInternalDatas> enemyData = DataManager.Instance.EnemyInternalDatas;

        for (int i = 0; i < enemyData.Count; i++)
        {
            enemyName[i].text = "名前: " + enemyData[i].name;
            age[i].text = "年齢: " + enemyData[i].age;
            bloodType[i].text = "血液型: " + enemyData[i].bloodType;
            mainHp[i].text = "HP: " + enemyData[i].mainHp;
            mainPower[i].text = "ATK: " + enemyData[i].mainPower;
            defence[i].text = "DEF: " + enemyData[i].mainDefense;
        }
    }


    // Use this for initialization
    void Start()
    {
        //EnemySpawn();
        EnemyRead();
    }

    // Update is called once per frame
    void Update()
    {
    }



    [SerializeField]
    GameObject[] enemyModel;
    void EnemySpawn()
    {
        // エネミーの数に応じて敵表示
        if (DataManager.Instance.EnemyInternalDatas.Count == 1)
        {
            Instantiate(enemyModel[0], new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (DataManager.Instance.EnemyInternalDatas.Count == 2)
        {
            Instantiate(enemyModel[0], new Vector3(-2, 0, 0), Quaternion.identity);
            Instantiate(enemyModel[1], new Vector3(2, 0, 0), Quaternion.identity);
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 3)
        {
            Instantiate(enemyModel[0], new Vector3(-3, 0, 0), Quaternion.identity);
            Instantiate(enemyModel[1], new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(enemyModel[2], new Vector3(3, 0, 0), Quaternion.identity);
        }
    }

}
