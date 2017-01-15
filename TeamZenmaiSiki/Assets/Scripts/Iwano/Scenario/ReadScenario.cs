using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;

public class ReadScenario : MonoBehaviour
{
    private static ReadScenario instance;

    public static ReadScenario Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(ReadScenario);
                instance = (ReadScenario)FindObjectOfType(t);

                if (instance == null)
                {
                    Debug.LogError("ReadScenarioのインスタンスがnullです");
                }
            }
            return instance;
        }
    }

    public struct ScenariosData
    {
        public string command;           //命令コマンド
        public string drawName;          //描画する名前
        public string drawCharacterPos;  //キャラクターを描画する位置

        public List<string> sentences;

        public List<string> backGround;

        public List<string> backGroundBgm;     //BGM
        public List<string> soundEffect;       //SE

        //FIXED:
        //キャラクターが一つの要素中に二つ描画されることはないので、
        //List型にしているのは間違い。α版提出後に修正すること
        public List<string> charaSprite;     //描画するキャラクターのイラスト
    }

    enum ElementNames
    {
        command = 0,       //命令コマンド
        drawName,          //描画する名前
        drawCharacterPos,  //キャラクターを描画する位置
        sentences,         //本文

        backGround,        //背景

        backGroundBgm,     //BGM
        soundEffect,       //SE

        charaSprite        //描画するキャラクターのイラスト
    }

    public const string COMMAND_SENTENCE = "Sentence";
    public const string COMMAND_DRAW = "Draw";
    public const string COMMAND_BRANK = "Brank";

    private ScenariosData[] scenariosData;

    public ScenariosData[] ScenariosDatas
    {
        get { return scenariosData; }
    }

    //csvデータの要素数
    private const int CSVDATA_ELEMENTS = 8;

    //パスの名前
    //TIPS:必要なCSVファイルの名前をここに登録する
    string[,] scenarioDictionary =
    {
        //一章
        {
            "ep1",
            "ep1_2",
            "ep1_3",
            "ep1_4",//以下数合わせ
            "ep1_5",//
        },
        //二章
        {
            "ep2",
            "ep2_2",
            "ep2_3",
            "ep2_4",
            "ep2_5"
        },
        //三章
        {
            "ep3",
            "ep3_2",
            "ep3_3",
            "ep3_4",
            "ep3_5",//数合わせ
        },
        //四章
        {
            "ep4",
            "ep4_2",
            "ep4_3",
            "ep4_4",
            "ep4_5"//数合わせ
        },
        //五章
        {
            "ep5",
            "ep5_2",
            "ep5_3",
            "ep5_4",
            "ep5_5"//数合わせ
        },
        //六章
        {
            "ep6",
            "ep6_2",
            "ep6_3",
            "ep6_4",
            "ep6_5"//数合わせ
        },
        //七章
        {
            "ep7",
            "ep7_2",
            "ep7_3",
            "ep7_4",
            "ep7_5"//数合わせ
        },
        //八章
        {
            "ep8",
            "ep8_2",
            "ep8_3",
            "ep8_4",//以下数合わせ
            "ep8_5"
        },
        //九章
        {
            "ep9",
            "ep9_2",
            "ep9_3",//以下数合わせ
            "ep9_4",
            "ep9_5"
        },
        //十章
        {
            "ep10",
            "ep10_2",
            "ep10_3",
            "ep10_4",//以下数合わせ
            "ep10_5"
        }
    };

    string[] didCommaSeparationData;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
        }
    }

    //ファイル読み込みをしてくれる
    //第一引数…読み込みたいシナリオの名前を入力。
    //TIPS：内部でファイルまでのパスは記述しているので、名前だけで大丈夫
    public void ReadFile(int chapterNumber_,int sectionNumber_)
    {
        //読み込むパスを決定
        //FIXED:データマネージャーからのデータの受け取りは、
        //ReadFileを呼ぶときに引数で渡すほうがいいかも？

#if UNITY_STANDALONE
        string path = "file://" + Application.dataPath + "/CSVFiles/Scenario/Ep" + (DataManager.Instance.ScenarioChapterNumber + 1) + "/" + scenarioDictionary[chapterNumber_,sectionNumber_] + ".csv";
#elif UNITY_ANDROID
        string path = "jar:file://" + Application.dataPath + "/CSVFiles/Scenario/Ep" + (DataManager.Instance.ScenarioChapterNumber + 1) + "/" + scenarioDictionary[chapterNumber_,sectionNumber_] + ".csv";
#endif

        //Debug.Log(path);

        //行にわけられたデータを保存
        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        //行に分けたデータをカンマ分けして格納するデータのインスタンスを生成
        didCommaSeparationData = new string[lines.Length];

        char[] commaSplitter = { ',' };

        //現在のCSVの書式では行数分生成すると必要以上に
        //生成してしまうので、必要数だけ生成する
        int count = 0;
        string commandStorageTemp;

        for (int i = 1; i < lines.Length; i++)
        {
            commandStorageTemp = didCommaSeparationData[(int)ElementNames.command];

            if (commandStorageTemp != "")
            {
                count += 1;
            }
        }
        scenariosData = new ScenariosData[count + 1];

        for (int i = 1; i < lines.Length; i++)
        {
            //一行をカンマわけされたデータを格納
            didCommaSeparationData = DataSeparation(lines[i], commaSplitter, CSVDATA_ELEMENTS);
            
            scenariosData[i].command = didCommaSeparationData[(int)ElementNames.command];

            if (scenariosData[i].command != COMMAND_DRAW)
            {
                scenariosData[i].sentences = new List<string>();
                scenariosData[i].backGroundBgm = new List<string>();
                scenariosData[i].soundEffect = new List<string>();
            }

            scenariosData[i].charaSprite = new List<string>();
            scenariosData[i].backGround = new List<string>();

            if (scenariosData[i].command == COMMAND_SENTENCE ||
                scenariosData[i].command == COMMAND_BRANK)
            {
                StorageAll(i);
            }
            else if (scenariosData[i].command == "")
            {
                ForEmptyCommand(i);
            }
            else if (scenariosData[i].command == COMMAND_DRAW)
            {
                StorageForDrawCommand(i);
            }
        }
    }

    //コマンドが空だった場合の処理
    void ForEmptyCommand(int elementNum_)
    {
        //１つ前のコマンドが「Sentence」か「Brank」の場合、
        //１つ前の要素を指定して必要なデータだけ格納
        if (scenariosData[elementNum_ - 1].command == COMMAND_SENTENCE ||
            scenariosData[elementNum_ - 1].command == COMMAND_BRANK)
        {
            AdditionalStorage(elementNum_ - 1);
        }
        else if (scenariosData[elementNum_ - 1].command == "")
        {
            //１つ前のコマンドが空の場合、
            //２つ前の要素を指定して必要なデータだけ格納
            AdditionalStorage(elementNum_ - 2);
        }
    }

    //コマンドがDrawだった場合の処理
    void StorageForDrawCommand(int elementNum_)
    {
        scenariosData[elementNum_].drawCharacterPos = didCommaSeparationData[(int)ElementNames.drawCharacterPos];
        scenariosData[elementNum_].backGround.Add(didCommaSeparationData[(int)ElementNames.backGround]);
        scenariosData[elementNum_].charaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //Addするものだけ格納
    void AdditionalStorage(int elementNum_)
    {
        scenariosData[elementNum_].sentences.Add(didCommaSeparationData[(int)ElementNames.sentences]);

        scenariosData[elementNum_].backGroundBgm.Add(didCommaSeparationData[(int)ElementNames.backGroundBgm]);
        scenariosData[elementNum_].soundEffect.Add(didCommaSeparationData[(int)ElementNames.soundEffect]);

        scenariosData[elementNum_].backGround.Add(didCommaSeparationData[(int)ElementNames.backGround]);

        //TIPS：CharaSpriteは本来格納しなくてもいいが、ランダムアクセスする都合上、
        //空データも入れておかなければいけないため格納している
        scenariosData[elementNum_].charaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //全要素を格納
    void StorageAll(int elementNum_)
    {
        scenariosData[elementNum_].drawName = didCommaSeparationData[(int)ElementNames.drawName];
        scenariosData[elementNum_].drawCharacterPos = didCommaSeparationData[(int)ElementNames.drawCharacterPos];

        scenariosData[elementNum_].sentences.Add(didCommaSeparationData[(int)ElementNames.sentences]);

        scenariosData[elementNum_].backGround.Add(didCommaSeparationData[(int)ElementNames.backGround]);
       
        scenariosData[elementNum_].backGroundBgm.Add(didCommaSeparationData[(int)ElementNames.backGroundBgm]);
        scenariosData[elementNum_].soundEffect.Add(didCommaSeparationData[(int)ElementNames.soundEffect]);

        scenariosData[elementNum_].charaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //第一引数…ReadCsvData関数で一行にされたデータ
    //第二引数…渡されたデータを区切る文字
    //第三引数…第一引数のデータの要素数。for文の周回数
    string[] DataSeparation(string lines_, char[] spliter_, int trialNumber_)
    {
        //リターン値。カンマ分けしたデータを一行分格納する。
        string[] CommaSeparationData = new string[trialNumber_];
        for (int i = 0; i < trialNumber_; i++)
        {
            //１行にあるCsvDataの要素数分準備する
            string[] readStrData = new string[trialNumber_];
            //CsvDataを引数の文字で区切って1つずつ格納
            //readStrData = lines_.Split(spliter_, option);
            readStrData = lines_.Split(spliter_);
            //readStrDataをリターン値に格納
            CommaSeparationData[i] = readStrData[i];
        }
        return CommaSeparationData;
    }
}
