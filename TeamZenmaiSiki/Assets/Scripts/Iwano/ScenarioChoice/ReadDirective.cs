using UnityEngine;
using System.Collections;

using System.Collections.Generic;

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
        "SampleDirective.csv",  //一節
        //"SampleDirective_2",  //二節
    };

    //TIPS:引数には一1章なら「１」と入力する
    public void ReadFile(int scenarioNumberData_)
    {
        string path = Application.dataPath + "/CSVFiles/ScenarioChoice/" + pathName[scenarioNumberData_ - 1];

        string[] lines = ReadCsvFoundation.ReadCsvData(path);

        didCommaSeparrationData = new string[lines.Length];

        char[] commmaSplitter = { ',' };

        DataManager.DirectiveData[] tempDirectiveData = new DataManager.DirectiveData[lines.Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            didCommaSeparrationData = ReadCsvFoundation.NotOptionDataSeparation(lines[i], commmaSplitter, CSVDATA_ELEMENTS);
            
            tempDirectiveData[i].scenarioNumberBaseData = didCommaSeparrationData[(int)ElementName.scenarioNumberBaseData];
            tempDirectiveData[i].scenarioTitle = didCommaSeparrationData[(int)ElementName.scenarioTitle];
            tempDirectiveData[i].missionObjective = didCommaSeparrationData[(int)ElementName.missionObjective];

            tempDirectiveData[i].firstMission = didCommaSeparrationData[(int)ElementName.firstMission];
            tempDirectiveData[i].firstMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.firstMissionAchievementCondition];

            tempDirectiveData[i].secondMission = didCommaSeparrationData[(int)ElementName.secondMission];
            tempDirectiveData[i].secondMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.secondMissionAchievementCondition];

            tempDirectiveData[i].thirdMission = didCommaSeparrationData[(int)ElementName.thirdMission];
            tempDirectiveData[i].thirdMissionAchievementCondition = didCommaSeparrationData[(int)ElementName.thirdMissionAchievementCondition];

            tempDirectiveData[i].collectionTargetName = new List<string>();

            for(int k = 0;k < 9;k++)
            {
                tempDirectiveData[i].collectionTargetName.Add(didCommaSeparrationData[(int)ElementName.collectionTargetName + k]);
            }
        }

        DataManager.Instance.DirectiveDatas.Add(tempDirectiveData);
    }
}
