using UnityEngine;
using System.Collections;

public class MainGear : MonoBehaviour {
	[SerializeField]
	float angleSpeed;

	void Awake()
	{

	}

	IEnumerator MainRotate()
	{
		while (true)
		{
			transform.Rotate(new Vector3(0, 0, angleSpeed));
			yield return null;
		}
	}
}
