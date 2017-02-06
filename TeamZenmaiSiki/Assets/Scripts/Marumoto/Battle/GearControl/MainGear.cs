using UnityEngine;
using System.Collections;

/// <summary>
/// 一番大きい左上のギアを回す。
/// </summary>
public class MainGear : MonoBehaviour {
	[SerializeField]
	float angleSpeed;
	[SerializeField]
	float reverseAngleSpeed;
	[SerializeField]
	float accelAngleSpeed;

	void Awake()
	{
		StartCoroutine(MainRotate());
		StartCoroutine(ManageRotation());
	}

	IEnumerator MainRotate()
	{
		while (true)
		{
			transform.Rotate(new Vector3(0, 0, angleSpeed));
			yield return null;
		}
	}

	/// <summary>
	/// 回転の不規則化の1ルーチン。
	/// </summary>
	/// <returns></returns>
	IEnumerator ManageRotation()
	{
		while (true)
		{
			yield return StartCoroutine(RotateRoutine(StopRotate(0.5f), 3.0f));
			yield return StartCoroutine(RotateRoutine(StopRotate(1.0f), 0.7f));
			yield return StartCoroutine(RotateRoutine(ReverseRotate(2.0f), 2.0f));
			yield return StartCoroutine(RotateRoutine(AccelRotate(5.0f), 1.0f));
			yield return StartCoroutine(RotateRoutine(StopRotate(0.7f), 1.0f));
			yield return StartCoroutine(RotateRoutine(StopRotate(0.7f), 0.7f));
			yield return StartCoroutine(RotateRoutine(StopRotate(1.0f), 0.4f));
			yield return StartCoroutine(RotateRoutine(AccelRotate(4.0f), 1.0f));
			yield return StartCoroutine(RotateRoutine(StopRotate(0.5f), 0.0f));
			yield return StartCoroutine(RotateRoutine(ReverseRotate(5.0f), 0.0f));
			yield return StartCoroutine(RotateRoutine(StopRotate(0.6f), 0.0f));
		}
	}

	/// <summary>
	/// ギア回転のラッピング。
	/// </summary>
	/// <param name="_coroutine">使いたいコルーチン指定。</param>
	/// <param name="_waitTime">コルーチン開始までの待機時間。</param>
	/// <returns></returns>
	IEnumerator RotateRoutine (IEnumerator _coroutine, float _waitTime)
	{
		yield return new WaitForSeconds(_waitTime);
		yield return StartCoroutine(_coroutine);
	}

	/// <summary>
	/// 逆回転。
	/// </summary>
	/// <param name="_activeTime">何秒するか。</param>
	/// <returns></returns>
	IEnumerator ReverseRotate(float _activeTime)
	{
		while (_activeTime > 0)
		{
			transform.Rotate(new Vector3(0, 0, -reverseAngleSpeed));
			_activeTime -= Time.deltaTime;
			yield return null;
		}
	}

	/// <summary>
	/// 加速回転。
	/// </summary>
	/// <param name="_activeTime">何秒するか。</param>
	/// <returns></returns>
	IEnumerator AccelRotate(float _activeTime)
	{
		while (_activeTime > 0)
		{
			transform.Rotate(new Vector3(0, 0, accelAngleSpeed));
			_activeTime -= Time.deltaTime;
			yield return null;
		}
	}

	/// <summary>
	/// 静止。
	/// </summary>
	/// <param name="_activeTime">何秒するか。</param>
	/// <returns></returns>
	IEnumerator StopRotate(float _activeTime)
	{
		while (_activeTime > 0)
		{
			transform.Rotate(new Vector3(0, 0, -angleSpeed));
			_activeTime -= Time.deltaTime;
			yield return null;
		}
	}
}
