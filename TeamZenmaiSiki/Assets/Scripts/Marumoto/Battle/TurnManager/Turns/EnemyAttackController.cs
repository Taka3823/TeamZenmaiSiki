using UnityEngine;
using System.Collections;

public class EnemyAttackController : MonoBehaviour {
    [SerializeField]
    PlayerStatus playerStatus;

    private int currentActIndex;
    private bool isAttacking;

    void Start()
    {
        currentActIndex = 0;
        isAttacking = false;
    }

    /// <summary>
    /// エネミーの攻撃ルーチン。
    /// </summary>
    public void EnemyAttacking()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackAction());
        }
        else
        {
            EnemyManager.Instance.AttackMotion(currentActIndex);
        }
    }

    /// <summary>
    /// 攻撃のアクション。
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackAction()
    {
        isAttacking = true;
        playerStatus.IncreaseDamage(5);
        yield return new WaitForSeconds(1.0f);

        isAttacking = false;
        ProgressActIndex();
        ChangePhaseSequence();
    }

    /// <summary>
    /// LibraPhaseへの移行。
    /// </summary>
    private void ChangePhaseSequence()
    {
        if (currentActIndex >= EnemyManager.Instance.GetEnemyElems())
        {
            currentActIndex = 0;
            TurnManager.Instance.ProgressFunction();
        }
    }

    private void ProgressActIndex() { currentActIndex ++; }
}
