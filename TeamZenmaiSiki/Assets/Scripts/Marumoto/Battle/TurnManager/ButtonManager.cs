using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {
    Image image;      
    Button button;    

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
	}

    public void EnableButton()
    {
        image.enabled = true;
        button.enabled = true;
    }

    public void DisableButton()
    {
        image.enabled = false;
        button.enabled = false;
    }

    public void SetInteractable(bool _cond)
    {
        button.interactable = _cond;
    }
}
