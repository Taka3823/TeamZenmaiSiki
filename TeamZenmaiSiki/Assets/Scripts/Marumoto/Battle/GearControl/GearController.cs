using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GearController : MonoBehaviour
{
	[SerializeField]
	List<Transform> gears;

	[SerializeField]
	List<float> angleSpeed;

	void Update()
	{
		for (int i = 0; i < gears.Count; i++)
		{
			gears[i].Rotate(new Vector3(0, 0, angleSpeed[i]));
		}
	}
}