using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField, Tooltip("AttackCircle.prefabをアタッチ")]
    GameObject prefab;

    private GameObject attackCircle;
    private List<Vector3> enemiesPos;
    private int currentTargetEnemyIndex;
    private int oldTargetIndex;

    void Start()
    {
        enemiesPos = new List<Vector3>();
        enemiesPos = EnemyManager.Instance.GetEnemiesPos();
        currentTargetEnemyIndex = 0;
    }

    /// <summary>
    /// アタックサークルが存在しないときに生成する。
    /// </summary>
    public void GenerateAttackCircle()
    {
        if (attackCircle == null)
        {
            //Debug.Log(currentTargetEnemyIndex);
            //現在ターゲットの敵にサークル描画

            attackCircle = (GameObject)Instantiate(prefab,
                            enemiesPos[currentTargetEnemyIndex],
                            Quaternion.identity);
            Debug.Log("Clear");
            PlayerAttackUpdateManager.Instance.SetAttackCircleObject(attackCircle);
            EnemyManager.Instance.SetCurrentTargetPos(currentTargetEnemyIndex);
        }
    }

    /// <summary>
    /// ターゲットとなるエネミーのIndexを進める。
    /// </summary>
    public void ProgressCurrentTargetIndex()
    {
        currentTargetEnemyIndex++;
        TargetIndexClamp();
    }

    /// <summary>
    /// currentTargetEnemyIndexが範囲を超えないように調節。
    /// </summary>
    private void TargetIndexClamp()
    {
        if (currentTargetEnemyIndex > enemiesPos.Count - 1)
        {
            currentTargetEnemyIndex = 0;
            TurnManager.Instance.ProgressFunction();
        }
        if (currentTargetEnemyIndex < 0)
        {
            currentTargetEnemyIndex = 0;
        }
    }

    public void DecreaseCurrentTargetIndex() { currentTargetEnemyIndex--; }
}
