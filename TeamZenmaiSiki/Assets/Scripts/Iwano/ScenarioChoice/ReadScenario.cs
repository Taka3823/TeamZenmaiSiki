using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;

public class ReadScenario : MonoBehaviour
{
    public struct ScenariosData
    {
        public string command;           //命令コマンド
        public string drawName;          //描画する名前
        public string drawCharacterPos;  //キャラクターを描画する位置
        public List<string> sentences; //本文

        public List<string> backGroundBgm;     //BGM
        public List<string> soundEffect;       //SE

        public List<string> CharaSprite;     //描画するキャラクターのイラスト
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

    const string COMMAND_SENTENCE = "Sentence";
    const string COMMAND_DRAW = "Draw";
    const string COMMAND_BRANK = "Brank";

    private ScenariosData[] scenariosData;

    public ScenariosData[] ScenariosDatas
    {
        get { return scenariosData; }
    }

    //csvデータの要素数
    private const int CSVDATA_ELEMENTS = 8;

    //パスの名前
    //TIPS:必要なCSVファイルの名前をここに登録する
    private string[] scenarioDictionary =
    {
        "Sample.csv"
    };

    string[] didCommaSeparationData;

    void Start()
    {
        //TIPS:本来はこちらを使用する。
        //ReadFile(DataManager.Instance.ScenarioDictionaryNumber);
        
        //現在はデバッグ用のためこうしている
        ReadFile(0);
    }

    //ファイル読み込みをしてくれる
    //第一引数…読み込みたいシナリオの名前を入力。
    //TIPS：内部でファイルまでのパスは記述しているので、名前だけで大丈夫
    void ReadFile(int dictionaryNumber_)
    {
        //読み込むパスを決定
        //FIXED:データマネージャーからのデータの受け取りは、
        //      ReadFileを呼ぶときに引数で渡すほうがいいかも？
        string path = Application.dataPath + "/CSVFiles/Scenario/" + scenarioDictionary[dictionaryNumber_];

        //行にわけられたデータを保存
        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        //行に分けたデータをカンマ分けして格納するデータのインスタンスを生成
        didCommaSeparationData = new string[lines.Length];

        char[] commaSplitter = { ',' };

        //現在のCSVの書式では行数分生成すると必要以上に
        //生成してしまうので、必要数だけ生成する
        int count = 0;
        string commandStorageTemp;

        for (int i = 0; i < lines.Length; i++)
        {
            commandStorageTemp = didCommaSeparationData[(int)ElementNames.command];

            if (commandStorageTemp != "")
            {
                count += 1;
            }
        }

        scenariosData = new ScenariosData[count];

        for (int i = 0; i < lines.Length; i++)
        {
            //一行をカンマわけされたデータを格納
            didCommaSeparationData = DataSeparation(lines[i], commaSplitter, CSVDATA_ELEMENTS-1);

            scenariosData[i].command = didCommaSeparationData[(int)ElementNames.command];

            if (scenariosData[i].command != COMMAND_DRAW)
            {
                scenariosData[i].sentences = new List<string>();
                scenariosData[i].backGroundBgm = new List<string>();
                scenariosData[i].soundEffect = new List<string>();
            }

            scenariosData[i].CharaSprite   = new List<string>();

            if (scenariosData[i].command == COMMAND_SENTENCE)
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
        scenariosData[elementNum_].CharaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //Addするものだけ格納
    void AdditionalStorage(int elementNum_)
    {
        scenariosData[elementNum_].sentences.Add(didCommaSeparationData[(int)ElementNames.sentences]);

        scenariosData[elementNum_].backGroundBgm.Add(didCommaSeparationData[(int)ElementNames.backGroundBgm]);
        scenariosData[elementNum_].soundEffect.Add(didCommaSeparationData[(int)ElementNames.soundEffect]);

        //TIPS：CharaSpriteは本来格納しなくてもいいが、ランダムアクセスする都合上、
        //空データも入れておかなければいけないため格納している
        scenariosData[elementNum_].CharaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //全要素を格納
    void StorageAll(int elementNum_)
    {
        scenariosData[elementNum_].drawName = didCommaSeparationData[(int)ElementNames.drawName];
        scenariosData[elementNum_].drawCharacterPos = didCommaSeparationData[(int)ElementNames.drawCharacterPos];
        scenariosData[elementNum_].sentences.Add(didCommaSeparationData[(int)ElementNames.sentences]);

        scenariosData[elementNum_].backGroundBgm.Add(didCommaSeparationData[(int)ElementNames.backGroundBgm]);
        scenariosData[elementNum_].soundEffect.Add(didCommaSeparationData[(int)ElementNames.soundEffect]);

        scenariosData[elementNum_].CharaSprite.Add(didCommaSeparationData[(int)ElementNames.charaSprite]);
    }

    //第一引数…ReadCsvData関数で一行にされたデータ
    //第二引数…渡されたデータを区切る文字
    //第三引数…第一引数のデータの要素数。for文の周回数
    string[] DataSeparation(string lines_, char[] spliter_, int trialNumber_)
    {
        //TIPS:カンマとカンマの間に何もなかったら格納しないことにする設定だが、
        //ここでなにも格納しないことにしてしまうと、
        //後のFor文で""の情報確認ができないため、コメントアウト
        //System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

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
