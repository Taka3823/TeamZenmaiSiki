using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class EnemyDataCanvas : MonoBehaviour {

    // Use this for initialization
    private static EnemyDataCanvas instance;

    public static EnemyDataCanvas Instance
    {
        get { return instance; }
    }
    bool iscreate;
    [SerializeField]
    GameObject plate;
    List<Vector3> posList;
    List<Vector3> endposList;
    List<GameObject> plates;
    EnemyDataPlate enemydataplate;
    void Start () {
        instance = this;
        iscreate = false;
        iscancel = false;
        posList = new List<Vector3>();
        endposList = new List<Vector3>();
    }
    bool iscancel;
    public void DestroyPlate()
    {
        foreach (Transform child in transform)
        {

            if (child.GetComponent<EnemyDataPlate>().IsDelet())
            {
                Destroy(child.gameObject);
                SearchManager.Instance.setEnemyDatas(new List<EnemyData.EnemyInternalDatas>());
            }

        }
       
    }
    public void ResetPlate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);

        }
        SearchManager.Instance.setEnemyDatas(new List<EnemyData.EnemyInternalDatas>());
    }
    public void CancelPlate()
    {
        iscancel = true;
        foreach (Transform child in transform)
        {

            child.GetComponent<EnemyDataPlate>().SetIsEnd(true);

        }
    }
    // Update is called once per frame
    void Update () {
        DestroyPlate();
	}
    public void CreatePlate()
    {
        iscancel = false;
        
        if (posList.Count > 0)
        {
            posList.Clear();

        }
        if (endposList.Count > 0)
        {
            endposList.Clear();

        }
        SetPosition();
        Debug.Log(posList.Count);
        //deba
        for (int i = 0; i < posList.Count; i++)
        {
            if (TabManager.Instance.GetIsDisplay())
            {
                Debug.Log("タッチ");
                GameObject plateObj = Instantiate(plate, endposList[i], Quaternion.Euler(0, 0, 0)) as GameObject;
                plateObj.transform.parent = transform;
                plateObj.GetComponent<EnemyDataPlate>().set(SearchManager.Instance.getSendEnemyDatas()[i]);
                plateObj.GetComponent<EnemyDataPlate>().SetEndPosition(endposList[i]);
                string pass = "Sprits/Search/personal_info_plate" + (i+1).ToString();
                Sprite image = new Sprite();
                image = Resources.Load<Sprite>(pass);
                plateObj.GetComponent<Image>().sprite = image;
            }
            else
            {
                Debug.Log("タッチ");
                GameObject plateObj = Instantiate(plate, posList[i], Quaternion.Euler(0, 0, 0)) as GameObject;
                plateObj.transform.parent = transform;
                plateObj.GetComponent<EnemyDataPlate>().set(SearchManager.Instance.getSendEnemyDatas()[i]);
                plateObj.GetComponent<EnemyDataPlate>().SetEndPosition(endposList[i]);
                string pass = "Sprits/Search/personal_info_plate" + (i + 1).ToString();
                Sprite image = new Sprite();
                image = Resources.Load<Sprite>(pass);
                plateObj.GetComponent<Image>().sprite = image;
            }
            
           
        }
        iscreate = true;
    }

    void SetPosition()
    {
        float trancex;
        float center = 667;
        float posy = 350;
        float endposx = 200;
        float transy;
        Debug.Log(SearchManager.Instance.getSendEnemyDatas().Count);
        switch (SearchManager.Instance.getSendEnemyDatas().Count)
        {
            case 1:
                trancex = 0;
                posList.Add(new Vector3(0, posy, 0));
                endposList.Add(new Vector3(0, posy, 0));
                break;
            case 2:
                trancex = 250;
                transy = 100;
                posList.Add(new Vector3(center-trancex, posy, 0));
                posList.Add(new Vector3(center + trancex,posy, 0));
                endposList.Add(new Vector3(endposx, posy+transy, 0));
                endposList.Add(new Vector3(endposx, posy + transy, 0));
                break;
            case 3:
                trancex = 450;
                transy = 200;
                posList.Add(new Vector3(center -trancex, posy, 0));
                posList.Add(new Vector3(center, posy, 0));
                posList.Add(new Vector3(center + trancex, posy, 0));
                endposList.Add(new Vector3(endposx, posy + transy, 0));
                endposList.Add(new Vector3(endposx, posy, 0));
                endposList.Add(new Vector3(endposx, posy - transy, 0));
                break;
        }
    }
    float EasingCubicOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t + 1) + b;
    }
}
