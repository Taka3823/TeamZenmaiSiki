using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class DirectivetitleDicide : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        int temp = DataManager.Instance.ScenarioChapterNumber;
        int temp2 = DataManager.Instance.ScenarioSectionNumber;

        GetComponent<Text>().text = DataManager.Instance.DirectiveDatas[temp][temp2].scenarioTitle;
        //GetComponent<Text>().text = "だいちゃんだけ元気";
    }
}
