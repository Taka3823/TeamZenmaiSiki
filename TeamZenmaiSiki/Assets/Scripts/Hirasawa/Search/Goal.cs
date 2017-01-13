using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    Vector3 startlocal;
    // Use this for initialization
    float angle = 0;
	void Start () {
        startlocal = transform.localPosition;
	}
	// Update is called once per frame
	void Update () {
        angle += 0.03f;
        float trans_y = 0.05f*Mathf.Sin(angle);
        transform.localPosition = new Vector3(transform.localPosition.x, startlocal.y+trans_y, transform.localPosition.z);
	}
}
