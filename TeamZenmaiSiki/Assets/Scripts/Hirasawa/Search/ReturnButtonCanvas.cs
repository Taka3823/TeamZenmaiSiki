using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ReturnButtonCanvas : MonoBehaviour {

    // Use this for initialization
    private static ReturnButtonCanvas instance;

    public static ReturnButtonCanvas Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        
    }
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setenableReturnUI(bool flag)
    {

        foreach (Transform child in transform)
        {
            child.GetComponent<Button>().interactable = flag;
        }
    }
}
