using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateExtraMission : MonoBehaviour
{
    const string PHYSICAL = "PHYSICAL";  //残り体力ｎ以上でクリア
    const string SPECIFIC = "SPECIFIC";  //特定の誰かを回収してクリア
    const string RECOVER = "RECOVER";   //回収対象者をｎ体以上回収してクリア
    const string BRAKE_CORE = "BRAKECORE"; //コアをｎ個以上破壊してクリア
    const string ALLKILL = "ALLKILL";   //全員倒してクリア

    [SerializeField]
    GameObject cOM;

    [SerializeField]
    Vector3[] distance = new Vector3[3];
    
    string[] drawMissionName = new string[3];

    void Start()
    {
        int chap = DataManager.Instance.ScenarioChapterNumber;
        int sect = DataManager.Instance.ScenarioSectionNumber;

        DicideMissionText(RECOVER, "3", 0);
        DicideMissionText(PHYSICAL, "40", 1);
        DicideMissionText(SPECIFIC, "フランクリン・パッカー", 2);

        for (int i = 0; i < 3; i++)
        {
            CreateSpecialMission(i);
        }

        //DicideMissionText(DataManager.Instance.DirectiveDatas[chap][sect].firstMission,  DataManager.Instance.DirectiveDatas[chap][sect].firstMissionAchievementCondition,  0);
        //DicideMissionText(DataManager.Instance.DirectiveDatas[chap][sect].secondMission, DataManager.Instance.DirectiveDatas[chap][sect].secondMissionAchievementCondition, 1);
        //DicideMissionText(DataManager.Instance.DirectiveDatas[chap][sect].thirdMission,  DataManager.Instance.DirectiveDatas[chap][sect].thirdMissionAchievementCondition,  2);
    }

    void CreateSpecialMission(int element_)
    {
        GameObject obj = Instantiate(cOM) as GameObject;

        obj.transform.parent = this.transform;

        obj.transform.localPosition = new Vector3(this.transform.localPosition.x + distance[element_].x, 
                                                  this.transform.localPosition.y - (95f * (element_ + 1)) + distance[element_].y,
                                                  this.transform.localPosition.z + distance[element_].z);

        obj.GetComponent<Text>().text = drawMissionName[element_];
    }
    
    void DicideMissionText(string missionName_,string missionAchivrmentConditionNum_,int num_)
    {   
        if (missionName_ == PHYSICAL)
        {
            drawMissionName[num_] = "残り体力" + missionAchivrmentConditionNum_ +"以上でクリア";
        }
        else if (missionName_ == SPECIFIC)
        {
            drawMissionName[num_] = missionAchivrmentConditionNum_ + "を回収してクリア";
        }
        else if (missionName_ == RECOVER)
        {
            drawMissionName[num_] = "回収対象者を" + missionAchivrmentConditionNum_ +"体以上回収してクリア";
        }
        else if (missionName_ == BRAKE_CORE)
        {
            drawMissionName[num_] = "コアを" + missionAchivrmentConditionNum_ + "個以上破壊してクリア";
        }
        else if (missionName_ == ALLKILL)
        {
            drawMissionName[num_] = "全員(人間含む)倒してクリア";
        }
    }
}
