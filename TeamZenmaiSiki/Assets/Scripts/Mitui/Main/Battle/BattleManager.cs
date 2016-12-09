using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    EnemyStatusRead enemyStatusRead;

    DataManager.PlayerDatas playerDatas;

    public List<Vector3> getPos() { return enemyStatusRead.getPos(); }
    public List<int> getBattleMainHp() { return enemyStatusRead.getBattleMainHp(); }
    public List<int> getBattleMainPower() { return enemyStatusRead.getBattleMainPower(); }
    public List<int> getBattleMainDefence() { return enemyStatusRead.getBattleMainDefence(); }
    public List<int> getBattleCoreHp() { return enemyStatusRead.getBattleCoreHp(); }
    public List<int> getBattleCorePower() { return enemyStatusRead.getBattleCorePower(); }
    public List<int> getBattleCoreDefence() { return enemyStatusRead.getBattleCoreDefence(); }
    public List<GameObject> getEnemyObject() { return enemyStatusRead.getEnemyObject(); }

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
