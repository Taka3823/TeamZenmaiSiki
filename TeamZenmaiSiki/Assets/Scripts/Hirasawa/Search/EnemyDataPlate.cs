using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyDataPlate : MonoBehaviour {

    [SerializeField]

    // Use this for initialization
    void Start () {
   

    }

    // Update is called once per frame
    void Update () {
	
	}
    public void set(EnemyData.EnemyInternalDatas data)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Name")
            {
                child.GetComponent<Text>().text = "Name: " + data.name;
            }
            if (child.name == "Age")
            {
                child.GetComponent<Text>().text = "age: "+data.age.ToString();
            }
            if (child.name == "BloodType")
            {
                child.GetComponent<Text>().text = "type: " + data.bloodType;
            }
            if (child.name == "FirstMemo")
            {
                child.GetComponent<Text>().text = "memo: "+data.memos[0];
            }
            if (child.name == "SecondMemo")
            {
                child.GetComponent<Text>().text = data.memos[1];
            }
        }


    }
}
