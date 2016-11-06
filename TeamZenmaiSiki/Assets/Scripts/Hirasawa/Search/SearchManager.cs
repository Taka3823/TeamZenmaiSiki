using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using System.Collections.Generic;
public class SearchManager : MonoBehaviour {

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
        batlleDataList = buf;
        for(int j = 0; j < batlleDataList.Count; j++)
        {
            Debug.Log(batlleDataList[j].name);
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
    void OnTouchDown()
    {

        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {

                GameObject obj = hit.collider.gameObject;

                if (hit.collider.gameObject.tag == "Unit")
                {
                    //Debug.Log("Unit");
                    obj.GetComponent<Unit>().OnTouchDown();
                }
                else if (hit.collider.tag == "Untagged")
                {
                    //Debug.Log("よくわからないもの");
                    MyCanvas.SetInteractive("Button", false);
                }

            }
            else
            {
                //Debug.Log("はずれ");
                MyCanvas.SetInteractive("Button", false);
            }
        }

    }
}
