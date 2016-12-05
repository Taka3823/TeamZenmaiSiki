using UnityEngine;
using System.Collections;

public class SetSprite : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {
        string pass = "Sprits/Search/BackGround/view_c_XX";
        Sprite image = new Sprite();
        image = Resources.Load<Sprite>(pass);
        GetComponent<SpriteRenderer>().sprite = image;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
