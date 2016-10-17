using UnityEngine;
using System.Collections;

public class EnemyDataReader : MonoBehaviour {

	// Use this for initialization
    public struct EnemyData
    {
        Vector2 trancePosition;
        Vector2 scale;
        EnemyType.EnemyTypes enemyType;
        string[] texts;
    }
    private EnemyData enemyData;

    public EnemyData getEnemyData
    {
        get { return enemyData; }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
