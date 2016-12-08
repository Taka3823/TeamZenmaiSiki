using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    Text[] mainDefence;
    [SerializeField]
    GameObject[] enemyModel;

    List<EnemyData.EnemyInternalDatas> enemyData;
    List<Vector3> pos = new List<Vector3>();
    List<int> battleMainHp = new List<int>();
    List<int> battleMainPower = new List<int>();
    List<int> battleMainDefence = new List<int>();
    List<int> battleCoreNum = new List<int>();
    List<int> battleCoreHp = new List<int>();
    List<int> battleCorePower = new List<int>();
    List<int> battleCoreDefence = new List<int>();

    /// <summary>
    /// テキスト表示のメソッド
    /// </summary>
    void EnemyTextUpdate()
    {
        List<EnemyData.EnemyInternalDatas> enemyData = DataManager.Instance.EnemyInternalDatas;

        for (int i = 0; i < enemyData.Count; i++)
        {
            enemyName[i].text = "名前: " + enemyData[i].name;
            age[i].text = "年齢: " + enemyData[i].age;
            bloodType[i].text = "血液型: " + enemyData[i].bloodType;
            mainHp[i].text = "HP: " + enemyData[i].mainHp + "/" + battleMainHp[i];
            mainPower[i].text = "POW: " + enemyData[i].mainPower;
            mainDefence[i].text = "DEF: " + enemyData[i].mainDefense;
        }
    }

    /// <summary>
    /// 敵の体力や攻撃など変動するもの
    /// </summary>
    void EnemySetup()
    {
        for(int i = 0; i < enemyData.Count; i++)
        {
            battleMainHp.Add(enemyData[i].mainHp);
            battleMainPower.Add(enemyData[i].mainPower);
            battleMainDefence.Add(enemyData[i].mainDefense);
            battleCoreHp.Add(enemyData[i].coreHp[i]);
            battleCorePower.Add(enemyData[i].corePower[i]);
            battleCoreDefence.Add(enemyData[i].coreDefense[i]);
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
    /// 敵数に応じて表示位置を決め敵表示
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
        for(int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; i++)
        {
            GameObject refobj = (GameObject)Instantiate(enemyModel[i], pos[i], Quaternion.identity);
            Sprite sprite = new Sprite();
            string pass = "Sprits/Battle/" + DataManager.Instance.EnemyInternalDatas[i].battleTexturePass;
            sprite = Resources.Load<Sprite>(pass);
            refobj.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public List<Vector3> getPos() { return pos; }
    public List<int> getBattleMainHp() { return battleMainHp; }
    public List<int> getBattleMainPower() { return battleMainPower; }
    public List<int> getBattleMainDefence() { return battleMainHp; }
    public List<int> getBattleCoreHp() { return battleCoreHp; }
    public List<int> getBattleCorePower() { return battleCorePower; }
    public List<int> getBattleCoreDefence() { return battleCoreDefence; }
}
