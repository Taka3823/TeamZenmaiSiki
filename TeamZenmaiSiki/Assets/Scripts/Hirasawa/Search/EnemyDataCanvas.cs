using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    void Start () {
        instance = this;
        iscreate = false;
        posList = new List<Vector3>();
	}
    public void DestroyPlate()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
    public void CreatePlate()
    {
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
    //public void DestroyPlate()
    //{
    //    foreach (Transform n in plate.transform)
    //    {
    //        GameObject.Destroy(n.gameObject);
    //    }
    //}
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
}
