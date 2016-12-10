using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyStatusRead : MonoBehaviour
{
    /// <summary>
    /// 不動値
    /// </summary>
    [SerializeField]
    Text[] enemyName;
    [SerializeField]
    Text[] mainHp;
    [SerializeField]
    Text[] coreHp;
    [SerializeField]
    Text[] mainPower;
    [SerializeField]
    Text[] corePower;
    [SerializeField]
    Text[] mainDefence;
    [SerializeField]
    Text[] coreDefence;
    [SerializeField]
    Text[] age;
    [SerializeField]
    Text[] bloodType;
    [SerializeField]
    Text[] memos;

    List<EnemyData.EnemyInternalDatas> enemyData;
    List<Vector3> pos = new List<Vector3>();

    [SerializeField]
    GameObject enemyPrefab;

    List<GameObject> enemyObject = new List<GameObject>();

    /// <summary>
    /// 動的値
    /// </summary>
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
            enemyName[i].text = enemyData[i].name;
            mainHp[i].text = "HP: " + enemyData[i].mainHp + "/" + battleMainHp[i];
            coreHp[i].text = "CHP: " + enemyData[i].coreHp[i] + "/" + battleCoreHp[i];
            mainPower[i].text = "ATK: " + enemyData[i].mainPower;
            mainDefence[i].text = "DEF: " + enemyData[i].mainDefense;
            age[i].text = "年齢: " + enemyData[i].age;
            bloodType[i].text = "血液型: " + enemyData[i].bloodType;
            for (int j = 0; j < enemyData[i].coreNum; j++)
            {
                corePower[i].text = "CATK: " + enemyData[i].corePower[i];
                coreDefence[i].text = "CDEF: " + enemyData[i].coreDefense[i];
            }
            for (int k = 0; k < 2; k++)
            {
                memos[i].text = enemyData[i].memos[k];
            }
        }
        
    }

    /// <summary>
    /// 敵の体力や攻撃など変動するもの
    /// </summary>
    void EnemySetup()
    {
        for (int i = 0; i < enemyData.Count; i++)
        {
            battleMainHp.Add(enemyData[i].mainHp);
            battleMainPower.Add(enemyData[i].mainPower);
            battleMainDefence.Add(enemyData[i].mainDefense);
            battleCoreHp.Add(enemyData[i].coreHp[i]);
            battleCorePower.Add(enemyData[i].corePower[i]);
            battleCoreDefence.Add(enemyData[i].coreDefense[i]);
        }
    }

    void Awake()
    {
        enemyData = DataManager.Instance.EnemyInternalDatas;
        EnemySpawn();
    }

    // Use this for initialization
    void Start()
    {
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
            pos.Add(new Vector3(0, 1.5f, 0));
        }

        if (DataManager.Instance.EnemyInternalDatas.Count == 2)
        {
            pos.Add(new Vector3(-3, 1.5f, 0));
            pos.Add(new Vector3(3, 1.5f, 0));
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 3)
        {
            pos.Add(new Vector3(-5, 1.5f, 0));
            pos.Add(new Vector3(0, 1.5f, 0));
            pos.Add(new Vector3(5, 1.5f, 0));
        }
        for (int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; ++i)
        {
            //GameObject enemyObject = Instantiate(enemyPrefab, pos[i], Quaternion.identity) as GameObject;
            Sprite sprite = new Sprite();
            string pass = "Sprits/Battle/EnemyCharacters/" + DataManager.Instance.EnemyInternalDatas[i].battleTexturePass;
            sprite = Resources.Load<Sprite>(pass);
            Debug.Log(sprite.name);
            //enemyPrefab.GetComponent<SpriteRenderer>().sprite = sprite;
            GameObject refObj = Instantiate(enemyPrefab, pos[i], Quaternion.identity) as GameObject;
            refObj.GetComponent<SpriteRenderer>().sprite = sprite;
            enemyObject.Add(refObj);
            //refobj.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public List<Vector3> getPos() { return pos; }
    public List<int> getBattleMainHp() { return battleMainHp; }
    public List<int> getBattleMainPower() { return battleMainPower; }
    public List<int> getBattleMainDefence() { return battleMainHp; }
    public List<int> getBattleCoreHp() { return battleCoreHp; }
    public List<int> getBattleCorePower() { return battleCorePower; }
    public List<int> getBattleCoreDefence() { return battleCoreDefence; }
    public List<GameObject> getEnemyObject() { return enemyObject; }
}
