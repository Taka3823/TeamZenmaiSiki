using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// PlayerAttackのフェーズにおける「順序を保証したい関数」をこのクラスのUpdateで呼ぶ。
/// </summary>
public class PlayerAttackUpdateManager : MonoBehaviour {
    private static PlayerAttackUpdateManager instance;
    public static PlayerAttackUpdateManager Instance
    {
        get { return instance; }
    }

    enum PartsName
    {
        EMPTY = 0,
        BODY,
        CORE_1,
        CORE_2,
        CORE_3,
        CORE_FRAME_1
    }
    //FIXME:緊急措置
    [SerializeField]
    Text playerSTRText;

    [SerializeField]
    PlayerAttackController playerAttackController;

    BattleCalculation calculation = new BattleCalculation();
    EnemyCollision enemyCollision = new EnemyCollision();

    public Vector3 SecondaryCirclePos { get; set; }
    GameObject attackCircle;      //InstantiateしたAttackCircle。
    bool circleColliderEnable;    //CircleColliderが有効化されているか。

    private int playerSTR;

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        circleColliderEnable = false;
        SecondaryCirclePos = new Vector3();
        playerSTR = BattleManager.Instance.getBattlePlayerAtk();
        playerSTR = 10;
        playerSTRText.text = "ATK: " + playerSTR.ToString();
    }

	void LateUpdate ()
    {
        playerSTRText.text = "ATK: " + playerSTR.ToString();
        if (circleColliderEnable)
        {
            StartCoroutine(PlayerAttacking());
        }
	}

    /// <summary>
    /// プレイヤーの一連の攻撃のコルーチン関数
    /// </summary>
    /// <returns>呼ばれた直後に1フレーム処理を待つ</returns>
    private IEnumerator PlayerAttacking()
    {
        SetCircleColliderEnable(false);
        yield return null;

        HitSequence();
        if (EnemyDead())
        {
            EnemyDestroy();
        }
        playerAttackController.ProgressCurrentTargetIndex();
        Destroy(attackCircle);
    }

    /// <summary>
    /// 攻撃が当たっていたときの処理。
    /// </summary>
    public void HitSequence()
    {
        int hitIndex = enemyCollision.Collision(EnemyManager.Instance.Pos[EnemyManager.Instance.CurrentTargetIndex],
                                                SecondaryCirclePos,
                                                EnemyManager.Instance.Size[EnemyManager.Instance.CurrentTargetIndex]);

        if (hitIndex == (int)PartsName.EMPTY) return;
        else if (hitIndex == (int)PartsName.BODY)
        {
            int _enemyMainDEF = EnemyManager.Instance.MainDEF[EnemyManager.Instance.CurrentTargetIndex];
            EnemyManager.Instance.ToEnemyMainDamage(calculation.CalcDamage(playerSTR, _enemyMainDEF));
        }
        else if ((hitIndex == (int)PartsName.CORE_1) || (hitIndex == (int)PartsName.CORE_FRAME_1))
        {
            int _enemyCoreDEF = EnemyManager.Instance.CoreDEF[EnemyManager.Instance.CurrentTargetIndex];
            EnemyManager.Instance.ToEnemyCoreDamage(calculation.CalcDamage(playerSTR, _enemyCoreDEF));
        }

        //デバッグ用：ステータス表示。
        Debug.Log("MainHP  : "+EnemyManager.Instance.MainHP[EnemyManager.Instance.CurrentTargetIndex]);
        Debug.Log("CoreHP  : "+EnemyManager.Instance.CoreHP[EnemyManager.Instance.CurrentTargetIndex]);
        Debug.Log("CoreSTR : "+EnemyManager.Instance.CoreSTR[EnemyManager.Instance.CurrentTargetIndex]);
    }

    /// <summary>
    /// エネミーのデータを消去する処理
    /// </summary>
    private void EnemyDestroy()
    {
        Destroy(EnemyManager.Instance.Enemies[EnemyManager.Instance.CurrentTargetIndex]);
        EnemyManager.Instance.EnemyErase();
        playerAttackController.DecreaseCurrentTargetIndex();
        TurnManager.Instance.ButtonManagement();
    }

    /// <summary>
    /// エネミーの死亡判定
    /// </summary>
    /// <returns>死んでいればtrue</returns>
    private bool EnemyDead()
    {
        if (EnemyManager.Instance.MainHP[EnemyManager.Instance.CurrentTargetIndex] <= 0) return true;
        return false;
    }

    public void SetCircleColliderEnable(bool _cond) { circleColliderEnable = _cond; }
    public void SetAttackCircleObject(GameObject _refObj) { attackCircle = _refObj; }
}