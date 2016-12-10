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
    [SerializeField]
    PlayerStatusRead playerStatusRead;

    /// <summary>
    /// エネミー
    /// </summary>
    /// <returns></returns>
    public List<Vector3> getPos() { return enemyStatusRead.getPos(); }

    public List<int> getBattleMainHp() { return enemyStatusRead.getBattleMainHp(); }
    //private List<int> getBattleMainHp = new EnemyStatusRead().getBattleMainHp();
    //public List<int> GetBattleMainHp
    //{
    //    get { return getBattleMainHp; }
    //    set { getBattleMainHp = value; }
    //}
    public List<int> getBattleCoreHp() { return enemyStatusRead.getBattleCoreHp(); }
    //private List<int> getBattleCoreHp = new EnemyStatusRead().getBattleCoreHp();
    //public List<int> GetBattleCoreHp
    //{
    //    get { return getBattleCoreHp; }
    //    set { getBattleCoreHp = value; }
    //}
    public List<int> getBattleCorePower() { return enemyStatusRead.getBattleCorePower(); }
    //private List<int> getBattleCorePower = new EnemyStatusRead().getBattleCorePower();
    //public List<int> GetBattleCorePower
    //{
    //    get { return getBattleCorePower; }
    //    set { getBattleCorePower = value; }
    //}

    public List<int> getBattleMainPower() { return enemyStatusRead.getBattleMainPower(); }
    public List<int> getBattleMainDefence() { return enemyStatusRead.getBattleMainDefence(); }
    public List<int> getBattleCoreDefence() { return enemyStatusRead.getBattleCoreDefence(); }
    public List<GameObject> getEnemyObject() { return enemyStatusRead.getEnemyObject(); }

    /// <summary>
    /// プレイヤー
    /// </summary>
    private int getBattlePlayerHp = new PlayerStatusRead().getBattlePlayerHp();
    public int GetBattlePlayerHp
    {
        get { return getBattlePlayerHp; }
        set { getBattlePlayerHp = value; }
    }
    public int getBattlePlayerAtk() { return playerStatusRead.getBattlePlayerAtk(); }

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
