using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CreateNode : MonoBehaviour
{
    //ノードのプレハブ
    [SerializeField]
    GameObject nodePrefab;

    //スライド可能範囲を指定しているオブジェクト
    [SerializeField]
    GameObject content;

    private DataManager.DirectiveData directivedata;
    void Awake()
    {
       
    }

    // Use this for initialization
    void Start ()
    {
        directivedata = new DataManager.DirectiveData();
        directivedata.collectionTargetName = new List<string>();
        SetDebugDatas();
        //directivedata = DataManager.Instance.DirectiveDatas[DataManager.Instance.ScenarioChapterNumber][DataManager.Instance.ScenarioSectionNumber];

        for(int i = 0;i < directivedata.collectionTargetName.Count; i++)
        {
            GameObject obj = Instantiate(nodePrefab);
            obj.transform.parent = content.transform;
            obj.GetComponent<EnemyNameNode>().setCollection(false,false);//仮
            obj.GetComponent<EnemyNameNode>().setName(directivedata.collectionTargetName[i]);
        }
	}
    void SetDebugDatas()
    {
        directivedata.collectionTargetName.Add("ゴウダタケシ");
        directivedata.collectionTargetName.Add("ジョンソン");
        directivedata.collectionTargetName.Add("ゴウダジャイ子");
        directivedata.collectionTargetName.Add("マルモッコリ");
        directivedata.collectionTargetName.Add("ミッチーバスケ");
    }
}
