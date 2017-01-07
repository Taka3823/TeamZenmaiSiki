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
        int killcount = 0;
        
        //SetDebugDatas();
        directivedata = DataManager.Instance.DirectiveDatas[DataManager.Instance.ScenarioChapterNumber][DataManager.Instance.ScenarioSectionNumber];
        if (DataManager.Instance.IsTargetKilled.Count == 0)
        {
            for(int i = 0; i < directivedata.collectionTargetName.Count; i++)
            {
                //DataManager.Instance.IsTargetKilled.Add(false);
            }
        }
        for (int i = 0;i < directivedata.collectionTargetName.Count; i++)
        {
            //Debug.Log(directivedata.collectionTargetName[i]);
            if (directivedata.collectionTargetName[i] != "")
            {
                GameObject obj = Instantiate(nodePrefab);
                obj.transform.parent = content.transform;
                obj.GetComponent<EnemyNameNode>().setCollection(false, false);//仮
                obj.GetComponent<EnemyNameNode>().setName(directivedata.collectionTargetName[i]);
                for(int k = 0; k < DataManager.Instance.KillNames.Count; k++)
                {
                    if (directivedata.collectionTargetName[i] == DataManager.Instance.KillNames[k])
                    {
                        bool isnone = false;
                        for(int j = 0; j < DataManager.Instance.IsTargetKilled.Count; j++)
                        {
                            if (directivedata.collectionTargetName[i] == DataManager.Instance.IsTargetKilled[j])
                            {
                                isnone = true;
                            }
                            
                        }
                        if (!isnone)
                        {
                            obj.GetComponent<EnemyNameNode>().setColor();
                        }
                    }
                }
                for(int k = 0; k < DataManager.Instance.IsTargetKilled.Count; k++)
                {
                    if (directivedata.collectionTargetName[i] == DataManager.Instance.IsTargetKilled[k])
                    {
                        obj.GetComponent<EnemyNameNode>().setKillEffect((killcount)*60);
                        killcount++;
                    }
                }
            }
           
        }
        DataManager.Instance.IsTargetKilled.Clear();
	}
    void SetDebugDatas()
    {
        directivedata.collectionTargetName.Add("ジョニー・ダインリー");
        directivedata.collectionTargetName.Add("ケイシー・リックマン");
        directivedata.collectionTargetName.Add("マリリン・セルウェイ");
        directivedata.collectionTargetName.Add("ポリー・ポロック");
        directivedata.collectionTargetName.Add("シルヴェスター・ゴールディング");
        directivedata.collectionTargetName.Add("エルシー・クローク");
        directivedata.collectionTargetName.Add("フランクリン・パッカー");
        directivedata.collectionTargetName.Add("セルマ・ブラウン");
    }
}
