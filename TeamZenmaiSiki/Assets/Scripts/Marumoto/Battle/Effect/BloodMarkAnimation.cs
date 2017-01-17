using UnityEngine;
using System.Collections;

public class BloodMarkAnimation : MonoBehaviour {
	[SerializeField]
	SpriteRenderer sprite;
	[SerializeField]
	float _crossValue;
	[SerializeField]
	float timeRate;
	[SerializeField]
	float fallSpeed;

	float time = 0.0f;
	

	void Start()
	{
		float randomRotationZ = Random.Range(0, 359);
		transform.Rotate(new Vector3(0, 0, randomRotationZ));
		StartCoroutine(FadeAlpha());
		StartCoroutine(FadeScale());
		StartCoroutine(FadePosition());
	}

	IEnumerator FadeAlpha()
	{
		while (true)
		{
			sprite.color -= new Color(0, 0, 0, time);
			time += timeRate;
			if (time >= 255.0f)
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
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator FadePosition()
	{
		float _fallValue = 0.0f;
		while (true)
		{
			transform.position -= new Vector3(0,
											  _fallValue,
											  0);
			yield return null;
			_fallValue += fallSpeed;
		}
	}
}
