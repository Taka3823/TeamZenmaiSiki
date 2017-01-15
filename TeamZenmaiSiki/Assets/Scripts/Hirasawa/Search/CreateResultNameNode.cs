using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CreateResultNameNode : MonoBehaviour {

    [SerializeField]
    GameObject node;

    // Use this for initialization
    void Start()
    {
        int chapterNum = DataManager.Instance.ScenarioChapterNumber;
        int sectionNum = DataManager.Instance.ScenarioSectionNumber;

        for (int i = 0; i < DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName.Count; i++)
        {
            CreateNode(DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName[i],i);
        }

        //for (int i = 0; i < 5; i++)
        //{
        //    CreateNode("だいちゃん" + i);
        //}
    }

    void CreateNode(string name_,int num)
    {
        GameObject obj = Instantiate(node) as GameObject;
        obj.transform.parent = this.transform;
        obj.transform.position = Vector3.zero;

        obj.GetComponent<Text>().text = name_;
        int chapterNum = DataManager.Instance.ScenarioChapterNumber;
        int sectionNum = DataManager.Instance.ScenarioSectionNumber;
        bool[] ischeck;
        ischeck = new bool[DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName.Count];
         
        for (int i = 0; i < ischeck.Length; i++)
        {
            for(int j = 0; j < DataManager.Instance.KillNames.Count; j++)
            {
                if(DataManager.Instance.DirectiveDatas[chapterNum][sectionNum].collectionTargetName[i]== DataManager.Instance.KillNames[j])
                {
                    ischeck[i] = true;
                    break;
                }
                else
                {
                    ischeck[i] = false;
                }
            }
        }
        foreach (Transform child in obj.transform)
        {
            if(ischeck[num])
            child.gameObject.GetComponent<Image>().enabled = true;
        }

    }
}
