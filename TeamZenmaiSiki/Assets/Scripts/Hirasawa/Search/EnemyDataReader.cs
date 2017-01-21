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
        string pass = "file://" + Application.streamingAssetsPath +
            "/CSVFiles/Search/EnemyData/Episode" + episodeNum.ToString() + "/Stage" + stageNum.ToString() + "/";

        string[] str = ReadCsvFoundation.ReadCsvData(pass + "Unit" + unitNumber.ToString() + ".csv");
        char[] commaSpliter = { ',' };

        List<EnemyData.EnemyInternalDatas> line = new List<EnemyData.EnemyInternalDatas>();
        for (int i = 1; i < str.Length; i++)//そのユニット内の敵の数だけデータを取得
        {
            string[] str2 = ReadCsvFoundation.NotOptionDataSeparation(str[i], commaSpliter, 25);
            //[Unit1の][２番目の敵]など・・・
            EnemyData.EnemyInternalDatas enemybuf = new EnemyData.EnemyInternalDatas();

            enemybuf.coreName = new string[3];
            enemybuf.coreHp = new int[3];
            enemybuf.corePower = new int[3];
            enemybuf.coreDefense = new int[3];
            enemybuf.memos = new string[2];
            enemybuf.isbattle = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.ISBATTLE)] == "はい";
            enemybuf.name = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.NAME)];
            enemybuf.age =  convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.AGE)]);
            enemybuf.sex = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.SEX)];
            enemybuf.bloodType = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.BLOODTYPE)];
            enemybuf.memos[0] = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.MEMOS1)];
            enemybuf.memos[1] = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.MEMOS2)];

            enemybuf.mainHp = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.MAINHP)]);
            enemybuf.mainPower = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.MAINPOWER)]);
            enemybuf.mainDefense = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.MAINDEFENCE)]);
            enemybuf.battleTexturePass= str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.BATTLETEXTUREPASS)];
            enemybuf.collisionPass= str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.COLLISIONPASS)];
            enemybuf.coreNum = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.CORENUM)]);

            int num = 3;

            for (int j = 0; j < num; j++)
            {
                enemybuf.coreName[j] = str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.CORENAME) + (j * 4)];
                enemybuf.coreHp[j] = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.CORENAME) + (j * 4) + 1]);
                enemybuf.corePower[j] = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.CORENAME) + (j * 4) + 2]);
                enemybuf.coreDefense[j] = convert(str2[System.Convert.ToInt32(EnemyData.EnemyDataIndex.CORENAME) + (j * 4) + 3]);
            }
            line.Add(enemybuf);
        }

        enemyData.Add(line);
    }
    private int convert(string _str)
    {
        if (_str == "")
        {
            return 0;
        }
        else
        {
            return int.Parse(_str);
        }
        
    }
}
