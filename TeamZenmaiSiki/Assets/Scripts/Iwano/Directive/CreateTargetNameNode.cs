using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateTargetNameNode : MonoBehaviour
{
    [SerializeField]
    GameObject node;

	// Use this for initialization
	void Start ()
    {
        //int chapterNum = DataManager.Instance.ScenarioChapterNumber;
        //int sectionNum = DataManager.Instance.ScenarioSectionNumber;

        //for(int i= 0;i < DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName.Count;i++)
        //{
        //    CreateNode(DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName[i]);
        //}

        for (int i = 0; i < 5; i++)
        {
            CreateNode("だいちゃん" + i);
        }


    }

    void CreateNode(string name_)
    {
        GameObject obj = Instantiate(node) as GameObject;
        obj.transform.parent = this.transform;//parentObj.transform;
        obj.GetComponent<Text>().text = name_;
    }
}
