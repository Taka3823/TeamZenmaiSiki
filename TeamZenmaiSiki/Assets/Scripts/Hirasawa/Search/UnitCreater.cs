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
    private List<string> unittexlist;
    void Awake()
    {

    }
	void Start () {
        enemyDataReader = new EnemyDataReader();
        int episodeNum = 2;
        int stageNum = 1;
        createNum = new int();
        UnitPos = new List<Vector3>();
        unittexlist = new List<string>();
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
       "/CSVFiles/Search/EnemyData/Episode" + episodeNum.ToString() + "/Stage" + stageNum.ToString() + "/";

        //そのステージのUnitがどのポジションにいるかを取得
        string[] unitPosition = ReadCsvFoundation.ReadCsvData(pass + "UnitsPos.csv");
        char[] commaSpliter = { ',' };
        
        createNum = unitPosition.Length-1;//Unitの数を取得

        for (int i = 1; i < createNum+1; i++)
        {
           string[] strPos= ReadCsvFoundation.DataSeparation(unitPosition[i], commaSpliter, 4);
            Vector3 pos = new Vector3();
            pos.x = float.Parse(strPos[0]);
            pos.y = float.Parse(strPos[1]);
            pos.z = float.Parse(strPos[2]);
            UnitPos.Add(pos);
            unittexlist.Add(strPos[3]);
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
            unitObj.GetComponent<Unit>().SetNumber(i);
            List<EnemyData.EnemyInternalDatas> enemyDataList=new List<EnemyData.EnemyInternalDatas>();
            //unitObj.GetComponent<Unit>().setEnemyDatas()
            for (int j = 0; j < enemyDataReader.GetEnemyData[i].Count; j++)//Unit内の敵の数だけ
            {
                EnemyData.EnemyInternalDatas buf = new EnemyData.EnemyInternalDatas();
                buf = enemyDataReader.GetEnemyData[i][j];
                enemyDataList.Add(buf);//それぞれのデータを格納
            }
            unitObj.GetComponent<Unit>().setEnemyDatas(enemyDataList,UnitPos[i]);
           
            string pass =
            "Sprits/Search/Character";
            //Sprite image = Resources.Load<Sprite>(pass);
            //Debug.Log(image.name);
            Sprite sprite = new Sprite();
            unitObj.GetComponent<SpriteRenderer>().sprite = GetSprite(pass, unittexlist[i]);
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
    private Sprite GetSprite(string fileName, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
        return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
    }

}
