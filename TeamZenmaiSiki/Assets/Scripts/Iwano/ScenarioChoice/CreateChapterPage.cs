using UnityEngine;
using System.Collections;

public class CreateChapterPage : MonoBehaviour
{
    [SerializeField]
    GameObject chapterPageBase;

    [SerializeField]
    GameObject parentObj;
    
    [SerializeField]
    GameObject nodePrefab;

    [SerializeField]
    ReadDirective readDirective;

    // Use this for initialization
    void Start ()
    {
        //TIPS:こっちが本番
        //for(int i = 0;i < SaveManager.Instance.GetClearChapterNum() + 1;i++)
        for (int i = 0; i < 3 + 1; i++)
        {
            CreatePage(i);
        }
	}

    void CreatePage(int num)
    {
        GameObject obj = Instantiate(chapterPageBase)as GameObject;
        obj.transform.SetParent(parentObj.transform);
        obj.transform.localPosition = new Vector3(0 + Screen.width * num, 0, 0);


        GameObject child = obj.transform.FindChild("ScrollView").gameObject;
        GameObject grandChild = child.transform.FindChild("Content").gameObject;

        grandChild.GetComponent<CreateEpsiodeNode>().NodePrefab = nodePrefab;
        grandChild.GetComponent<CreateEpsiodeNode>().ReadDirectiveData = readDirective;
        grandChild.GetComponent<CreateEpsiodeNode>().DesignationChapterNum = num + 1;
        grandChild.GetComponent<CreateEpsiodeNode>().DataInit();
    }
}
