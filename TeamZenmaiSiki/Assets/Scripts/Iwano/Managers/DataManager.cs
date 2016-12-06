using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //デバッグ用
        DataManager.Instance.TargetName.Add("クック");
        DataManager.Instance.TargetName.Add("エンリケス");
        DataManager.Instance.TargetName.Add("アリアス");
        DataManager.Instance.TargetName.Add("サリー");
        DataManager.Instance.TargetName.Add("ベネット");
    }

    //*****************************************************//
    //                                                     //
    //                     岩野記述欄                       //
    //                                                     //
    //*****************************************************//

    //呼び出すシナリオのEpナンバー
    private int scenarioChapterNumber;

    public int ScenarioChapterNumber
    {
        get { return scenarioChapterNumber; }
        set { scenarioChapterNumber = value; }
    }

    //呼び出すシナリオのEpの何節かの数字
    private int scenarioSectionNumber;

    public int ScenarioSectionNumber
    {
        get { return scenarioSectionNumber; }
        set { scenarioSectionNumber = value; }
    }

    //アプリを一度起動したかどうか
    private bool isAppAwake = false;

    public bool IsAppAwake
    {
        get { return isAppAwake; }
        set { isAppAwake = value; }
    }

    //シナリオが何章まであるかの数字
    //onst int FINAL_VOLUME_NUMBER = 1;


    //解放されているシナリオの情報を確保している
    //FIX:jsonでデータを格納するべき。
    //今はそこまでの余裕がないのでここで格納しておく    
    //TIPS:[章,節]
    //private bool[,] openScenario = { };

    //public bool[,] OpenScenario
    //{
    //    get { return openScenario; }
    //    set { openScenario = value; }
    //}


    //指令書のデータ項目

    public struct DirectiveData
    {
        public string scenarioNumberBaseData;            //シナリオのナンバー。文字列で保管
        public string scenarioTitle;                     //シナリオのタイトル
        public string missionObjective;                  //今回の指令の名前（目的？）

        public string firstMission;                      //1つ目のミッション
        public string firstMissionAchievementCondition;  //1つ目のミッションの目標数や回収対象の名前

        public string secondMission;                     //2つ目のミッション
        public string secondMissionAchievementCondition; //2つ目のミッションの目標数や回収対象の名前

        public string thirdMission;                      //3つ目のミッション
        public string thirdMissionAchievementCondition;  //3つ目のミッションの目標数や回収対象の名前

        public List<string> collectionTargetName;        //回収対象者名
    }

    private List<DirectiveData[]> directiveDatas = new List<DirectiveData[]>();

    public List<DirectiveData[]> DirectiveDatas
    {
        get { return directiveDatas; }
        set { directiveDatas = value; }
    }

    //シナリオのナンバーを”string型”で返す
    public string GetScenarioNumber(int chapterNum_,int sectionNum_)
    {
        return DirectiveDatas[chapterNum_][sectionNum_].scenarioNumberBaseData;
    }

    //指令書に書かれている目標の名前を仮保存
    //TIPS:CSVデータから格納する前に、Clearをかけてから格納すること
    private List<string> targetName = new List<string>();

    public List<string> TargetName
    {
        get { return targetName; }
        set { targetName = value; }
    }

    private int targetNumber = 0;

    public int TargetNumber
    {
        get { return targetNumber; }
        set { targetNumber = value; }
    }

    /**********************************************/

    //*****************************************************//
    //                                                     //
    //                     平沢記述欄                       //
    //                                                     //
    //*****************************************************//

    private List<EnemyData.EnemyInternalDatas> enemyInternalDatas = new List<EnemyData.EnemyInternalDatas>();

    public List<EnemyData.EnemyInternalDatas> EnemyInternalDatas
    {
        get { return enemyInternalDatas; }
        set { enemyInternalDatas = value; }
    }

    Vector3 pos = new Vector3();

    private Vector3 cameraPos = new Vector3();

    public Vector3 CameraPos
    {
        get { return cameraPos; }
        set { cameraPos = value; }
    }

    private int playerHp;

    public int PlayerHp
    {
        get { return playerHp; }
        set { playerHp = value; }
    }
    private int playerMaxHp;

    public int PlayerMaxHp
    {
        get { return playerMaxHp; }
        set { playerMaxHp = value; }
    }
    private List<bool> notActiveUnit = new List<bool>();

    public List<bool> NotActiveUnit
    {
        get { return notActiveUnit; }
        set { notActiveUnit = value; }
    }

    private List<string> killNames = new List<string>();

    public List<string> KillNames
    {
        get { return killNames; }
        set { killNames = value; }
    }

    private List<bool> isTargetKilled = new List<bool>();

    public List<bool> IsTargetKilled
    {
        get { return isTargetKilled; }
        set { isTargetKilled = value; }
    }

    private int killNum;

    public int KillNum
    {
        get { return killNum; }
        set { killNum = value; }
    }

    private List<bool> isRemoveHandPocketBook = new List<bool>();//最初にCSV読み込み

    public List<bool> IsRemoveHandPocketBook
    {
        get { return isRemoveHandPocketBook; }
        set { isRemoveHandPocketBook = value; }
    }

    //達成した指令リスト
    private List<bool> isAchieveBounties = new List<bool>();

    public List<bool> IsAchieveBounties
    {
        get { return isAchieveBounties; }
        set { isAchieveBounties = value; }
    }


    public struct PlayerDatas
    {
        int hp;
        int attack;
    }

    private PlayerDatas playerData = new PlayerDatas();

    public PlayerDatas PlayerData
    {
        get { return playerData; }
        set { playerData = value; }

    }

    int episodeNum;
    int sectionNum;

    //*****************************************************//
    //                                                     //
    //                     丸本記述欄                       //
    //                                                     //
    //*****************************************************//

    //*****************************************************//
    //                                                     //
    //                     三井記述欄                       //
    //                                                     //
    //*****************************************************//

    //*****************************************************//
    //                                                     //
    //                     竹中記述欄                       //
    //                                                     //
    //*****************************************************//
}
