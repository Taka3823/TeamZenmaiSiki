using UnityEngine;
using System.Collections;

public class BackGear : MonoBehaviour {

	void Update () {
        if (!TabManager.Instance.GetIsDisplay()) return;
        transform.Rotate(new Vector3(0,0,1));
	}
}
