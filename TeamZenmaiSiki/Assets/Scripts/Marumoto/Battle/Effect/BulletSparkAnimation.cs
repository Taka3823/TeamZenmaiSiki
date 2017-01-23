using UnityEngine;

/// <summary>
/// 銃弾のヒット判定がコアフレームだった時の跳弾のスプライトアニメーション。
/// </summary>
public class BulletSparkAnimation : MonoBehaviour {
	float time = 0.0f;

	void Update ()
	{
		if (time > 0.6f)
		{
			Destroy(this.gameObject);
		}
		time += Time.deltaTime;
	}
}
