using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttackController : MonoBehaviour {
    [SerializeField, Tooltip("AttackCircle.prefabをアタッチ")]
    GameObject prefab;

    private GameObject attackCircle;
    private List<Vector3> enemiesPos;
    private int currentTargetEnemyIndex;
    private Vector3 currentTargetPosition;
    private int oldTargetIndex;
    private bool eraseFlag;
    private bool progressPhaseFlag;

    void Start()
    {
        enemiesPos = new List<Vector3>();
        enemiesPos = TurnManager.Instance.GetEnemiesPos();
        currentTargetEnemyIndex = 0;
        currentTargetPosition = new Vector3();
        eraseFlag = false;
        progressPhaseFlag = false;
    }

    void LateUpdate()
    {
        ListEraser();
        ProgressTargetIndex();

        if (currentTargetEnemyIndex > enemiesPos.Count-1)
        {
            currentTargetEnemyIndex = 0;
            if (TurnManager.Instance.GetFunctionNumber() == (int)TurnManager.FunctionID.PLAYER_ATTACK)
            {
                TurnManager.Instance.ProgressFunction();
            }
        }
    }

    public void GenerateAttackCircle()
    {
        if (attackCircle == null)
        {
            //Debug.Log(currentTargetEnemyIndex);
            //現在ターゲットの敵にサークル描画
            attackCircle = (GameObject)Instantiate(prefab,
                            enemiesPos[currentTargetEnemyIndex],
                            Quaternion.identity);
        }
    }

    void ListEraser()
    {
        eraseFlag = ObjToPhaseManager.Instance.GetEraseFlag();
        if (eraseFlag)
        {
            currentTargetPosition = ObjToPhaseManager.Instance.GetPosition();

            //Debug.Log(currentTargetPosition);

            for (int i = 0; i < enemiesPos.Count; i++)
            {
                if (enemiesPos[i].x == currentTargetPosition.x)
                {
                    if (enemiesPos[i].y == currentTargetPosition.y)
                    {
                        enemiesPos.RemoveAt(currentTargetEnemyIndex);
                        currentTargetEnemyIndex--;
                    }
                }
            }
            ObjToPhaseManager.Instance.SetEraseFlag(false);
        }
    }

    public void ProgressTargetIndex()
    {
        progressPhaseFlag = ObjToPhaseManager.Instance.GetProgressPhaseFlag();
        if (progressPhaseFlag)
        {
            currentTargetEnemyIndex++;
            if(currentTargetEnemyIndex > enemiesPos.Count - 1)
            {
                TurnManager.Instance.ProgressFunction();
                currentTargetEnemyIndex = 0;
            }
            if (currentTargetEnemyIndex < 0)
            {
                currentTargetEnemyIndex = 0;
            }

            ObjToPhaseManager.Instance.SetProgressPhaseFlag(false);
        }
    }
}
