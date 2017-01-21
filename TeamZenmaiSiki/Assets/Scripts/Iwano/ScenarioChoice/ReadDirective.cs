using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using UnityEngine.UI;

using System.IO;

public class ReadDirective : MonoBehaviour
{
    //DirectiveDataのデータの順番と一致させる
    enum ElementName
    {
        scenarioNumberBaseData = 0,  //シナリオのナンバー。文字列で保管
        scenarioTitle,               //シナリオのタイトル
        missionObjective,            //今回の指令の名前（目的？）

        firstMission,                       //1つ目のミッション
        firstMissionAchievementCondition,   //1つ目のミッションの目標数や回収対象の名前

        secondMission,                      //2つ目のミッション
        secondMissionAchievementCondition,  //2つ目のミッションの目標数や回収対象の名前

        thirdMission,                       //3目のミッション
        thirdMissionAchievementCondition,   //3つ目のミッションの目標数や回収対象の名前

        collectionTargetName                //回収対象者名
    }

    private const int CSVDATA_ELEMENTS = 18;

    string[] didCommaSeparrationData;

    string[] pathName =
    {
        "Episode_1.csv",  //１章
        "Episode_2.csv",  //２章
        "Episode_3.csv",  //３章
        "Episode_4.csv",  //４章
        "Episode_5.csv",  //５章
        "Episode_6.csv",  //６章
        "Episode_7.csv",  //７章
        "Episode_8.csv",  //８章
        "Episode_9.csv",  //９章
        "Episode_10.csv", //１０章
    };

    private int lineLength;

    public int LineLength
    {
        get { return lineLength; }
        set { lineLength = value; }
    }

    //TIPS:引数には一1章なら「１」と入力する
    public void ReadFile(int chapterNumber_)
    {
#if UNITY_STANDALONE
         string path = "file://" + Application.streamingAssetsPath + "/CSVFiles/ScenarioChoice/" + pathName[chapterNumber_ - 1];
#elif UNITY_ANDROID
        string path = "jar:file://" + Application.dataPath + "!/assets" + "/CSVFiles/ScenarioChoice/" + pathName[chapterNumber_ - 1];
#endif

        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        didCommaSeparrationData = new string[lines.Length];

        lineLength = lines.Length;

        char[] commmaSplitter = { ',' };

        DataManager.DirectiveData[] tempDirectiveData = new DataManager.DirectiveData[lines.Length];

        for (int i = 1; i < lines.Length; i++)
        {
            didCommaSeparrationData = ReadCsvFoundation.NotOptionDataSeparation(lines[i], commmaSplitter, CSVDATA_ELEMENTS);

            tempDirectiveData[i - 1].scenarioNumberBaseData = didCommaSeparrationData[(int)ElementName.scenarioNumberBaseData];
            tempDirectiveData[i - 1].scenarioTitle = didCommaSeparrationData[(int)ElementName.scenarioTitle];
            tempDirectiveData[i - 1].missionObjective = didCommaSeparrationData[(int)ElementName.missionObjective];

            tempDirectiveData[i - 1].firstMission = didCommaSeparrationData[(int)ElementName.firstMission];
            tempDirectiveData[i - 1].firstMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.firstMissionAchievementCondition];

            tempDirectiveData[i - 1].secondMission = didCommaSeparrationData[(int)ElementName.secondMission];
            tempDirectiveData[i - 1].secondMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.secondMissionAchievementCondition];

            tempDirectiveData[i - 1].thirdMission = didCommaSeparrationData[(int)ElementName.thirdMission];
            tempDirectiveData[i - 1].thirdMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.thirdMissionAchievementCondition];

            tempDirectiveData[i - 1].collectionTargetName = new List<string>();

            for (int k = 0; k < 9; k++)
            {
                tempDirectiveData[i - 1].collectionTargetName.Add(didCommaSeparrationData[(int)ElementName.collectionTargetName + k]);
            }
        }

        DataManager.Instance.DirectiveDatas.Add(tempDirectiveData);
    }
}
