using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// 純粋な人間を殺してしまった時のGameover自作アニメーション。
/// </summary>
public class GameOverKilledHuman : MonoBehaviour {
	[SerializeField]
	Image background;
	[SerializeField]
	Transform hand;
	[SerializeField]
	Image gameoverImage;
	[SerializeField]
	AnimationCurve handCurve;

	void Awake()
	{
		StartCoroutine(FadeAlpha(background, 0.008f));
		StartCoroutine(RoutineAnimation());
	}

	/// <summary>
	/// 順序立ててアニメーションしたいものを一括管理。
	/// </summary>
	/// <returns></returns>
	IEnumerator RoutineAnimation()
	{
		yield return new WaitForSeconds(1.0f);
		float _startTime = Time.timeSinceLevelLoad;
		yield return StartCoroutine(FadeHandImage(_startTime, 3.0f, hand.localPosition));
		yield return StartCoroutine(FadeAlpha(gameoverImage, 0.008f));
		yield return new WaitForSeconds(3.0f);
		float _waitTime = 1.5f;
		FadeManager.Instance.FadeInOut(_waitTime - 0.1f, 0.3f);
		Invoke("LoadSceneScenarioChoice", _waitTime);
	}

	/// <summary>
	/// 手が下から伸びてくるアニメーション。
	/// </summary>
	/// <param name="_startTime">アニメーション開始時間。</param>
	/// <param name="_moveTime">アニメーションに使用する時間。</param>
	/// <param name="_startPos">手の画像の開始位置。</param>
	/// <returns></returns>
	IEnumerator FadeHandImage(float _startTime, float _moveTime, Vector3 _startPos)
	{
		Vector3 _endPos = new Vector3(0f, _startPos.y + 300.0f, 0f);
		while (true)
		{
			float diffTime = Time.timeSinceLevelLoad - _startTime;
			if (diffTime > _moveTime)
			{
				yield break;
			}
			float rate = diffTime / _moveTime;
			float pos = handCurve.Evaluate(rate);

			hand.localPosition = Vector3.Lerp(_startPos, _endPos, pos);
			yield return null;
		}
	}

	/// <summary>
	/// どんどん不透明に。
	/// </summary>
	/// <param name="_image">対象のSprite</param>
	/// <param name="_fadeSpeed">1フレーム当たりの加算値。</param>
	/// <returns></returns>
	IEnumerator FadeAlpha(Image _image, float _fadeSpeed)
	{
		while (true)
		{
			if (_image.color.a >= 1.0f) yield break;
			_image.color += new Color(0f, 0f, 0f, _fadeSpeed);

			yield return null;
		}
	}

	/// <summary>
	/// どんどん透明に。
	/// </summary>
	/// <param name="_image">対象のSprite</param>
	/// <param name="_fadeSpeed">1フレーム当たりの加算値。</param>
	/// <returns></returns>
	IEnumerator FadeAlphaReverse(Image _image, float _fadeSpeed)
	{
		while (true)
		{
			if (_image.color.a <= 0) yield break;
			_image.color -= new Color(0f, 0f, 0f, _fadeSpeed);

			yield return null;
		}
	}

	/// <summary>
	/// Gameoverアニメーションが終わったら自動的にシナリオ選択に飛ぶ。
	/// </summary>
	private void LoadSceneScenarioChoice()
	{
		SceneManager.LoadScene("ScenarioChoice");
	}
}
