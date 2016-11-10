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

    /// <summary>
    /// ボタンのオブジェクトを有効化。
    /// </summary>
    public void EnableButton()
    {
        image.enabled = true;
        button.enabled = true;
    }

    /// <summary>
    /// ボタンのオブジェクトを無効化。
    /// </summary>
    public void DisableButton()
    {
        image.enabled = false;
        button.enabled = false;
    }

    /// <summary>
    /// ボタンのInteractableを変更。
    /// </summary>
    /// <param name="_cond">"true"で有効化,"false"で無効化。</param>
    public void SetInteractable(bool _cond)
    {
        button.interactable = _cond;
    }
}
