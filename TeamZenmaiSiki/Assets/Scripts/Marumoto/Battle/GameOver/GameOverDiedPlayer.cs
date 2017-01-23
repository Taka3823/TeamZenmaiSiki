using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// プレイヤー死亡時のGameover演出を自作。
/// </summary>
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

	/// <summary>
	/// 順序立てて進めたいアニメーションをここで一括に管理。
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// 瞼が閉じるような動きのアニメーション。
	/// </summary>
	/// <param name="_startTime">アニメーション開始時間(秒)。</param>
	/// <param name="_moveTime">何秒かけてアニメーションするか。</param>
	/// <param name="_startPosTop">上瞼の初期位置。</param>
	/// <param name="_startPosBottom">下瞼の初期位置。</param>
	/// <returns></returns>
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

	/// <summary>
	/// 画像のをどんどん不透明に。
	/// </summary>
	/// <param name="_image">対象のSprite</param>
	/// <param name="_fadeSpeed">1フレームで加算する値。</param>
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
	/// 画像をどんどん透明に。
	/// </summary>
	/// <param name="_image">対象のSprite</param>
	/// <param name="_fadeSpeed">1フレームで加算する値。</param>
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
	/// アニメーションがすべて終わったら自動的にシナリオ選択画面へ遷移。
	/// </summary>
	private void LoadSceneScenarioChoice()
	{
		SceneManager.LoadScene("ScenarioChoice");
	}
}
