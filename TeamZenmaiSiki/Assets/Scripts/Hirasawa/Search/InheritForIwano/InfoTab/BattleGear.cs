using UnityEngine;
using System.Collections;

public class BattleGear : MonoBehaviour {

    // Use this for initialization
    Vector3 startrotation;
	void Start () {
        startrotation = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (TabManager.Instance.Getisblood())
        {
            //Debug.Log();
            transform.Rotate(new Vector3(0,0,-2));
        }
	}
}
