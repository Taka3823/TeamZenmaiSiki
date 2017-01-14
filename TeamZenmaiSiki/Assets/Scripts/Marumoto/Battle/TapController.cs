using UnityEngine;

public class TapController : MonoBehaviour {

	void Update()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp(0))
		{
			//タップ音再生
		}
#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Ended)
			{
				//タップ音再生
			}
		}
#endif
	}
}
