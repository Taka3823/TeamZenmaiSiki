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

    [SerializeField]
    PlayerAttackController playerAttackController;

    BattleCalculation battleCalculation = new BattleCalculation();
    GameObject attackCircle;               //InstantiateしたAttackCircle。
    GameObject targetObject;               //現在のターゲットオブジェクト。
    bool circleColliderEnable;             //CircleColliderが有効化されているか。
    bool isHit;                            //CircleColliderに敵がヒットしたか。

    //*******仮置き********
    int[] enemyHP = { 100, 100, 100 };
    int[] enemyDEF = { 1, 2, 3 };
    int playerSTR = 5;
    //********************

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
            int currentIndex = EnemyManager.Instance.GetCurrentTargetIndex();
            int damage = battleCalculation.CalculateDamage(playerSTR, enemyDEF[currentIndex]);
            Debug.Log("beforeEnemyHP = " + enemyHP[currentIndex].ToString());
            enemyHP[currentIndex] -= damage;

            //TODO:HPが0だったら。
            if (enemyHP[currentIndex] <= 0)
            {
                EnemyDestroy();
            }
            Debug.Log("attackDamage" + damage.ToString());
            Debug.Log("afterEnemyHP = " + enemyHP[currentIndex].ToString());
            isHit = false;
        }
    }

    private void EnemyDestroy()
    {
        Destroy(targetObject);
        EnemyManager.Instance.EnemyPosErase();
        playerAttackController.DecreaseCurrentTargetIndex();
        TurnManager.Instance.ButtonManagement();
    }

    public void SetCircleColliderEnable(bool _cond) { circleColliderEnable = _cond; }
    public void SetAttackCircleObject(GameObject _refObj) { attackCircle = _refObj; }
    public void SetTargetObject(GameObject _refObj) { targetObject = _refObj; }
    public void SetIsHit(bool _cond) { isHit = _cond; }
}
