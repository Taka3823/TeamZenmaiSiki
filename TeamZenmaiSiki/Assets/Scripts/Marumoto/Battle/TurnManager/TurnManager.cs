using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
    private static TurnManager instance;

    public static TurnManager Instance
    {
        get { return instance; }
    }

    //ターンのフェイズごとに名前をつける。
    public enum FunctionID
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

    private List<Vector3> enemies;          //エネミーたちの座標取得用 
    private int enemyElements;              //エネミーの人数

    private delegate void Functions();      //関数のデリゲート型
    private List<Functions> turnFunctions;  //ターンのフェーズごとに関数に格納
    private int functionNumber;             //現在の関数のID
    private int oldID;                      //1フレーム前のFunctionID

    void Awake()
    {
        if (instance == null) { instance = this; }
        SetupEnemies();
        SetupTurnFunctions();
    }
	
	void LateUpdate ()
    {
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

    //ターン最初の情報等見ておけるフェーズ
    void Libra()
    {
        //TODO:長押しで情報表示
        //Debug.Log("Libra");
    }

    //プレイヤーのアタックフェーズ
    void PlayerAttack()
    {
        //TODO:敵の数だけAttackCircle描画。攻撃のサイクルを実装。
        playerAttackController.GenerateAttackCircle();
        //TODO:AttckCircleが存在していなかった場合生成。
    }

    //エネミーのアタックフェーズ
    void EnemyAttack()
    {
        //Debug.Log("EnemyAttack");
    }

    //フェーズや状態によってボタンの状態や種類を変更
    void ButtonManagement()
    {
        //ターン先頭（Libra関数）での処理
        if (functionNumber == (int)FunctionID.LIBRA)
        {
            if (!enemyLast)
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

    private void SetupTurnFunctions()
    {
        functionNumber = 0;
        turnFunctions = new List<Functions>();
        turnFunctions.Add(Libra);
        turnFunctions.Add(PlayerAttack);
        turnFunctions.Add(EnemyAttack);
    }

    private void SetupEnemies()
    {
        enemies = new List<Vector3>();
        var refObj = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject temp = refObj[0];
        refObj[0] = refObj[1];
        refObj[1] = temp;

        for (int i = 0; i < refObj.Length; i++)
        {
            enemies.Add(refObj[i].transform.position);
            //Debug.Log(refObj[i].name);
        }
        
    }

    public List<Vector3> GetEnemiesPos() { return enemies; }
    public void ProgressFunction() { functionNumber++; }
    public int GetFunctionNumber() { return functionNumber; }
}
