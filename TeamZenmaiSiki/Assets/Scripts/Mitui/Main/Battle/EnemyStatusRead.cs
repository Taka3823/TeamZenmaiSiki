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
	List<Text> infoText;

    List<EnemyData.EnemyInternalDatas> enemyData = new List<EnemyData.EnemyInternalDatas>();
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
        for (int i = 0; i < infoText.Count; i++)
		{
			infoText[i].text = ConnectEnemyStatus(i);
        }
    }

	string ConnectEnemyStatus(int _index)
	{
		string _result = "";
		if (_index >= enemyData.Count) return _result;

		_result += enemyData[_index].name + "\n"
				+ "生命力: " + battleMainHp[_index].ToString() + "/" + enemyData[_index].mainHp.ToString() + "\n"
				+ "攻撃力: " + enemyData[_index].mainPower.ToString() + "\n"
				+ "防御力: " + enemyData[_index].mainDefense.ToString() + "\n"
				+ "年齢: "   + enemyData[_index].age.ToString() + "\n"
				+ "血液型: " + enemyData[_index].bloodType + "\n" + "\n";

		if (enemyData[_index].coreNum >= 0)
		{
			for(int i = 0; i < enemyData[_index].coreNum; i++)
			{
				_result += "コア耐久力: " + battleCoreHp[_index].ToString() + "/" + enemyData[_index].coreHp[i].ToString() + "\n"
						+  "コア攻撃力: " + enemyData[_index].corePower[i].ToString() + "\n"
						+  "コア防御力: " + enemyData[_index].coreDefense[i].ToString() + "\n" + "\n";
			}
		}

		_result += enemyData[_index].memos[0] + "\n" + enemyData[_index].memos[1];

		return _result;
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
            for (int k = 0; k < enemyData[i].coreNum; k++)
            {
                battleCoreHp.Add(enemyData[i].coreHp[k]);
                battleCorePower.Add(enemyData[i].corePower[k]);
                battleCoreDefence.Add(enemyData[i].coreDefense[k]);
            }
        }
    }

    /// <summary>
    /// 敵数に応じて表示位置を決め敵表示
    /// </summary>
    void EnemySpawn()
    {
        if (DataManager.Instance.EnemyInternalDatas.Count == 1)
        {
            pos.Add(new Vector3(0, 0.9f, 0));
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 2)
        {
            pos.Add(new Vector3(-3, 0.9f, 0));
            pos.Add(new Vector3( 3, 0.9f, 0));
        }
        if (DataManager.Instance.EnemyInternalDatas.Count == 3)
        {
            pos.Add(new Vector3(-5, 0.9f, 0));
            pos.Add(new Vector3( 0, 0.9f, 0));
            pos.Add(new Vector3( 5, 0.9f, 0));
        }
        for (int i = 0; i < DataManager.Instance.EnemyInternalDatas.Count; ++i)
        {
            Sprite sprite = new Sprite();
            string pass = "Sprits/Battle/EnemyCharacters/" + DataManager.Instance.EnemyInternalDatas[i].battleTexturePass;
            sprite = Resources.Load<Sprite>(pass);
            Debug.Log(sprite.name);
            GameObject refObj = Instantiate(enemyPrefab, pos[i], Quaternion.identity) as GameObject;
            refObj.GetComponent<SpriteRenderer>().sprite = sprite;
            enemyObject.Add(refObj);
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

    //FIXME:まるも加筆
    public string getKillName(int _index)
    {
        return enemyData[_index].name;
    }

    public List<Vector3> getPos() { return pos; }
    public List<int> getBattleMainHp() { return battleMainHp; }
    public List<int> getBattleMainPower() { return battleMainPower; }
    public List<int> getBattleMainDefence() { return battleMainDefence; }
    public List<int> getBattleCoreHp() { return battleCoreHp; }
    public List<int> getBattleCorePower() { return battleCorePower; }
    public List<int> getBattleCoreDefence() { return battleCoreDefence; }

    public void setBattleMainHp(int _index,int _value)
    {
        battleMainHp[_index] = _value;
    }
    public void setBattleCoreHp(int _index, int _value)
    {
        battleCoreHp[_index] = _value;
    }
    public void setBattleCorePower(int _index, int _value)
    {
        battleCorePower[_index] = _value;
    }

    public List<GameObject> getEnemyObject() { return enemyObject; }

    public void removeEnemyData(int _index)
    {
        enemyData.RemoveAt(_index);
    }
}
