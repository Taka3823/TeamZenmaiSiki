using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.IO;

public class ReadCsvFoundation : MonoBehaviour
{
    //第一引数…読み込むCSVデータファイルのパス　
    public static string[] ReadCsvData(string path_)
    {
        //ファイル読み込み
        //StreamReader sr = new StreamReader(path_);

        WWW www = new WWW(path_);

        while(!www.isDone)
        {
            //ファイルの読み込みが終わるまで待つ処理
            Debug.Log("思い");
        }

        //stringに変換
        string strStream = www.text;//sr.ReadToEnd();

        //カンマとカンマの間に何もなかったら格納しないことにする設定
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        //行に分ける
        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        return lines;
    }

    //第一引数…ReadCsvData関数で一行にされたデータ
    //第二引数…渡されたデータを区切る文字
    //第三引数…第一引数のデータの要素数。for文の周回数
    public static string[] DataSeparation(string lines_, char[] spliter_, int trialNumber_)
    {
        //カンマとカンマの間に何もなかったら格納しないことにする設定
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        //リターン値。カンマ分けしたデータを一行分格納する。
        string[] CommaSeparationData = new string[trialNumber_];
        for (int i = 0; i < trialNumber_; i++)
        {
            //１行にあるCsvDataの要素数分準備する
            string[] readStrData = new string[trialNumber_];
            //CsvDataを引数の文字で区切って1つずつ格納
            readStrData = lines_.Split(spliter_, option);
            //readStrDataをリターン値に格納
            CommaSeparationData[i] = readStrData[i];
        }
        return CommaSeparationData;
    }

    public static string[] NotOptionDataSeparation(string lines_, char[] spliter_, int trialNumber_)
    {
        //リターン値。カンマ分けしたデータを一行分格納する。
        string[] CommaSeparationData = new string[trialNumber_];
        for (int i = 0; i < trialNumber_; i++)
        {
            //１行にあるCsvDataの要素数分準備する
            string[] readStrData = new string[trialNumber_];
            //CsvDataを引数の文字で区切って1つずつ格納
            readStrData = lines_.Split(spliter_);
            //readStrDataをリターン値に格納
            CommaSeparationData[i] = readStrData[i];
        }
        return CommaSeparationData;
    }
}
