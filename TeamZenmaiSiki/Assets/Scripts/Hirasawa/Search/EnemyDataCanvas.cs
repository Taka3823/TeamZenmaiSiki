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
    List<GameObject> plates;
    EnemyDataPlate enemydataplate;
    void Start () {
        instance = this;
        iscreate = false;
        iscancel = false;
        posList = new List<Vector3>();
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
        SetPosition();
        //deba
        for (int i = 0; i < posList.Count; i++)
        {
            GameObject plateObj = Instantiate(plate, posList[i], Quaternion.Euler(0, 0, 0)) as GameObject;
            plateObj.transform.parent=transform;
            plateObj.GetComponent<EnemyDataPlate>().set(SearchManager.Instance.getSendEnemyDatas()[i]);
        }
        iscreate = true;
    }

    void SetPosition()
    {
        float trancex;
        float center = 667;
        float posy = 350;
        switch (SearchManager.Instance.getSendEnemyDatas().Count)
        {
            case 1:
                trancex = 0;
                posList.Add(new Vector3(0, posy, 0));
                break;
            case 2:
                trancex = 250;
                posList.Add(new Vector3(center-trancex, posy, 0));
                posList.Add(new Vector3(center + trancex,posy, 0));
                break;
            case 3:
                trancex = 450;
                posList.Add(new Vector3(center -trancex, posy, 0));
                posList.Add(new Vector3(center, posy, 0));
                posList.Add(new Vector3(center + trancex, posy, 0));
                break;
        }
    }
    float EasingCubicOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t + 1) + b;
    }
}
