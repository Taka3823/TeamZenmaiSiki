using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ReadScenario : MonoBehaviour
{
    public struct ScenariosData
    {
        public string command;           //命令コマンド
        public string drawName;          //描画する名前
        public string drawCharacterPos;  //キャラクターを描画する位置
        public List<string[]> sentences; //本文

        public string backGroundBgm;     //BGM
        public string soundEffect;       //SE

        public string[] CharaSprite;     //描画するキャラクターのイラスト
    }

    enum ElementNames
    {
        command = 0,       //命令コマンド
        drawName,          //描画する名前
        drawCharacterPos,  //キャラクターを描画する位置
        sentences,         //本文

        backGroundBgm,     //BGM
        soundEffect,       //SE

        charaSprite        //描画するキャラクターのイラスト
    }

    private ScenariosData[] scenariosData;

    public ScenariosData[] ScenariosDatas
    {
        get { return scenariosData; }
    }

    //csvデータの要素数
    private const int CSVDATA_ELEMENTS = 7;

    //パスの名前
    private string[] scenarioDictionary =
    {
        "Sample.csv"
    };

    string[] didCommaSeparationData;

    void Start()
    {




    }

    void Update()
    {

    }

    void ReadFile()
    {
        string path = Application.dataPath + "/CSVFiles/" + scenarioDictionary[DataManager.Instance.ScenarioDictionaryNumber];

        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        scenariosData = new ScenariosData[lines.Length];

        didCommaSeparationData = new string[lines.Length];

        char[] commaSplitter = { ',' };

        for (int i = 0; i < lines.Length; i++)
        {
            didCommaSeparationData = ReadCsvFoundation.DataSeparation(lines[i], commaSplitter, CSVDATA_ELEMENTS);

            scenariosData[i].command = didCommaSeparationData[(int)ElementNames.command];

            if (scenariosData[i].command == "Sentence")
            {
                StorageForSentenceCommand(i);
            }
            else if (scenariosData[i].command == "")
            {
                StorageForEmptyCommand(i);
            }
            else if (scenariosData[i].command == "Draw")
            {
                StorageForDrawCommand(i);
            }
            else if (scenariosData[i].command == "Brank")
            {
                StorageForBrankCommand(i);
            }
        }
    }

    /**
     public string command;           //命令コマンド
     public string drawName;          //描画する名前
     public string drawCharacterPos;  //キャラクターを描画する位置
     public List<string[]> sentences; //本文

     public string backGroundBgm;     //BGM
     public string soundEffect;       //SE

     public string[] CharaSprite;
    /**/
    void StorageForSentenceCommand(int elementNum_)
    {
        scenariosData[elementNum_].drawName = didCommaSeparationData[(int)ElementNames.drawName];
        scenariosData[elementNum_].drawCharacterPos = didCommaSeparationData[(int)ElementNames.drawCharacterPos];
        //scenariosData[elementNum_]

    }

    void StorageForBrankCommand(int elementNum_)
    {

    }

    void StorageForDrawCommand(int elementNum_)
    {

    }

    void StorageForEmptyCommand(int elementNum_)
    {

    }
}
