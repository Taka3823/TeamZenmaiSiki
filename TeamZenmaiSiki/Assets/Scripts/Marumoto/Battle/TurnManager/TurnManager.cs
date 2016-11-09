using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
    private static TurnManager instance;

    public static TurnManager Instance
    {
        get { return instance; }
    }

    //ターンのフェイズごとに名前をつける。
    enum FunctionID
    {
        LIBRA = 0,
        PLAYER_ATTACK,
        ENEMY_ATTACK
    }

    //戦闘画面中に表示されるボタンのID
    enum ButtonID
    {
        ATTACK = 0,
        AVOID
    }

    public enum EnemyElements
    {
        NOTHING = 0,
        LAST
    }

    [SerializeField]
    ButtonManager[] buttonManager;          //戦う&逃がすボタンの表示コントロールスクリプトの配列

    //この三つはターンのフェーズごとのスクリプト
    [SerializeField]
    LibraController libraController;
    [SerializeField]
    PlayerAttackController playerAttackController;
    [SerializeField]
    EnemyAttackController enemyAttackController;

    [SerializeField]
    bool enemyLast;                         //エネミーが最後の１体かどうか

    private delegate void Functions();      //関数のデリゲート型
    private List<Functions> turnFunctions;  //ターンのフェーズごとに関数に格納
    private int functionNumber;             //現在の関数のID
    private int oldID;                      //1フレーム前のFunctionID

    void Awake()
    {
        if (instance == null) { instance = this; }
        SetupTurnFunctions();
    }
	
	void LateUpdate ()
    {
        if (EnemyNothing())
        {
            ReturnToSearch();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            functionNumber++;
        }

        if (functionNumber == turnFunctions.Count) functionNumber = 0;

        if (oldID != functionNumber)
        {
            ButtonManagement();
        }

        turnFunctions[functionNumber]();
        oldID = functionNumber;
	}

    /// <summary>
    /// ターンの初めに情報を確認できるフェーズ。
    /// </summary>
    void Libra()
    {
        //TODO:長押しで情報表示
        //Debug.Log("Libra");
    }

    /// <summary>
    /// プレイヤーが攻撃するフェーズ。
    /// </summary>
    void PlayerAttack()
    {
        playerAttackController.GenerateAttackCircle();
    }

    /// <summary>
    /// エネミーが攻撃するフェーズ。
    /// </summary>
    void EnemyAttack()
    {
        Debug.Log("EnemyAttack");
    }

    /// <summary>
    /// フェーズごとにボタンの状態を管理する。
    /// </summary>
    void ButtonManagement()
    {
        //ターン先頭（Libra関数）での処理
        if (functionNumber == (int)FunctionID.LIBRA)
        {
            if (!EnemyLast())
            {
                buttonManager[(int)ButtonID.AVOID].SetInteractable(false);
                buttonManager[(int)ButtonID.ATTACK].SetInteractable(true);
            }
            else
            {
                buttonManager[(int)ButtonID.AVOID].SetInteractable(true);
                buttonManager[(int)ButtonID.ATTACK].SetInteractable(true);
            }
        }

        //プレイヤーのアタックフェーズ時処理
        if (functionNumber == (int)FunctionID.PLAYER_ATTACK)
        {
            buttonManager[(int)ButtonID.AVOID].SetInteractable(false);
            buttonManager[(int)ButtonID.ATTACK].SetInteractable(false);
        }

        //エネミーのアタックフェーズ時処理
        if (functionNumber == (int)FunctionID.ENEMY_ATTACK)
        {
            buttonManager[(int)ButtonID.AVOID].SetInteractable(false);
            buttonManager[(int)ButtonID.ATTACK].SetInteractable(false);
        }
    }

    /// <summary>
    /// デリゲート型の変数にターンのフェーズごとに分けた関数を登録。
    /// </summary>
    private void SetupTurnFunctions()
    {
        functionNumber = 0;
        turnFunctions = new List<Functions>();
        turnFunctions.Add(Libra);
        turnFunctions.Add(PlayerAttack);
        turnFunctions.Add(EnemyAttack);
    }

    private bool EnemyLast()
    {
        if (EnemyManager.Instance.GetEnemyElems() == (int)EnemyElements.LAST) return true;
        return false;
    }

    private bool EnemyNothing()
    {
        if (EnemyManager.Instance.GetEnemyElems() == (int)EnemyElements.NOTHING) return true;
        return false;
    }

    public void ProgressFunction() { functionNumber++; }
    public int GetFunctionNumber() { return functionNumber; }

    /// <summary>
    /// Searchシーンをロードする。
    /// </summary>
    public void ReturnToSearch()
    {
        SceneManager.LoadScene("Search");
    }
}
