using UnityEngine;
using System.Collections;

public class BloodMarkAnimation : MonoBehaviour {
	[SerializeField]
	SpriteRenderer sprite;
	[SerializeField]
	float _crossValue;
	[SerializeField]
	float angleSpeed;

	float angle = 0.0f;
	

	void Start()
	{
		float randomRotationZ = Random.Range(0, 359);
		transform.Rotate(new Vector3(0, 0, randomRotationZ));
		StartCoroutine(FadeAlpha());
		StartCoroutine(FadeScale());
	}

	IEnumerator FadeAlpha()
	{
		while (true)
		{
			sprite.color -= new Color(0, 0, 0, angle);
			angle += angleSpeed;
			if (angle >= 255.0f)
			{
				Destroy(this.gameObject);
			}
			yield return null;
		}
	}

	IEnumerator FadeScale()
	{
		while (true)
		{
			transform.localScale = new Vector3(transform.localScale.x * _crossValue, 
				                               transform.localScale.y * _crossValue, 
											   transform.localScale.z);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
