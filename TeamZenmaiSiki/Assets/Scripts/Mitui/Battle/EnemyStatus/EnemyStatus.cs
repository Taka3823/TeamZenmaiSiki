using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class EnemyStatus : MonoBehaviour {

    public struct CSVData
    {
        public int HP;
        public int ATK;
    }

    const int CSVDATA_ELEMENTS = 2;

    private CSVData[] csvData;

    public void ReadFile()
    {
        string path = Application.dataPath + "/CsvTest.csv";

        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        csvData = new CSVData[lines.Length];
        

        string[]  didCommaSeparationData = new string[lines.Length];

        char[] commaSpliter = { ',' };
        
        for(int i = 0; i < lines.Length; i++)
        {
            didCommaSeparationData = ReadCsvFoundation.DataSeparation(lines[i], commaSpliter, CSVDATA_ELEMENTS);
        }

        Debug.Log(csvData[0]);

    }

    // Use this for initialization
    void Start () {
        ReadFile();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Hit!");
    }
}
