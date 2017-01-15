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
    GameObject text;
    [SerializeField]
    GameObject BackGround;
    [SerializeField]
    GameObject Special;
    [SerializeField]
    GameObject Collect;
    [SerializeField]
    GameObject[] list;
    [SerializeField]
    GameObject[] lightlist;
    bool[] ischeck;
    bool[] islightend;
    bool[] ischangeend;
    bool isset;
    bool[] iseffectEnd;
    DataManager.DirectiveData datas;
    // Use this for initialization
    void Start () {
        GetComponent<Canvas>().enabled = false;
        instance = this;
        int chapNum = DataManager.Instance.ScenarioChapterNumber;
        int sectionNum = DataManager.Instance.ScenarioSectionNumber;
        datas = DataManager.Instance.DirectiveDatas[chapNum][sectionNum];
        ischeck = new bool[3];
        islightend = new bool[3];
        ischangeend = new bool[3];
        iseffectEnd = new bool[3];
        for(int i = 0; i < 3; i++)
        {
            ischeck[i] = false;
            islightend[i] = false;
            ischangeend[i] = false;
        }
        isset = false;
        GameObject SpecialObj = Instantiate(Special) as GameObject;
        SpecialObj.transform.SetParent(BackGround.transform);
        SpecialObj.transform.position = new Vector3(-426,167,0);
        //SpecialObj.transform.position = new Vector3(-300, 0, 0);
        GameObject CollectObj = Instantiate(Collect, new Vector3(180, 30, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        CollectObj.transform.SetParent(BackGround.transform);
        text.GetComponent<Text>().text = DataManager.Instance.DirectiveDatas[DataManager.Instance.ScenarioChapterNumber][DataManager.Instance.ScenarioSectionNumber].scenarioTitle;
    }
	public void SetEnable(bool flag)
    {
        GetComponent<Canvas>().enabled = flag;
    }
    public void SaveData()
    {
        int checknum = 0;
        for(int i = 0; i < 3; i++)
        {
            if (ischeck[i])
            {
                checknum++;
            }
        }
        int chapter = DataManager.Instance.ScenarioChapterNumber;
        int section = DataManager.Instance.ScenarioSectionNumber;
        int killnum;
        if (SaveManager.Instance.GetSaveData(chapter, section).cleartype == SaveManager.ClearType.CLEAR)
        {
            killnum = SaveManager.Instance.GetSaveData(chapter, section).destroyNum;
        }
        else
        {
            killnum = DataManager.Instance.KillNames.Count;
        }
        int achieve;
        achieve = Mathf.Max(checknum, SaveManager.Instance.GetSaveData(chapter, section).achieveSpecial);

        SaveManager.Instance.ScenarioSave(killnum, achieve);
    }
    public void SetDebug()
    {
        //ThirdCollect.GetComponent<Image>().color = Color.red;
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
            if (FadeManager.Instance.IsFadeEffect()) return;
            for(int i = 0; i < list.Length; i++)
            {
                if (iseffectEnd[i]) continue;
                if (ischeck[i])
                {
                    if (i == 0)
                    {
                        ChangeObject(list[i], i);
                        if (ischangeend[i])
                        {
                            LightChange(lightlist[i], i);
                        }
                    }
                    else
                    {
                        if (iseffectEnd[i - 1])
                        {
                            ChangeObject(list[i], i);
                            if (ischangeend[i])
                            {
                                LightChange(lightlist[i], i);
                            }
                        }
                        
                    }
                    
                }
                
            }
        }
	}
    public void set()
    {
        for(int i = 0; i < 3; i++)
        {
            ischeck[i] = check(i);
            iseffectEnd[i] = (!ischeck[i]);
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
    private void ChangeObject(GameObject obj,int num)
    {


        obj.GetComponent<Image>().fillAmount += 0.02f;
        if (obj.GetComponent<Image>().fillAmount >= 1.0f)
        {
            ischangeend[num] = true;
        }
    }
    private void LightChange(GameObject obj,int num)
    {
        float speed = 0.15f;
        if (ischangeend[num])
        {
            if (!islightend[num])
            {
                obj.GetComponent<Image>().fillAmount += speed;
                if (obj.GetComponent<Image>().fillAmount >= 1.0f)
                {
                    islightend[num] = true;
                    obj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                obj.GetComponent<Image>().fillAmount -= speed;
                if (obj.GetComponent<Image>().fillAmount <= 0.0f)
                {
                    iseffectEnd[num] = true;
                }
            }
           
        }
    }
}
