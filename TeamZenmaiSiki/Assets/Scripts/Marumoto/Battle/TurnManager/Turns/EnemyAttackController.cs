using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyAttackController : MonoBehaviour {
    private int currentActIndex;
    private bool isAttacking;
    public int PlayerHP { get; private set; }

    [SerializeField]
    Text playerHP;

    void Start()
    {
        PlayerHP = BattleManager.Instance.GetBattlePlayerHp;
        PlayerHP = 150;
        currentActIndex = 0;
        isAttacking = false;
    }

    void LateUpdate()
    {
        playerHP.text = "HP: " + PlayerHP + "/150";
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
        Debug.Log("EnemyElems : " + EnemyManager.Instance.EnemyElems);
        Debug.Log("CAIndex : " + currentActIndex);
        Debug.Log("MainSTR.Num:" + EnemyManager.Instance.MainSTR.Count);
        Debug.Log("CoreSTR.Num:" + EnemyManager.Instance.CoreSTR.Count);

                
        isAttacking = true;
        
        yield return new WaitForSeconds(0.5f);

        int _enemySTR = EnemyManager.Instance.MainSTR[currentActIndex] + EnemyManager.Instance.CoreSTR[currentActIndex];
        ToPlayerDamage(_enemySTR);
        isAttacking = false;
        ProgressActIndex();
        ChangePhaseSequence();
    }

    /// <summary>
    /// LibraPhaseへの移行。
    /// </summary>
    private void ChangePhaseSequence()
    {
        if (currentActIndex >= EnemyManager.Instance.EnemyElems)
        {
            currentActIndex = 0;
            TurnManager.Instance.ProgressFunction();
        }
    }

    private void ToPlayerDamage(int _damage)
    {
        PlayerHP -= _damage;
        if (PlayerHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void ProgressActIndex() { currentActIndex ++; }
}
