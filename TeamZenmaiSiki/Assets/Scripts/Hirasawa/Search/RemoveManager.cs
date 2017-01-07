using UnityEngine;
using System.Collections;

public class RemoveManager : MonoBehaviour {

    private static RemoveManager instance;

    public static RemoveManager Instance
    {
        get { return instance; }
    }
    // Use this for initialization
    void Start () {
        instance = this;
        int a = DataManager.Instance.ScenarioChapterNumber;
        int b = DataManager.Instance.ScenarioSectionNumber;
        //Debug.Log(a);
        //Debug.Log(b);
        //datas = DataManager.Instance.DirectiveDatas[a][b];

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < DataManager.Instance.KillNames.Count; i++)
            {
                Debug.Log(DataManager.Instance.KillNames[i] + i);
            }
            Debug.Log(DataManager.Instance.KillNum);
        }
	}
}
