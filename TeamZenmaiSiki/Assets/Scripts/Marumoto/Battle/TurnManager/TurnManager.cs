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
}
