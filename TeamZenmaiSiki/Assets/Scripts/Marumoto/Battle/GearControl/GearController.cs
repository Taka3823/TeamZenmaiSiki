using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 戦闘シーンの四隅のギアを動かす処理。
/// </summary>
public class GearController : MonoBehaviour
{
	[SerializeField, Tooltip("子のギアオブジェクト")]
	List<Transform> gears;

	[SerializeField, Tooltip("それぞれのギアの回転速度。")]
	List<float> angleSpeed;

	void Update()
	{
		for (int i = 0; i < gears.Count; i++)
		{
			gears[i].Rotate(new Vector3(0, 0, angleSpeed[i]));
		}
	}
}