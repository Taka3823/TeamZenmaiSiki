using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SearchManager : MonoBehaviour , ISceneBase
{

	// Use this for initialization
	
    private static SearchManager instance;

    public static SearchManager Instance
    {
        get { return instance; }
    }
    private List<EnemyData.EnemyInternalDatas> sendDatas;
    private List<EnemyData.EnemyInternalDatas> batlleDataList;
    public List<EnemyData.EnemyInternalDatas> getSendEnemyDatas()
    {
        return sendDatas;
    }
    public List<EnemyData.EnemyInternalDatas> getBattleEnemyDatas()
    {
        return batlleDataList;
    }
    public void setEnemyDatas(List<EnemyData.EnemyInternalDatas> _sendDatas)
    {
        sendDatas = _sendDatas;
        List<EnemyData.EnemyInternalDatas> buf = new List<EnemyData.EnemyInternalDatas>();
        for(int i = 0; i < sendDatas.Count; i++)
        {
            if (sendDatas[i].isbattle)
            {
                buf.Add(sendDatas[i]);
            }
        }
        //batlleDataList = buf;
        DataManager.Instance.EnemyInternalDatas = buf;
        for (int j = 0; j < sendDatas.Count; j++)
        {
            Debug.Log(sendDatas[j].name);
        }
    }
    void Start()
    {
        instance = this;
        batlleDataList = new List<EnemyData.EnemyInternalDatas>();
        sendDatas = new List<EnemyData.EnemyInternalDatas>();
    }

    // Update is called once per frame
    void Update()
    {
        OnTouchDown();
    }
    public void GoScenario()
    {
        SceneChange("Scenario");
    }
    void OnTouchDown()
    {

        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit)
            {
                GameObject obj = hit.collider.gameObject;
                if (hit.collider.gameObject.tag == "Unit")
                {
                    obj.GetComponent<Unit>().OnTouchDown();
                }
                else if (hit.collider.tag == "Untagged")
                {
                    //Debug.Log("よくわからないもの");
                    MyCanvas.SetInteractive("Button", false);
                }
                else if (hit.collider.tag == "Goal")
                {
                    ReturnCanvas.setenableReturnUI(true);
                }

            }
            else
            {
                Debug.Log("はずれ");
                MyCanvas.SetInteractive("Button", false);
            }
        }

    }

    public void SceneChange(string nextSceneName_)
    {
        SceneManager.LoadScene(nextSceneName_);
    }
}
