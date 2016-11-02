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

    //*****************************************************//
    //                                                     //
    //                     平沢記述欄                       //
    //                                                     //
    //*****************************************************//

    private List<EnemyData.EnemyInternalDatas> enemyInternalDatas;

    public List<EnemyData.EnemyInternalDatas> EnemyInternalDatas
    {
        get { return enemyInternalDatas; }
        set { enemyInternalDatas = value; }
    }

    private bool isAppAwake = false;

    public bool IsAppAwake
    {
        get { return isAppAwake; }
        set { isAppAwake = value; }
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
