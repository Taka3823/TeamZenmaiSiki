using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour {

    // Use this for initialization
    private bool isViewData;
    private List<EnemyData.EnemyInternalDatas> enemyDatas;
    public List<EnemyData.EnemyInternalDatas> getEnemyDatas()
    {
        return enemyDatas;
    }
    public void setEnemyDatas(List<EnemyData.EnemyInternalDatas> _enemyDatas)
    {
        enemyDatas = _enemyDatas;
    }
    public bool getIsViewData()
    {
        return isViewData;
    }
    public void setIsViewData(bool _isViewData)
    {
        isViewData = _isViewData;
    }
    void Start () {


    }
	public void veiw()
    {
   

    }
   
   public void OnTouchDown()
    {

        SearchManager.Instance.setEnemyDatas(enemyDatas);
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
      

    }

}
