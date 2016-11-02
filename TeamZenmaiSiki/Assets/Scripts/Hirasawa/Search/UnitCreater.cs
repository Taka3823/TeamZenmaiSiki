using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitCreater : MonoBehaviour {

    [SerializeField]
    GameObject unit;

    // Use this for initialization
    EnemyDataReader enemyDataReader;
   
    private int createNum;
    private List<Vector3> UnitPos;
    void Awake()
    {

    }
	void Start () {
        enemyDataReader = new EnemyDataReader();
        int episodeNum = 1;
        int stageNum = 1;
        createNum = new int();
        UnitPos = new List<Vector3>();
        ReadUnitData(episodeNum, stageNum);
        ReadEnemyData(episodeNum, stageNum);
        CreateUnit();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void ReadUnitData(int episodeNum,int stageNum)//Unitの
    {
        string pass = Application.dataPath +
       "/CSVFiles/Search/Episode" + episodeNum.ToString() + "/Stage" + stageNum.ToString() + "/";

        //そのステージのUnitがどのポジションにいるかを取得
        string[] unitPosition = ReadCsvFoundation.ReadCsvData(pass + "UnitsPos.csv");
        char[] commaSpliter = { ',' };
        
        createNum = unitPosition.Length;//Unitの数を取得

        for (int i = 0; i < createNum; i++)
        {
           string[] strPos= ReadCsvFoundation.DataSeparation(unitPosition[i], commaSpliter, 3);
            Vector3 pos = new Vector3();
            pos.x = float.Parse(strPos[0]);
            pos.y = float.Parse(strPos[1]);
            pos.z = float.Parse(strPos[2]);
            UnitPos.Add(pos);
        }
       
       

        //Debug.Log(enemyDataReader.getEnemyData[0][0].name);
    }
    private void CreateUnit()
    {
        //Unit u;
      
        for(int i = 0; i < createNum; i++)
        {
            GameObject unitObj = Instantiate(unit, UnitPos[i], Quaternion.Euler(0, 0, 0)) as GameObject;
            //u = unitObj.GetComponent<Unit>();
            //u.setIsViewData(false);

            unitObj.GetComponent<Unit>().setIsViewData(false);
            List<EnemyData.EnemyInternalDatas> enemyDataList=new List<EnemyData.EnemyInternalDatas>();
            //unitObj.GetComponent<Unit>().setEnemyDatas()
            for (int j = 0; j < enemyDataReader.GetEnemyData[i].Count; j++)//Unit内の敵の数だけ
            {
                EnemyData.EnemyInternalDatas buf = new EnemyData.EnemyInternalDatas();
                buf = enemyDataReader.GetEnemyData[i][j];
                enemyDataList.Add(buf);//それぞれのデータを格納
            }
            unitObj.GetComponent<Unit>().setEnemyDatas(enemyDataList);
            //dataBuf.name = "aaa";
            //unitObj.GetComponent<Unit>().setEnemyDatas(dataBuf);
        }
        
    }

    private void ReadEnemyData(int episodeNum, int stageNum)
    {
        //そのステージのUnitの数だけデータを取得
        for (int i = 0; i < createNum; i++)
        {
            enemyDataReader.ReadData(episodeNum, stageNum, i + 1);
        }
    }
    private void AddEnemyDataToUnits()
    {

    }
}
