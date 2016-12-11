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
    /// エネミーオブジェクトのリスト
    /// </summary>
    public List<GameObject> Enemies { get; private set; }

    /// <summary>
    /// エネミーの表示座標リスト
    /// </summary>
    public List<Vector3> Pos { get; private set; }

    /// <summary>
    /// エネミーの素のHP
    /// </summary>
    public List<int> MainHP { get; private set; }

    /// <summary>
    /// エネミーの素の攻撃力
    /// </summary>
    public List<int> MainSTR { get; private set; }

    /// <summary>
    /// エネミーの素の防御力
    /// </summary>
    public List<int> MainDEF { get; private set; }

    /// <summary>
    /// エネミーのコアのHP
    /// </summary>
    public List<int> CoreHP { get; private set; }

    /// <summary>
    /// エネミーのコアの攻撃力
    /// </summary>
    public List<int> CoreSTR { get; private set; }

    /// <summary>
    /// エネミーのコアの防御力
    /// </summary>
    public List<int> CoreDEF { get; private set; }

    /// <summary>
    /// エネミーのコアが壊れているか。
    /// 壊れている場合"true"
    /// </summary>
    public List<bool> CoreBroken { get; private set; }

    /// <summary>
    /// 現在のターゲットインデックス。
    /// </summary>
    public int CurrentTargetIndex { get; set; }

    /// <summary>
    /// エネミーの人数。
    /// </summary>
    public int EnemyElems { get; private set; }

    /// <summary>
    /// 当たり判定のインデックス
    /// </summary>
    public List<List<List<int>>> CollisionIndex { get; private set; }

    CollisionData collisionData;

    public List<string> CollisionPath { get; private set; }

    private float angle;
    private Vector3 baseScale;

    void Awake()
    {
        if (instance == null) { instance = this; }
        baseScale = new Vector3(1, 1, 1);
    }

    void Start()
    {
        SetupEnemy();
        CurrentTargetIndex = 0;
    }

    /// <summary>
    /// エネミーの情報を検索し情報を格納する。Awake()で一度だけ呼ぶ。
    /// </summary>
    private void SetupEnemy()
    {
        Pos = new List<Vector3>();
        Enemies = new List<GameObject>();
        CoreBroken = new List<bool>();
        CollisionPath = new List<string>();

        Pos = BattleManager.Instance.getPos();
        Enemies = BattleManager.Instance.getEnemyObject();
        MainHP = BattleManager.Instance.getBattleMainHp();
        MainSTR = BattleManager.Instance.getBattleMainPower();
        MainDEF = BattleManager.Instance.getBattleMainDefence();
        CoreHP = BattleManager.Instance.getBattleCoreHp();
        CoreSTR = BattleManager.Instance.getBattleCorePower();
        CoreDEF = BattleManager.Instance.getBattleMainDefence();
        EnemyElems = Enemies.Count;

        for (int i = 0; i < EnemyElems; i++)
        {
            string _collisionPath = Application.dataPath + "/CSVFiles/Battle/Collision/" + DataManager.Instance.EnemyInternalDatas[i].collisionPass;
            CoreBroken.Add(false);
            CollisionPath.Add(_collisionPath);
        }
        CollisionPath.Add(Application.dataPath + "/CSVFiles/Battle/Collision/WorkerA_def.txt");

        collisionData = new CollisionData();
        CollisionIndex = collisionData.CollisionIndex;

        //配列デバッグ用
        for (int i = 0; i < 30; i++)
        {
            string a = "";
            for (int j = 0; j < 30; j++)
            {
                a += CollisionIndex[0][i][j].ToString();
            }
            Debug.Log(a);
        }
    }

    /// <summary>
    /// エネミーの死亡と同時に、死亡したエネミーのデータのみを破棄。
    /// </summary>
    public void EnemyErase()
    {
        Enemies.RemoveAt(CurrentTargetIndex);
        Pos.RemoveAt(CurrentTargetIndex);
        MainHP.RemoveAt(CurrentTargetIndex);
        MainSTR.RemoveAt(CurrentTargetIndex);
        MainDEF.RemoveAt(CurrentTargetIndex);
        CoreHP.RemoveAt(CurrentTargetIndex);
        CoreSTR.RemoveAt(CurrentTargetIndex);
        CoreDEF.RemoveAt(CurrentTargetIndex);
        CoreBroken.RemoveAt(CurrentTargetIndex);
        EnemyElems = Enemies.Count;
    }

    /// <summary>
    /// まだアニメーションがない為自作した敵の攻撃を可視化するための関数。
    /// </summary>
    /// <param name="_index">攻撃させたいエネミーのIndex</param>
    public void AttackMotion(int _index)
    {
        float value = Mathf.PI / 1.0f;
        angle += value * Time.deltaTime;
        Enemies[_index].transform.localScale 
            = new Vector3(baseScale.x + 0.5f * Mathf.Sin(angle), 
                          baseScale.y + 0.5f * Mathf.Sin(angle), 
                          1);
        if (angle > Mathf.PI) angle = 0;
    }

    /// <summary>
    /// コアが破壊された時の処理
    /// </summary>
    /// <param name="_enemyIndex">何番目のエネミーか(index)</param>
    /// <param name="_coreIndex">何番目のコアか(index)</param>
    public void CoreBreaking(int _enemyIndex, int _coreIndex)
    {
        CoreBroken[_enemyIndex] = true;
        CoreSTR[_enemyIndex] = 0;
    }

    /// <summary>
    /// エネミーの素のHPに対するダメージ処理。
    /// </summary>
    /// <param name="_damageValue">エネミーに与えたいダメージ値</param>
    public void ToEnemyMainDamage(int _damageValue)
    {
        MainHP[CurrentTargetIndex] -= _damageValue;
        if (MainHP[CurrentTargetIndex] < 0)
        {
            MainHP[CurrentTargetIndex] = 0;
        }
    }

    /// <summary>
    /// エネミーのコアHPに対するダメージ処理
    /// </summary>
    /// <param name="_damageValue">コアへのダメージ量</param>
    public void ToEnemyCoreDamage(int _damageValue)
    {
        
    }
}