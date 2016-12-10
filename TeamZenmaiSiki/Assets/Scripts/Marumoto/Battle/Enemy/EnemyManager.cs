using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// エネミーオブジェクトのリスト。
    /// </summary>
    public List<GameObject> Enemies { get { return enemies; } }
    private List<GameObject> enemies;

    /// <summary>
    /// エネミーの表示座標リスト。
    /// </summary>
    public List<Vector3> EnemiesPos { get { return enemiesPos; } }
    private List<Vector3> enemiesPos;

    /// <summary>
    /// 現在のターゲットインデックス。
    /// </summary>
    public int CurrentTargetIndex { set { currentTargetIndex = value; } }
    private int currentTargetIndex;

    /// <summary>
    /// エネミーの人数。
    /// </summary>
    public int EnemyElems { get { return enemyElems; } }
    private int enemyElems;

    private float angle;
    private Vector3 baseScale;

    void Awake()
    {
        if (instance == null) { instance = this; }
        enemies = new List<GameObject>();
        enemiesPos = new List<Vector3>();
        baseScale = new Vector3(1, 1, 1);
    }

    void Start()
    {
        SetupEnemy();
        currentTargetIndex = 0;
    }

    /// <summary>
    /// エネミーの情報を検索し情報を格納する。Awake()で一度だけ呼ぶ。
    /// </summary>
    private void SetupEnemy()
    {
        enemiesPos = BattleManager.Instance.getPos();
        enemies = BattleManager.Instance.getEnemyObject();
        enemyElems = enemies.Count;
    }

    /// <summary>
    /// エネミーの死亡と同時に、死亡したエネミーのデータのみを破棄。
    /// </summary>
    public void EnemyPosErase()
    {
        enemies.RemoveAt(currentTargetIndex);
        enemiesPos.RemoveAt(currentTargetIndex);
        enemyElems = enemiesPos.Count;
    }

    /// <summary>
    /// まだアニメーションがない為自作した敵の攻撃を可視化するための関数。
    /// </summary>
    /// <param name="_index">攻撃させたいエネミーのIndex</param>
    public void AttackMotion(int _index)
    {
        float value = Mathf.PI / 1.0f;
        angle += value * Time.deltaTime;
        enemies[_index].transform.localScale 
            = new Vector3(baseScale.x + 0.5f * Mathf.Sin(angle), 
                          baseScale.y + 0.5f * Mathf.Sin(angle), 
                          1);
        if (angle > Mathf.PI) angle = 0;
    }
}