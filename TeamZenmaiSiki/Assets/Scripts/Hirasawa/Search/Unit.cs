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
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //MyCanvas.SetInteractive("Button", true);
            for (int i = 0; i < enemyDatas.Count; i++)
            {
                Debug.Log(enemyDatas[i].name);
            }
            //データをおくる
        }
  
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

    void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.name == "Player")
        {
            MyCanvas.SetInteractive("Button", true);
            //データをおくる
        }
        else {
            Debug.Log("anotherHit");
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            MyCanvas.SetInteractive("Button", false);
        }
        else {
            Debug.Log("anotherHit");
        }
    }
    
    // Update is called once per frame
    void Update () {
      

    }

}
