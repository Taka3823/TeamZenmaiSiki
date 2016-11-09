using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //MyCanvas.SetInteractive("Button",false);//仮です
	}
	
	// Update is called once per frame
	void Update () {
        float speed = 1.5f;
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
    //void OnTriggerEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "UnitObject(Clone)")
    //    {
    //        Debug.Log("Hit");
    //    }
    //    else {
    //        Debug.Log("anotherHit");
    //    }
    //}
}
