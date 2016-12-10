using UnityEngine;
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

    //********************************************
    //仮置き
    //********************************************
    int charaSTR = 3;


    [SerializeField]
    PlayerAttackController playerAttackController;

    BattleCalculation calculation = new BattleCalculation();

    GameObject attackCircle;      //InstantiateしたAttackCircle。
    GameObject targetObject;      //現在のターゲットオブジェクト。
    bool circleColliderEnable;    //CircleColliderが有効化されているか。
    bool isHit;                   //CircleColliderに敵がヒットしたか。

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        circleColliderEnable = false;
        isHit = false;

    }

	void LateUpdate ()
    {
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
        playerAttackController.ProgressCurrentTargetIndex();
        Destroy(attackCircle);
    }

    /// <summary>
    /// 攻撃が当たっていたときの処理。
    /// </summary>
    private void HitSequence()
    {
        if (isHit)
        {
            int _enemyDEF = EnemyManager.Instance.MainDEF[EnemyManager.Instance.CurrentTargetIndex];
            EnemyManager.Instance.ToEnemyMainDamage(calculation.CalcDamage(charaSTR, _enemyDEF));
            if (EnemyDead())
            {
                EnemyDestroy();
            }
            isHit = false;
        }
    }

    /// <summary>
    /// エネミーのデータを消去する処理
    /// </summary>
    private void EnemyDestroy()
    {
        Destroy(targetObject);
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
    public void SetTargetObject(GameObject _refObj) { targetObject = _refObj; }
    public void SetIsHit(bool _cond) { isHit = _cond; }
}
