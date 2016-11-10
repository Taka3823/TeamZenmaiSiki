using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ReturnCanvas : MonoBehaviour {

    // Use this for initialization
    static Canvas returnCanvas;
    void Awake()
    {
        returnCanvas = GetComponent<Canvas>();
        returnCanvas.enabled = false;
    }
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    static public void setenableReturnUI(bool flag)
    {
       returnCanvas.enabled = flag;
    }
    public void BackSearch()
    {
        returnCanvas.enabled = false;
    }
}
