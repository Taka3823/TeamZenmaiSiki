using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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

	IEnumerator FadeAlpha(Image _image, float _fadeSpeed)
	{
		while (true)
		{
			if (_image.color.a >= 1.0f) yield break;
			_image.color += new Color(0f, 0f, 0f, _fadeSpeed);

			yield return null;
		}
	}

	IEnumerator FadeAlphaReverse(Image _image, float _fadeSpeed)
	{
		while (true)
		{
			if (_image.color.a <= 0) yield break;
			_image.color -= new Color(0f, 0f, 0f, _fadeSpeed);

			yield return null;
		}
	}

	private void LoadSceneScenarioChoice()
	{
		SceneManager.LoadScene("ScenarioChoice");
	}
}
