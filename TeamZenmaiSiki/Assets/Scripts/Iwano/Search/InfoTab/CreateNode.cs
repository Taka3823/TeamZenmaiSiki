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
       //デバッグ用
        DataManager.Instance.TargetName.Add("田中太郎");
        DataManager.Instance.TargetName.Add("砂糖次郎");
        DataManager.Instance.TargetName.Add("里中一郎");
        DataManager.Instance.TargetName.Add("丸太中俊介");
        DataManager.Instance.TargetName.Add("五郎丸五郎");
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
