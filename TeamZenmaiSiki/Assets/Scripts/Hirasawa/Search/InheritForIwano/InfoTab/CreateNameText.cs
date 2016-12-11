using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CreateNameText : MonoBehaviour
{
    GameObject textObject;
    [SerializeField]
    DataManager.DirectiveData directivedata;
    void Start()
    {
        TextConfigInit();
        
        NameDecide();
    }

    void NameDecide()
    {
        textObject.GetComponent<Text>().text = DataManager.Instance.TargetName[DataManager.Instance.TargetNumber];

        DataManager.Instance.TargetNumber += 1;
    }

    void TextConfigInit()
    {
        textObject = new GameObject("NameText");

        textObject.transform.parent = this.gameObject.transform;

        textObject.AddComponent<Text>();

        textObject.GetComponent<Text>().transform.position = Vector3.zero;
        textObject.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(400, 100);
        textObject.GetComponent<Text>().font = Font.CreateDynamicFontFromOSFont("Arial",80);
        textObject.GetComponent<Text>().fontSize = 60;
        textObject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        textObject.GetComponent<Text>().color = Color.black;
    }
}
