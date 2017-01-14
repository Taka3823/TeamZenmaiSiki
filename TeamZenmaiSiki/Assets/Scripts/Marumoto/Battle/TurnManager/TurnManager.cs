using UnityEngine;
using UnityEngine.SceneManagement;
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

    private delegate void Functions();      //関数のデリゲート型
    private List<Functions> turnFunctions;  //ターンのフェーズごとに関数に格納
    private int functionNumber;             //現在の関数のID
    private int oldID;                      //1フレーム前のFunctionID
	private bool nothingCheck = false;

    void Awake()
    {
        if (instance == null) { instance = this; }
        SetupTurnFunctions();
    }
	
    void Start()
    {
        ButtonManagement();
    }

	void LateUpdate ()
    {
        if (EnemyNothing())
        {
			if (!nothingCheck)
			{
				nothingCheck = true;
				ReturnToSearch("walk_human.wav");
			}
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
        //特に処理はなし。
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
        enemyAttackController.EnemyAttacking();
    }

    /// <summary>
    /// ボタンの状態を変更。
    /// </summary>
    public void ButtonManagement()
    {
        if(functionNumber == (int)FunctionID.LIBRA)
        {
            buttonManager[(int)ButtonID.ATTACK].SetInteractable(true);
        }
        else
        {
            buttonManager[(int)ButtonID.ATTACK].SetInteractable(false);
        }

        if (EnemyLast())
        {
            CanAvoid();
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

    /// <summary>
    /// エネミーが残り1体であるかを判定。
    /// </summary>
    /// <returns>もし1体しかいないのであれば"true",それ以外は"false"</returns>
    private bool EnemyLast()
    {
        if (EnemyManager.Instance.EnemyElems == (int)EnemyElements.LAST) return true;
        return false;
    }

    /// <summary>
    /// エネミーが0体であるかを判定。
    /// </summary>
    /// <returns>もし0体なら"true",それ以外は"false"</returns>
    private bool EnemyNothing()
    {
        if (EnemyManager.Instance.EnemyElems == (int)EnemyElements.NOTHING) return true;
        return false;
    }

    /// <summary>
    /// 逃げるボタンを有効化。
    /// </summary>
    private void CanAvoid()
    {
        buttonManager[(int)ButtonID.AVOID].SetInteractable(true);
    }

    public void ProgressFunction() { functionNumber++; }
    public int GetFunctionNumber() { return functionNumber; }

    /// <summary>
    /// Searchシーンをロードする。
    /// </summary>
    public void ReturnToSearch(string _seName)
    {
		float _waitTime = 1.5f;
        DataManager.PlayerDatas _playerData = DataManager.Instance.PlayerData;
        _playerData.hp = enemyAttackController.PlayerHP;
        DataManager.Instance.PlayerData = _playerData;
		AudioManager.Instance.PlaySe(_seName);
		FadeManager.Instance.FadeInOut(_waitTime - 0.1f, 0.4f);
		Invoke("LoadSceneSearch", _waitTime);
    }

	private void LoadSceneSearch()
	{
		SceneManager.LoadScene("Search");
	}
}
