using UnityEngine;
using System.Collections;

public class BulletSparkAnimation : MonoBehaviour {
	int count = 0;
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
