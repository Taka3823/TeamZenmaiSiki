using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    EnemyStatusRead enemyStatusRead;

    /// <summary>
    /// エネミー
    /// </summary>
    /// <returns></returns>
    public List<Vector3> getPos() { return enemyStatusRead.getPos(); }

    public List<int> getBattleMainHp() { return enemyStatusRead.getBattleMainHp(); }
   
    public List<int> getBattleCoreHp() { return enemyStatusRead.getBattleCoreHp(); }
    
    public List<int> getBattleCorePower() { return enemyStatusRead.getBattleCorePower(); }

    public List<int> getBattleMainPower() { return enemyStatusRead.getBattleMainPower(); }
    public List<int> getBattleMainDefence() { return enemyStatusRead.getBattleMainDefence(); }
    public List<int> getBattleCoreDefence() { return enemyStatusRead.getBattleCoreDefence(); }
    public List<GameObject> getEnemyObject() { return enemyStatusRead.getEnemyObject(); }

    //FIXME:β版応急処置
    /// <summary>
    /// まるも加筆
    /// </summary>
    /// <param name="_index"></param>
    public void removeEnemyData(int _index)
    {
        enemyStatusRead.removeEnemyData(_index);
    }
    public string getKillName(int _index)
    {
        return enemyStatusRead.getKillName(_index);
    }

    /// <summary>
    /// プレイヤー
    /// </summary>
    private int getBattlePlayerHp;
    public int GetBattlePlayerHp
    {
        get { return getBattlePlayerHp; }
        set { getBattlePlayerHp = value; }
    }

	private int getBattlePlayerAtk;
    public int GetBattlePlayerAtk() { return getBattlePlayerAtk; }

    void Awake()
    {
        if (instance == null) { instance = this; }
		FadeManager.Instance.FadeOut(0.8f);
		getBattlePlayerHp = DataManager.Instance.PlayerData.hp;
		getBattlePlayerAtk = DataManager.Instance.PlayerData.attack;
    }
}
