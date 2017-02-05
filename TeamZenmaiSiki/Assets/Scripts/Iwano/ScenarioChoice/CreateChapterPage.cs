using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateChapterPage : MonoBehaviour
{
    [SerializeField]
    GameObject chapterPageBase;

    [SerializeField]
    GameObject parentObj;
    
    [SerializeField]
    GameObject nodePrefab;

    [SerializeField]
    GameObject fadePrefab;

    [SerializeField]
    ReadDirective readDirective;

    [SerializeField]
    Sprite[] backGround;

    string[] title =
    {
        "第一章　君が生れた日",
        "第ニ章　機械奴隷回収計画",
        "第三章　こぼれそうな思い",
        "第四章　狂い始めた機械",
        "第五章　反撃の狼煙を",

        "第六章　変調",
        "第七章　不要な駒",
        "第八章　降り積もる雪の中で",
        "第九章　Thank you My buddy.",
        "第十章　彼が守りたかったもの"
    };

    // Use this for initialization
    void Start ()
    {
        //TIPS:こっちが本番
        for(int i = 0;i < SaveManager.Instance.GetClearChapterNum() + 1;i++)
        //Debug用
        //for (int i = 0; i < 3 + 1; i++)
        {
            CreatePage(i);
        }
	}

    void CreatePage(int num)
    {
        GameObject obj = Instantiate(chapterPageBase)as GameObject;
        obj.transform.SetParent(parentObj.transform);
        obj.transform.localPosition = new Vector3(0 + Screen.width * num, 0, 0);
        //obj.transform.localScale = new Vector2(Screen.width,Screen.height);

        GameObject anotherChild = obj.transform.FindChild("ChapterTitle").gameObject;
        anotherChild.GetComponent<Text>().text = title[num];

        GameObject child = obj.transform.FindChild("ScrollView").gameObject;
        GameObject grandChild = child.transform.FindChild("Content").gameObject;

        grandChild.GetComponent<CreateEpsiodeNode>().NodePrefab = nodePrefab;
        grandChild.GetComponent<CreateEpsiodeNode>().FadePrefab = fadePrefab;
        grandChild.GetComponent<CreateEpsiodeNode>().ReadDirectiveData = readDirective;
        grandChild.GetComponent<CreateEpsiodeNode>().DesignationChapterNum = num + 1;
        grandChild.GetComponent<CreateEpsiodeNode>().DataInit();

        //FIX
        //なんかこっから下でNewGameObjectが8個生成される

        int chapterNum = num + 1;
        int sectionClearNum = SaveManager.Instance.GetClearSection(chapterNum) + 1;

        GameObject child2 = new GameObject();
        GameObject ggChild = new GameObject();
    
        for(int i = 0;i < grandChild.transform.childCount;i++)
        {
           child2 = grandChild.transform.FindChild(chapterNum + "_" + i).gameObject;

           GameObject grandChild2 = child2.transform.FindChild("EpisodeName").gameObject;
           ggChild = grandChild2.transform.FindChild("Text").gameObject;

            if(i < sectionClearNum)
            {
                child2.GetComponent<Button>().interactable = true;
                ggChild.GetComponent<Text>().color = new Color(1,1,1,1);
            }
        }
    }
}
