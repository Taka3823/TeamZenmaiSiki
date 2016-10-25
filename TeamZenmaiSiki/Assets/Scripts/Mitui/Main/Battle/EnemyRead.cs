using UnityEngine;
using System.Collections;

public class EnemyRead : MonoBehaviour {


    const int CSVDATA_ELEMENTS = 11;

    public EnemyData.EnemyInternalDatas[] enemyInternalDatas;

    public void ReadFile()
    {
        string path = Application.dataPath + "/CsvTest.csv";

        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        enemyInternalDatas = new EnemyData.EnemyInternalDatas[lines.Length];

        string[] didCommaSeparationData = new string[lines.Length];

        char[] commaSpliter = { ',' };

        for (int i = 0; i < lines.Length; i++)
        {
            didCommaSeparationData = ReadCsvFoundation.DataSeparation(lines[i], commaSpliter, CSVDATA_ELEMENTS);
            Debug.Log(didCommaSeparationData[0]);
        }
    }

    // Use this for initialization
    void Start()
    {
        ReadFile();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
