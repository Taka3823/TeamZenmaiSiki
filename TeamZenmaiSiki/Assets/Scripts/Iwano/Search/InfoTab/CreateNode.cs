using UnityEngine;
using System.Collections;

public class CreateNode : MonoBehaviour
{
    //ノードのプレハブ
    [SerializeField]
    GameObject nodePrefab;

    //スライド可能範囲を指定しているオブジェクト
    [SerializeField]
    GameObject content;

    void Awake()
    {
       
    }

    // Use this for initialization
    void Start ()
    {
        if(DataManager.Instance.TargetNumber == 0)
        {
            DataManager.Instance.TargetNumber = 0;
        }
        
        for(int i = 0;i < DataManager.Instance.TargetName.Count; i++)
        {
            GameObject obj = Instantiate(nodePrefab);
            obj.transform.parent = content.transform;
        }
	}
}
