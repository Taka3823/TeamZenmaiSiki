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
    /// ターゲットを破壊し、その情報を消す。
    /// </summary>
    private void HitSequence()
    {
        if (isHit)
        {
            Destroy(targetObject);
            EnemyManager.Instance.EnemyErase();
            playerAttackController.DecreaseCurrentTargetIndex();
            TurnManager.Instance.ButtonManagement();
            isHit = false;
        }
    }

    public void SetCircleColliderEnable(bool _cond) { circleColliderEnable = _cond; }
    public void SetAttackCircleObject(GameObject _refObj) { attackCircle = _refObj; }
    public void SetTargetObject(GameObject _refObj) { targetObject = _refObj; }
    public void SetIsHit(bool _cond) { isHit = _cond; }
}
