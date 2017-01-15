using UnityEngine;
using System.Collections;

public class EnemyAttackEffect : MonoBehaviour {
	[SerializeField]
	Vector2 maxShakeDiff;

	Vector3 cameraBasePosition;

	void Awake()
	{
		cameraBasePosition = transform.position;
	}

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

	void SetupNeutralPosition()
	{
		transform.position = cameraBasePosition;
	}
}
