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
	void Awake ()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //*****************************************************//
    //                                                     //
    //                     岩野記述欄                       //
    //                                                     //
    //*****************************************************//

    //呼び出すシナリオのID
    private int scenarioDictionaryNumber;

    public int ScenarioDictionaryNumber
    {
        get { return scenarioDictionaryNumber; }
        set { scenarioDictionaryNumber = value; }
    }

    private bool isAppAwake = false;

    public bool IsAppAwake
    {
        get { return isAppAwake; }
        set { isAppAwake = value; }
    }

    //仮で製作
    //指令書に書かれている目標の名前を仮保存
    //TIPS:CSVデータから格納する前に、Clearをかけてから格納すること
    /***********************************************/
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
