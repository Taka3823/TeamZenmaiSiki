using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {


    public struct EnemyInternalDatas
    {
        public string name;
        public EnemyType.EnemyTypes enemyType;
        public string serchTexturePass;
        public string[] texts;

        public int power;
        public int enemyHp;
        public int[] subHp;
        public int[] downPower;
        public string[] subName;
        public string[] skilName;
        public string battleTexturePass;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
