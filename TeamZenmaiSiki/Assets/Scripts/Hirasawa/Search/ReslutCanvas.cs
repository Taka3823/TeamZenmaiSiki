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

    bool[] ischeck;
    bool isset;
    DataManager.DirectiveData datas;
    // Use this for initialization
    void Start () {
        GetComponent<Canvas>().enabled = false;
        instance = this;
        int chapNum = DataManager.Instance.ScenarioChapterNumber;
        int sectionNum = DataManager.Instance.ScenarioSectionNumber;
        datas = DataManager.Instance.DirectiveDatas[chapNum][sectionNum];
        ischeck = new bool[3];
        isset = false;
        ischeck[0] = false;
        ischeck[1] = false;
        ischeck[2] = false;
        GameObject SpecialObj = Instantiate(Special) as GameObject;
        SpecialObj.transform.SetParent(BackGround.transform);
        SpecialObj.transform.position = new Vector3(-426,167,0);
        //SpecialObj.transform.position = new Vector3(-300, 0, 0);
        GameObject CollectObj = Instantiate(Collect, new Vector3(180, 30, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
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
            if (isset == false)
            {
                set();
                isset = true;
            }
            if (ischeck[0])
            {
                ChangeObject(FirstCollect);
            }
            if (ischeck[1])
            {
                ChangeObject(SecondCollect);
            }
            if (ischeck[2])
            {
                ChangeObject(ThirdCollect);
            }
        }
	}
    public void set()
    {
        for(int i = 0; i < 3; i++)
        {
            ischeck[i] = check(i);
        }
    }
    private bool check(int count)
    {
        bool ischeck = false;
        string type = getType(count);
        string value = getValue(count);
        int killcount = 0;
        switch (type)
        {
            case "PHYSICAL":
                
                int playerHp = int.Parse(value);
                ischeck = DataManager.Instance.PlayerData.hp >= playerHp;
                break;
            case "SPECIFIC":
                for(int i = 0; i < DataManager.Instance.KillNum; i++)
                {
                    if (DataManager.Instance.KillNames[i]==value)
                    {
                        ischeck = true;
                        break;
                    }
                }
                break;
            case "RECOVER":
             
                for (int i = 0; i < datas.collectionTargetName.Count; i++)
                {
                    for(int k = 0; k < DataManager.Instance.KillNames.Count; k++)
                    {
                        if (DataManager.Instance.KillNames[k] == datas.collectionTargetName[i])
                        {
                            killcount++;
                        }
                    }
              
                }
                ischeck = killcount >= int.Parse(value);
                break;
            case "BRAKECORE":
                int breakcore = DataManager.Instance.BrakedCoreCount;
                ischeck = (breakcore >= int.Parse(value));
                break;
            case "ALLKILL":
                for (int i = 0; i < datas.collectionTargetName.Count; i++)
                {
                    bool iskill = false;
                    for (int k = 0; k < DataManager.Instance.KillNames.Count; k++)
                    {
                        if (DataManager.Instance.KillNames[k] == datas.collectionTargetName[i])
                        {
                            iskill = true;
                        }
                    }
                    if (!iskill)
                    {
                        ischeck = false;
                        return ischeck;
                    }
                }
                return true;
        }
        return ischeck;
    }
    private string getType(int count)
    {
        switch (count)
        {
            case 0:
                return datas.firstMission;
            case 1:
                return datas.secondMission;
            case 2:
                return datas.thirdMission;
        }
        return "バグです";
    }

    private string getValue(int count)
    {
        switch (count)
        {
            case 0:
                return datas.firstMissionAchievementCondition;
            case 1:
                return datas.secondMissionAchievementCondition;
            case 2:
                return datas.thirdMissionAchievementCondition;
        }
        return "バグです";
    }
    private void ChangeObject(GameObject obj)
    {
        Color color = obj.GetComponent<Image>().color;
        color.g -= 0.01f;
        color.b -= 0.01f;
        obj.GetComponent<Image>().color = color;
    }
}
