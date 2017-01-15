using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateEpsiodeNode : MonoBehaviour
{   
    private GameObject nodePrefab;

    public GameObject NodePrefab
    {
        set { nodePrefab = value; }
    }
    
    private ReadDirective readDirective;

    public ReadDirective ReadDirectiveData
    {
        set { readDirective = value; }
    }

    //CreateChapterPageから受け取る引数の仮置き
    private int designationChapterNum;

    public int DesignationChapterNum
    {
        get { return designationChapterNum; }
        set { designationChapterNum = value; }
    }
    
    //大ちゃんからもらう数値の仮置き
    int GetClearSectionNum = 0;

    //void Start()
    //{
    //    DataInit();

    //    Destroy(GetComponent<CreateEpsiodeNode>());
    //}

    public void DataInit()
    {
        //readDirective.ReadFile(chapterNumber);

        //for (int i = 1; i < readDirective.LineLength; i++)
        //{
        //    CreateNode(chapterNumber, i - 1);
        //}

        if(designationChapterNum <= 0)
        {
            designationChapterNum = 1;
        }
        
        readDirective.ReadFile(designationChapterNum);

        for (int i = 0; i < readDirective.LineLength;i++)
        {
            if(i == 0)
            {
                i = 1;
            }

            CreateNode(designationChapterNum, i - 1);
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
        grandChild.GetComponent<Text>().color = new Color(1,1,1,0.5f);
        
        grandChild.GetComponent<Text>().text = DataManager.Instance.DirectiveDatas[chaptrerNum_-1][sectionNum_].scenarioTitle;
    }
}
