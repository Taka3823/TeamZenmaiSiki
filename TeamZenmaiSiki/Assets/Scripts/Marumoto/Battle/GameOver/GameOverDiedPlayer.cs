using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverDiedPlayer : MonoBehaviour {
	[SerializeField]
	Image eyeSight;
	[SerializeField]
	Transform topEyelid;
	[SerializeField]
	Transform bottomEyelid;
	[SerializeField]
	GameObject gameover;
	[SerializeField]
	AnimationCurve eyelidsCurve;

	Image gameoverImage;


	void Awake()
	{
		gameoverImage = gameover.GetComponent<Image>();
		StartCoroutine(RoutineAnimation());
		StartCoroutine(FadeAlpha(eyeSight, 0.008f));
	}

	IEnumerator RoutineAnimation()
	{
		float _startTime = Time.timeSinceLevelLoad;
		yield return StartCoroutine(FadeEyelids(_startTime, 4.0f, topEyelid.localPosition, bottomEyelid.localPosition));
		yield return StartCoroutine(FadeAlpha(gameoverImage, 0.008f));
		yield return new WaitForSeconds(2.0f);
		yield return StartCoroutine(FadeAlphaReverse(gameoverImage, 0.008f));
		float _waitTime = 1.5f;
		FadeManager.Instance.FadeInOut(_waitTime - 0.1f, 0.3f);
		Invoke("LoadSceneScenarioChoice", _waitTime);
	}

	IEnumerator FadeEyelids(float _startTime, float _moveTime, Vector3 _startPosTop, Vector3 _startPosBottom)
	{
		Vector3 _endPos = new Vector3(0f, 0f, 0f);
		while (true)
		{
			float diffTime = Time.timeSinceLevelLoad - _startTime;
			if (diffTime > _moveTime)
			{
				yield break;
			}
			float rate = diffTime / _moveTime;
			float pos = eyelidsCurve.Evaluate(rate);

			topEyelid.localPosition = Vector3.Lerp(_startPosTop, _endPos, pos);
			bottomEyelid.localPosition = Vector3.Lerp(_startPosBottom, _endPos, pos);
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
