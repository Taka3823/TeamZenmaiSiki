using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get { return instance; }
    }

    private List<GameObject> enemies;
    private List<Vector3> enemiesPos;
    private int currentTargetIndex;
    private int enemyElems;
    private float angle;
    private Vector3 baseScale;

    void Awake()
    {
        if (instance == null) { instance = this; }
        enemies = new List<GameObject>();
        enemiesPos = new List<Vector3>();
        baseScale = new Vector3(2, 2, 1);
        SetupEnemy();
    }

    void Start()
    {
        currentTargetIndex = 0;
    }

    /// <summary>
    /// エネミーの情報を検索し情報を格納する。Awake()で一度だけ呼ぶ。
    /// </summary>
    private void SetupEnemy()
    {
        var refObj = GameObject.FindGameObjectsWithTag("Enemy");
        enemyElems = refObj.Length;


        for (int i = 0; i < refObj.Length; i++)
        {
            enemies.Add(refObj[i]);
            enemiesPos.Add(refObj[i].transform.position);
        }
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
            = new Vector3(baseScale.x + 1.0f * Mathf.Sin(angle), 
                          baseScale.y + 1.0f * Mathf.Sin(angle), 
                          1);
        if (angle > Mathf.PI) angle = 0;
    }

    public void SetCurrentTargetPos(int _index) { currentTargetIndex = _index; }
    public List<GameObject> GetEnemies() { return enemies; }
    public List<Vector3> GetEnemiesPos() { return enemiesPos; }
    public int GetEnemyElems() { return enemyElems; }
}