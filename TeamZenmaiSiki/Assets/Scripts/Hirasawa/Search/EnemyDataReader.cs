using UnityEngine;
using System.Collections;

public class EnemyDataReader : MonoBehaviour {

	// Use this for initialization
    public struct EnemyData
    {
        public Vector2 trancePosition;
        public Vector2 scale;
        public EnemyType.EnemyTypes enemyType;
        public string[] texts;
    }

    private EnemyData enemyData;

    public EnemyData getEnemyData
    {
        get { return enemyData; }
    }
    public void setEnemyData(EnemyData _enemyData)
    {
        enemyData = _enemyData;
    }

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
