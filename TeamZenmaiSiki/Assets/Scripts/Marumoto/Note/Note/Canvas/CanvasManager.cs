using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {
    private static CanvasManager instance;
    public static CanvasManager Instance { get { return instance; } }

    [SerializeField]
    GameObject noteViewBackGround;
    [SerializeField]
    GameObject noteViewClose;
    [SerializeField]
    GameObject noteScrollView;

    [SerializeField]
    RectTransform rect;

    void Awake()
    {
        if (instance == null) { instance = this; }
        rect.transform.SetAsFirstSibling();
    }

    /// <summary>
    /// 手記閲覧を有効化する。
    /// </summary>
    public void NoteViewDisplay()
    {
        Popup(true);
    }

    public void NoteViewHide()
    {
        Popup(false);
    }

    private void Popup(bool _cond)
    {
        noteViewBackGround.SetActive(_cond);
        noteViewClose.SetActive(_cond);
        noteScrollView.SetActive(_cond);
    }
}
