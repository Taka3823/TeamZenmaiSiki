using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class EnemyStatus : MonoBehaviour {

    public struct CSVData
    {
        string Name;
        public int Hp;
        public int Atk;
        string Group;
    }

    const int CSVDATA_ELEMENTS = 4;

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
            Debug.Log(didCommaSeparationData[0]);
        }   
    }

    // Use this for initialization
    void Start () {
        ReadFile();
        	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
