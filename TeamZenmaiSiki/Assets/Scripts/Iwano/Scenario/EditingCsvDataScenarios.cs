using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EditingCsvDataScenarios : MonoBehaviour
{
    enum CsvReadingCommand
    {
        Draw = 0,  //キャラクターを描画するだけ 
        Sentence,  //文章を出しながらキャラクターを描画する
        Brank,     //ナレーションなどのキャラクターの名前を描画しない

        None = -1
    }

    struct ScenariosData
    {
        CsvReadingCommand command;
        string   drawName;
        string   drawCharacterPos;
        List<string[]> sentences;

        string backGroundBgm;
        string soundEffect;

        string[] CharaSprite;
    }

    ScenariosData[] scenariosData;

    void Awake()
    {



        //scenariosData = new ScenariosData[]
    }








}
