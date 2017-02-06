using UnityEngine;
using System.Collections;

/// <summary>
/// エネミーの攻撃時、画面が揺れる処理。
/// </summary>
public class EnemyAttackEffect : MonoBehaviour {
	[SerializeField]
	Vector2 maxShakeDiff;       //揺らした時、cameraBasePositionからの揺れ幅の最大値。

	Vector3 cameraBasePosition; //カメラの初期位置


	void Awake()
	{
		cameraBasePosition = transform.position;
	}

	/// <summary>
	/// カメラを揺らす。
	/// </summary>
	/// <param name="_shakeTime">カメラを揺らす時間。(秒)</param>
	/// <param name="_waitTime">カメラを揺らすまでの待機時間。(秒)</param>
	/// <returns></returns>
	public IEnumerator CameraShaking(float _shakeTime, float _waitTime)
	{
		yield return new WaitForSeconds(_waitTime);
		Vector2 _shakeValue;
		float _startTime = Time.timeSinceLevelLoad;
		float _shakeRateSeconds = 0.05f;

		while (true)
		{
			float _x = Random.Range(-maxShakeDiff.x, maxShakeDiff.x);
			float _y = Random.Range(-maxShakeDiff.y, maxShakeDiff.y);
			_shakeValue = new Vector2(_x, _y);

			transform.position = new Vector3(cameraBasePosition.x + _shakeValue.x,
											 cameraBasePosition.y + _shakeValue.y,
											 cameraBasePosition.z);

			yield return new WaitForSeconds(_shakeRateSeconds);

			float _hasMoveTime = Time.timeSinceLevelLoad - _startTime;
			if (_hasMoveTime >= _shakeTime)
			{
				SetupNeutralPosition();
				yield break;
			}
		}
	}

	/// <summary>
	/// カメラのPositionをもとの場所へもどす。（揺らした後に呼び出してください。）
	/// </summary>
	void SetupNeutralPosition()
	{
		transform.position = cameraBasePosition;
	}
}
