using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class NameDecision : MonoBehaviour
{
    Text nameText;

	public void SetNameText (int elemetNum_)
    {
        nameText = GetComponent<Text>();
        
        Debug.Log(DataManager.Instance.TargetName[elemetNum_]);

        nameText.text = DataManager.Instance.TargetName[elemetNum_];
        
        Debug.Log("番号は" + elemetNum_ + "で、名前は" + nameText.text);
    }
}
