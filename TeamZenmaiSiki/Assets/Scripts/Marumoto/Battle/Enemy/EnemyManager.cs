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
    public List<GameObject> Enemies { get; set; }

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
    /// エネミーの画像サイズ
    /// </summary>
    public List<Vector3> Size { get; private set; }

    /// <summary>
    /// 当たり判定のインデックス
    /// </summary>
    public List<List<List<int>>> CollisionIndex { get; private set; }

    /// <summary>
    /// 死んでるかどうか。
    /// </summary>
    public List<bool> Dead { get; set; }

    public int DeadNum { get; set; }

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
        Size = new List<Vector3>();
        Dead = new List<bool>();
        DeadNum = 0;

        Pos = BattleManager.Instance.getPos();
        Enemies = BattleManager.Instance.getEnemyObject();
        MainHP = BattleManager.Instance.getBattleMainHp();
        MainSTR = BattleManager.Instance.getBattleMainPower();
        MainDEF = BattleManager.Instance.getBattleMainDefence();
        CoreHP = BattleManager.Instance.getBattleCoreHp();
        CoreSTR = BattleManager.Instance.getBattleCorePower();
        CoreDEF = BattleManager.Instance.getBattleCoreDefence();
        EnemyElems = Enemies.Count;

        for (int i = 0; i < EnemyElems; i++)
        {
#if UNITY_STANDALONE
			string _collisionPath = "file://" + Application.dataPath + "/CSVFiles/Battle/Collision/" + DataManager.Instance.EnemyInternalDatas[i].collisionPass;
#elif UNITY_ANDROID
			string _collisionPath = "jar:file://" + Application.dataPath + "/CSVFiles/Battle/Collision/" + DataManager.Instance.EnemyInternalDatas[i].collisionPass;
#endif
			CollisionPath.Add(_collisionPath);
            CoreBroken.Add(false);
            Size.Add(new Vector3(Enemies[i].GetComponent<SpriteRenderer>().bounds.size.x, Enemies[i].GetComponent<SpriteRenderer>().bounds.size.y));
            Dead.Add(false);
        }

        collisionData = new CollisionData();
        CollisionIndex = collisionData.CollisionIndex;

        //デバッグ用:配列デバッグ用
        //for (int k = 0; k < EnemyElems; k++)
        //{
        //    for (int i = 0; i < 30; i++)
        //    {
        //        string a = "";
        //        for (int j = 0; j < 30; j++)
        //        {
        //            a += CollisionIndex[k][i][j].ToString();
        //        }
        //        Debug.Log(a);
        //    }
        //    Debug.Log("===================================================");
        //    Debug.Log("===================================================");
        //    Debug.Log("===================================================");
        //}
    }

    /// <summary>
    /// エネミーの死亡と同時に、死亡したエネミーのデータのみを破棄。
    /// </summary>
    public void EnemyErase()
    {
        DataManager.Instance.KillNum++;
        DataManager.Instance.KillNames.Add(BattleManager.Instance.getKillName(CurrentTargetIndex));
        BattleManager.Instance.removeEnemyData(CurrentTargetIndex);
        Enemies.RemoveAt(CurrentTargetIndex);
        Pos.RemoveAt(CurrentTargetIndex);
        MainHP.RemoveAt(CurrentTargetIndex);
        MainSTR.RemoveAt(CurrentTargetIndex);
        MainDEF.RemoveAt(CurrentTargetIndex);
        CoreHP.RemoveAt(CurrentTargetIndex);
        CoreSTR.RemoveAt(CurrentTargetIndex);
        CoreDEF.RemoveAt(CurrentTargetIndex);
        CoreBroken.RemoveAt(CurrentTargetIndex);
        Size.RemoveAt(CurrentTargetIndex);
        CollisionIndex.RemoveAt(CurrentTargetIndex);
        EnemyElems = Pos.Count;
    }

    /// <summary>
    /// まだアニメーションがない為自作した敵の攻撃を可視化するための関数。
    /// </summary>
    /// <param name="_index">攻撃させたいエネミーのIndex</param>
    public void AttackMotion(int _index)
    {
        float diffSize = 0.25f;
        float fadeTime = 0.5f;
        float value = Mathf.PI / fadeTime;
        angle += value * Time.deltaTime;

        if (angle > Mathf.PI) angle = 0;

        Enemies[_index].transform.localScale 
            = new Vector3(baseScale.x + diffSize * Mathf.Sin(angle), 
                          baseScale.y + diffSize * Mathf.Sin(angle), 
                          1);
        
    }

    /// <summary>
    /// コアが破壊された時の処理
    /// </summary>
    /// <param name="_enemyIndex">何番目のエネミーか(index)</param>
    /// <param name="_coreIndex">何番目のコアか(index)</param>
    public void CoreBreaking(int _enemyIndex)
    {
        DataManager.Instance.BrakedCoreCount++;
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
        if (CoreBroken[CurrentTargetIndex]) return;

        CoreHP[CurrentTargetIndex] -= _damageValue;
        if (CoreHP[CurrentTargetIndex] <= 0)
        {
            CoreHP[CurrentTargetIndex] = 0;
            CoreBreaking(CurrentTargetIndex);
        }
    }

    /// <summary>
    /// 全ての敵が死んでいるかどうか。
    /// </summary>
    /// <returns>全て死んでいれば"true"、それ以外"false"</returns>
    private bool AllDead()
    {
        if (EnemyElems == 0) return true;
        else                 return false;
    }
}