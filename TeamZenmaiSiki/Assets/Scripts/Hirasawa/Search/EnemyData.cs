using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {


    public struct EnemyInternalDatas
    {
        public bool isbattle;

        public string name;
        public int age;
        public string sex;
        public string bloodType;
        public string[] memos;

        public int mainHp;
        public int mainPower;
        public int mainDefense;
        public string battleTexturePass;
        public string collisionPass;

        public int coreNum;
        public string[] coreName;
        public int[] coreHp;
        public int[] corePower;
        public int[] coreDefense;

    }
    public enum EnemyDataIndex
    {
        ISBATTLE,
        NAME,AGE,SEX,BLOODTYPE,MEMOS1,MEMOS2,
        MAINHP,MAINPOWER,MAINDEFENCE, BATTLETEXTUREPASS, COLLISIONPASS,
        CORENUM,CORENAME,COREHP,
        COREPOWER,COREDEFENCE,
        ENEMYDATAINDEXMAX
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
