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
    public List<Vector3> GetPos() { return enemyStatusRead.GetPos(); }
    public List<int> GetEnemyHp() { return enemyStatusRead.GetEnemyHp(); }


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
