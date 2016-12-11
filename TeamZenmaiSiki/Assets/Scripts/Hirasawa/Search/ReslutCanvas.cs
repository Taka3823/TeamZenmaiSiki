using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ReslutCanvas : MonoBehaviour {

    private static ReslutCanvas instance;

    public static ReslutCanvas Instance
    {
        get { return instance; }
    }
    [SerializeField]
    GameObject BackGround;
    [SerializeField]
    GameObject Special;
    [SerializeField]
    GameObject Collect;
    [SerializeField]
    GameObject FirstCollect;
    [SerializeField]
    GameObject SecondCollect;
    [SerializeField]
    GameObject ThirdCollect;
    // Use this for initialization
    void Start () {
        GetComponent<Canvas>().enabled = false;
        instance = this;
        GameObject SpecialObj = Instantiate(Special, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        SpecialObj.transform.SetParent(BackGround.transform);
        SpecialObj.transform.localPosition = new Vector3(-360, 140, 0);
        GameObject CollectObj = Instantiate(Collect, new Vector3(1065, 430, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        CollectObj.transform.SetParent(BackGround.transform);
    }
	public void SetEnable(bool flag)
    {
        GetComponent<Canvas>().enabled = flag;
    }
    public void SetDebug()
    {
        ThirdCollect.GetComponent<Image>().color = Color.red;
    }
	// Update is called once per frame
	void Update () {
        if (GetComponent<Canvas>().isActiveAndEnabled)
        {
            Color color= ThirdCollect.GetComponent<Image>().color;
            color.g-= 0.01f;
            color.b -= 0.01f;
            ThirdCollect.GetComponent<Image>().color = color;
        }
	}
}
