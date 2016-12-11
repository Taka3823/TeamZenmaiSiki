using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TabManager : MonoBehaviour {
    private static TabManager instance;

    public static TabManager Instance
    {
        get { return instance; }
    }
    bool isBlood = false;
    public bool Getisblood()
    {
        return isBlood;
    }
    public void Setisblood(bool isblood)
    {
        isBlood = isblood;
    }
    bool isDisplay_ = false;
    public bool GetIsDisplay()
    {
        return isDisplay_;
    }
    public void SetIsDisplay(bool _isdisplay)
    {
        isDisplay_ = _isdisplay;
    }
    // Use this for initialization
    void Start () {
        instance = this;
        isBlood = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
