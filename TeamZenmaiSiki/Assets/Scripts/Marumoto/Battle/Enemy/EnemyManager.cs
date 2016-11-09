using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get { return instance; }
    }

    private List<Vector3> enemiesPos;
    private int currentTargetIndex;
    private int enemyElems;

    void Awake()
    {
        if (instance == null) { instance = this; }
        enemiesPos = new List<Vector3>();
        SetupEnemy();
    }

    void Start()
    {
        currentTargetIndex = 0;
    }

    private void SetupEnemy()
    {
        var refObj = GameObject.FindGameObjectsWithTag("Enemy");
        enemyElems = refObj.Length;

        GameObject temp = refObj[0];
        refObj[0] = refObj[1];
        refObj[1] = temp;

        for (int i = 0; i < refObj.Length; i++)
        {
            enemiesPos.Add(refObj[i].transform.position);
        }
    }

    public void EnemyPosErase()
    {
        enemiesPos.RemoveAt(currentTargetIndex);
        enemyElems = enemiesPos.Count;
    }
    public void SetCurrentTargetPos(int _index) { currentTargetIndex = _index; }
    public List<Vector3> GetEnemiesPos() { return enemiesPos; }
    public int GetEnemyElems() { return enemyElems; }
}