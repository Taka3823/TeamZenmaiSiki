using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SetLightChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            position.x = position.x + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x*gameObject.transform.localScale.x;
            child.transform.position = position;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
