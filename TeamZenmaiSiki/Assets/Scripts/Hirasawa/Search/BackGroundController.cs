using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {

    private Vector3 startpos;
    [SerializeField]
    float movevalue;
	// Use this for initialization
	void Start () {
        startpos = this.transform.position;
        var sr = GetComponent<SpriteRenderer>();
        var width = sr.bounds.size.x;
        //Debug.Log(width);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camerapos = new Vector3(NewCamera.Instance.cameraposx/movevalue, 0, 0);
        transform.position = startpos + camerapos;
    }
}
