using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour {

    // Use this for initialization
    private bool isViewData;
    private int number;
    private Vector3 startpos;
    private List<EnemyData.EnemyInternalDatas> enemyDatas;
    public List<EnemyData.EnemyInternalDatas> getEnemyDatas()
    {
        return enemyDatas;
    }
    public void setEnemyDatas(List<EnemyData.EnemyInternalDatas> _enemyDatas,Vector3 _startpos)
    {
        enemyDatas = _enemyDatas;
        startpos = _startpos;
    }
    public void SetNumber(int n)
    {
        number = n;
    }
    public int GetNumber()
    {
        return number;
    }
    public bool getIsViewData()
    {
        return isViewData;
    }
    public void setIsViewData(bool _isViewData)
    {
        isViewData = _isViewData;
    }
    void Awake()
    {
        startpos = new Vector3();
    }
    void Start () {
       
    }

   public void OnTouchDown()
    {
        List<EnemyData.EnemyInternalDatas> buf= new List<EnemyData.EnemyInternalDatas>();
        buf = SearchManager.Instance.getSendEnemyDatas();
        if (buf.Count > 1)//初期状態でなければ
        {
            SearchManager.Instance.setEnemyDatas(enemyDatas);
            if (number == SearchManager.Instance.GetEnemyNum())//前と同じものを選択していれば
            {
                //何もしない
            }
            else
            {
                EnemyDataCanvas.Instance.ResetPlate();
                SearchManager.Instance.setEnemyDatas(enemyDatas);
                SearchManager.Instance.setEnemyNUm(number);
                EnemyDataCanvas.Instance.CreatePlate();
            }
        }
        else
        {
            SearchManager.Instance.setEnemyNUm(number);
            SearchManager.Instance.setEnemyDatas(enemyDatas);
            EnemyDataCanvas.Instance.CreatePlate();
        }


        for (int i = 0; i < enemyDatas.Count; i++)
        {
            if (enemyDatas[i].isbattle)
            {
                MyCanvas.SetInteractive("Button", true);
                return;
            }
        }
        MyCanvas.SetInteractive("Button", false);
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = new Vector3();
        Vector3 camerapos = new Vector3();
        camerapos = new Vector3(NewCamera.Instance.cameraposx,0,0);
        pos = startpos + camerapos/2;
        transform.position = pos;

    }

}
