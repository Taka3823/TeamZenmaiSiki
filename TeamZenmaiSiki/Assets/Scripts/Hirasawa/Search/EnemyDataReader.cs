using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class EnemyDataReader : MonoBehaviour
{

    // Use this for initialization
   

    private List<List<EnemyData.EnemyInternalDatas>> enemyData = new List<List<EnemyData.EnemyInternalDatas>>();

    

    public List<List<EnemyData.EnemyInternalDatas>> GetEnemyData
    {
        get { return enemyData; }
    }

    public void setEnemyData(List<List<EnemyData.EnemyInternalDatas>> _enemyData)
    {

        enemyData = _enemyData;
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReadData(int episodeNum, int stageNum, int unitNumber)
    {
        //ステージまでのパスを取得
        string pass = Application.dataPath +
            "/CSVFiles/Search/Episode" + episodeNum.ToString() + "/Stage" + stageNum.ToString() + "/";

        string[] str = ReadCsvFoundation.ReadCsvData(pass + "Unit" + (unitNumber).ToString() + ".csv");
        char[] commaSpliter = { ',' };

        List<EnemyData.EnemyInternalDatas> line = new List<EnemyData.EnemyInternalDatas>();
        for (int i = 0; i < str.Length; i++)//そのユニット内の敵の数だけデータを取得
        {
            string[] str2 = ReadCsvFoundation.DataSeparation(str[i], commaSpliter, 3);
            //[Unit1の][２番目の敵]など・・・
            EnemyData.EnemyInternalDatas enemybuf = new EnemyData.EnemyInternalDatas();
        
            enemybuf.name = str2[0];

            line.Add(enemybuf);
        }

        enemyData.Add(line);
    }
}
