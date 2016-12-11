using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateEpsiodeNode : MonoBehaviour
{
    [SerializeField]
    GameObject nodePrefab;

    [SerializeField]
    ReadDirective readDirective;
    
	// Use this for initialization
	void Start ()
    {
        readDirective.ReadFile(1);

        for(int i = 1;i < readDirective.LineLength;i++)
        {
            CreateNode(1, i);
        }
    }

    void CreateNode(int chaptrerNum_,int sectionNum_)
    {
        GameObject obj = Instantiate(nodePrefab) as GameObject;
        obj.name = chaptrerNum_.ToString() + "_" + sectionNum_.ToString();

        obj.transform.parent = transform;

        obj.GetComponent<ButtonReaction>().ChapterNumber = chaptrerNum_;
        obj.GetComponent<ButtonReaction>().SectionNumber = sectionNum_;

        GameObject child = obj.transform.FindChild("EpisodeName").gameObject;
        GameObject grandChild = child.transform.FindChild("Text").gameObject;

        grandChild.GetComponent<Text>().fontSize = 60;
        grandChild.GetComponent<Text>().color = Color.white;

        grandChild.GetComponent<Text>().text = DataManager.Instance.DirectiveDatas[chaptrerNum_-1][sectionNum_].scenarioTitle;
    }
}
