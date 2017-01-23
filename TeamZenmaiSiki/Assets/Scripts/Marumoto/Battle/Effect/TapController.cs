using UnityEngine;

/// <summary>
/// 画面タップ音の管理。
/// </summary>
public class TapController : MonoBehaviour {

	void Update()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp(0))
		{
			AudioManager.Instance.PlaySe("tap.wav");
		}
#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Ended)
			{
				AudioManager.Instance.PlaySe("tap.wav");
			}
		}
#endif
	}
}
