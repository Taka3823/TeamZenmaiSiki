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
    List<EnemyData.EnemyInternalDatas> enemyData;
    [SerializeField]
    GameObject[] enemyModel;
    List<Vector3> pos = new List<Vector3>();
    List<int> enemyHp = new List<int>();

    void EnemyTextUpdate()
    {
        for (int i = 0; i < enemyData.Count; i++)
        {
            enemyName[i].text = "名前: " + enemyData[i].name;
            age[i].text = "年齢: " + enemyData[i].age;
            bloodType[i].text = "血液型: " + enemyData[i].bloodType;
            mainHp[i].text = "HP: " + enemyData[i].mainHp + "/" + enemyHp[i];
            mainPower[i].text = "ATK: " + enemyData[i].mainPower;
            defence[i].text = "DEF: " + enemyData[i].mainDefense;
        }
    }

    void EnemySetup()
    {
        for (int i = 0; i < enemyData.Count; i++)
        {
            enemyHp.Add(enemyData[i].mainHp);
        }

    }

    // Use this for initialization
    void Start()
    {
        enemyData = DataManager.Instance.EnemyInternalDatas;
        EnemySpawn();
        EnemySetup();
        EnemyTextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyTextUpdate();
    }


    /// <summary>
    /// 敵の数に応じて敵表示
    /// </summary>
    void EnemySpawn()
    {
        if (DataManager.Instance.EnemyInternalDatas.Count == 1)
        {
            pos.Add(new Vector3(0, 0, 0));
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 2)
        {
            pos.Add(new Vector3(-3, 0, 0));
            pos.Add(new Vector3(3, 0, 0));
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 3)
        {
            pos.Add(new Vector3(-4, 0, 0));
            pos.Add(new Vector3(0, 0, 0));
            pos.Add(new Vector3(4, 0, 0));
        }
        for (int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; i++)
        {
            GameObject refobj = (GameObject)Instantiate(enemyModel[i], pos[i], Quaternion.identity);
            Sprite sprite = new Sprite();
            string pass = "Sprits/Battle/" + DataManager.Instance.EnemyInternalDatas[i].battleTexturePass;
            sprite = Resources.Load<Sprite>(pass);
            refobj.GetComponent<SpriteRenderer>().sprite = sprite;
        }


    }
    public List<Vector3> GetPos() { return pos; }
    public List<int> GetEnemyHp() { return enemyHp; }

}
