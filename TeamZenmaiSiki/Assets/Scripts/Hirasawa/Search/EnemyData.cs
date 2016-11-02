using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {


    public struct EnemyInternalDatas
    {
        public string name;
        public int age;
        public string sex;
        public string bloodType;
        public string memos;
        public string serchTexturePass;
      
        public int mainHp;
        public int mainPower;
        public int mainDefense;
        public int coreNum;
        public string[] coreName;
        public int[] corebHp;
        public int[] corePower;
        public int[] coreDefense;
        public string battleTexturePass;
        public string collisionPass;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
