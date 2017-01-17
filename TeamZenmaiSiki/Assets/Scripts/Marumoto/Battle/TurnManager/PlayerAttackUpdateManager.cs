using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PlayerAttackのフェーズにおける「順序を保証したい関数」をこのクラスのUpdateで呼ぶ。
/// </summary>
public class PlayerAttackUpdateManager : MonoBehaviour {
    private static PlayerAttackUpdateManager instance;
    public static PlayerAttackUpdateManager Instance
    {
        get { return instance; }
    }

    enum PartsName
    {
        EMPTY = 0,
        BODY,
        CORE_1,
        CORE_2,
        CORE_3,
        CORE_FRAME_1
    }

	enum HitEffectName
	{
		BLOOD_MARK = 0,
		BULLET_SPARK,
		BLOOD_SPLASH
	}

    //FIXME:緊急措置
    [SerializeField]
    Text playerSTRText;

    [SerializeField]
    PlayerAttackController playerAttackController;

	[SerializeField, Tooltip("血痕、跳弾火花、血しぶき、の順番に登録")]
	List<GameObject> hitEffect;

	[SerializeField]
	GameObject gameoverKilledHuman;

	[SerializeField]
	Transform gameoverCanvas;

    BattleCalculation calculation = new BattleCalculation();
    EnemyCollision enemyCollision = new EnemyCollision();

    public Vector3 SecondaryCirclePos { get; set; }
    GameObject attackCircle;      //InstantiateしたAttackCircle。
    bool circleColliderEnable;    //CircleColliderが有効化されているか。

    private int playerSTR;

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        circleColliderEnable = false;
        SecondaryCirclePos = new Vector3();
        playerSTR = BattleManager.Instance.GetBattlePlayerAtk();
        playerSTR = 10;
        playerSTRText.text = "ATK: " + playerSTR.ToString();
    }

	void LateUpdate ()
    {
        playerSTRText.text = "ATK: " + playerSTR.ToString();
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
        if (EnemyDead())
        {
			if (EnemyManager.Instance.CoreNum[EnemyManager.Instance.CurrentTargetIndex] == 0)
			{
				Gameover();
			}
			EnemyManager.Instance.RegisterKillData();
			EnemyDestroy();
        }
        playerAttackController.ProgressCurrentTargetIndex();

		yield return new WaitForSeconds(0.5f);
        Destroy(attackCircle);
    }

    /// <summary>
    /// 攻撃が当たっていたときの処理。
    /// </summary>
    public void HitSequence()
    {
		float delayTime = 0.4f;
		int hitIndex = enemyCollision.Collision(EnemyManager.Instance.Pos[EnemyManager.Instance.CurrentTargetIndex],
                                                SecondaryCirclePos,
                                                EnemyManager.Instance.Size[EnemyManager.Instance.CurrentTargetIndex]);

		if (hitIndex == (int)PartsName.EMPTY)
		{
			AudioManager.Instance.PlaySe("kazewokiru.wav", delayTime);
			return;
		}
		else if (hitIndex == (int)PartsName.BODY)
		{
			HitBody(delayTime);
		}
		else if ((hitIndex == (int)PartsName.CORE_1) || (hitIndex == (int)PartsName.CORE_FRAME_1))
		{
			int _enemyCoreDEF = EnemyManager.Instance.CoreDEF[EnemyManager.Instance.CurrentTargetIndex];
			EnemyManager.Instance.ToEnemyCoreDamage(calculation.CalcDamage(playerSTR, _enemyCoreDEF));
			StartCoroutine(CreateEffect(hitEffect[(int)HitEffectName.BULLET_SPARK],
										SecondaryCirclePos,
										0.0f));
			if (hitIndex == (int)PartsName.CORE_1)
			{
				AudioManager.Instance.PlaySe("coredam.wav", delayTime);
			}
			if (hitIndex == (int)PartsName.CORE_FRAME_1)
			{
				AudioManager.Instance.PlaySe("cyoudan.wav", delayTime);
			}
		}
    }

    /// <summary>
    /// エネミーのデータを消去する処理
    /// </summary>
    private void EnemyDestroy()
    {
		StartCoroutine(EnemyDyingMotion(EnemyManager.Instance.Enemies[EnemyManager.Instance.CurrentTargetIndex]));
        EnemyManager.Instance.EnemyErase();
        playerAttackController.DecreaseCurrentTargetIndex();
        TurnManager.Instance.ButtonManagement();
    }

	private IEnumerator EnemyDyingMotion(GameObject _enemyObj)
	{
		yield return new WaitForSeconds(1.0f);
		SpriteRenderer _sprite = _enemyObj.GetComponent<SpriteRenderer>();
		float _angle = 0.0f;
		float _angleSpeed = 0.005f;
		while (true)
		{
			if(_angle >= 255.0f)
			{
				Destroy(_enemyObj);
				yield break;
			}

			_sprite.color -= new Color(0.0f, _angle, _angle, _angle);
			yield return null;
			_angle += _angleSpeed;
		}
	}
    /// <summary>
    /// エネミーの死亡判定
    /// </summary>
    /// <returns>死んでいればtrue</returns>
    private bool EnemyDead()
    {
        if (EnemyManager.Instance.MainHP[EnemyManager.Instance.CurrentTargetIndex] <= 0) return true;
        return false;
    }

	void HitBody(float _delayTime)
	{
		int _enemyMainDEF = EnemyManager.Instance.MainDEF[EnemyManager.Instance.CurrentTargetIndex];
		EnemyManager.Instance.ToEnemyMainDamage(calculation.CalcDamage(playerSTR, _enemyMainDEF));
		StartCoroutine(CreateEffect(hitEffect[(int)HitEffectName.BLOOD_MARK],
					   SecondaryCirclePos,
					   _delayTime));
		AudioManager.Instance.PlaySe("tamaniku.wav", _delayTime);
	}

	IEnumerator CreateEffect(GameObject _effect, Vector3 _effectPosition, float _delayTime)
	{
		yield return new WaitForSeconds(_delayTime);
		Instantiate(_effect, _effectPosition, Quaternion.identity);
	}

	public void Gameover()
	{
		var _refObj = Instantiate(gameoverKilledHuman);
		_refObj.transform.SetParent(gameoverCanvas, false);
	}


    public void SetCircleColliderEnable(bool _cond) { circleColliderEnable = _cond; }
    public void SetAttackCircleObject(GameObject _refObj) { attackCircle = _refObj; }
}