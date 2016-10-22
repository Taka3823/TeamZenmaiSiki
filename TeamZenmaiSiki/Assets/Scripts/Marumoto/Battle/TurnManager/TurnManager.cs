using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
    enum FunctionID
    {
        LIBRA = 0,
        PLAYER_ATTACK,
        ENEMY_ATTACK
    }

    enum ButtonID
    {
        ATTACK,
        AVOID
    }

    [SerializeField]
    ButtonManager[] buttonManager;          //戦う&逃がすボタンの表示コントロールスクリプトの配列

    [SerializeField]
    bool enemyLast;                         //エネミーが最後の１体かどうか

    private delegate void Functions();      //関数のデリゲート型
    private List<Functions> turnFunctions;  //ターンのフェーズごとに関数に格納
    private int functionNumber;             //現在の関数のID
    private int functionElements;           //格納する関数の個数

    // Use this for initialization
    void Start () {
        functionNumber = 0;
        functionElements = 3;

        turnFunctions = new List<Functions>();
        turnFunctions.Add(Libra);
        turnFunctions.Add(PlayerAttack);
        turnFunctions.Add(EnemyAttack);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            functionNumber++;
            if (functionNumber >= turnFunctions.Count) functionNumber = 0;
        }

        ButtonManagement();
        turnFunctions[functionNumber]();
	}

    //ターン最初の情報等見ておけるフェーズ
    void Libra()
    {
        Debug.Log("Libra()");
    }

    //プレイヤーのアタックフェーズ
    void PlayerAttack()
    {
        Debug.Log("PlayerAttack()");
    }

    //エネミーのアタックフェーズ
    void EnemyAttack()
    {
        Debug.Log("EnemyAttack()");
    }

    //フェーズや状態によってボタンの状態や種類を変更
    void ButtonManagement()
    {
        //ターン先頭（Libra関数）での処理
        if (functionNumber == (int)FunctionID.LIBRA)
        {
            if (!enemyLast)
            {
                buttonManager[(int)ButtonID.ATTACK].EnableButton();
                buttonManager[(int)ButtonID.AVOID].DisableButton();
            }
            buttonManager[(int)ButtonID.AVOID].SetInteractable(true);
            buttonManager[(int)ButtonID.ATTACK].SetInteractable(true);
        }

        //プレイヤーのアタックフェーズ時処理
        if (functionNumber == (int)FunctionID.PLAYER_ATTACK)
        {
            //敵キャラが独りじゃないとき
            if (!enemyLast)
            {
                buttonManager[(int)ButtonID.ATTACK].SetInteractable(false);
                buttonManager[(int)ButtonID.AVOID].DisableButton();
            }
            else
            {
                buttonManager[(int)ButtonID.AVOID].SetInteractable(false);
                buttonManager[(int)ButtonID.ATTACK].DisableButton();
                buttonManager[(int)ButtonID.AVOID].EnableButton();
            }
        }

        //エネミーのアタックフェーズ時処理
        if (functionNumber == (int)FunctionID.ENEMY_ATTACK)
        {
            buttonManager[(int)ButtonID.AVOID].SetInteractable(false);
        }
    }
}
