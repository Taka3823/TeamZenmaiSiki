using UnityEngine;
using System.Collections;

public class BigBackGear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!TabManager.Instance.GetIsDisplay()) return;
        transform.Rotate(new Vector3(0,0,-6.0f/8.0f));
	}
}
