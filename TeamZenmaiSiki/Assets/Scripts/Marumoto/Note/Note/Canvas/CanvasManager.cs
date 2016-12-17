using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    List<string> noteDataPathes = new List<string>();
    NoteCsvData noteCsvData;
    public List<NoteCsvData.NoteData> noteDatas { get; private set; }

    void Awake()
    {
        if (instance == null) { instance = this; }
        rect.transform.SetAsFirstSibling();
        noteDataPathes.Add(Application.dataPath + "/CSVFiles/Note/note.csv");

        noteCsvData = new NoteCsvData();
        noteDatas = new List<NoteCsvData.NoteData>();
        noteDatas = noteCsvData.GetNoteDatas();

        DebugNoteData();
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

    public List<string> GetNoteDataPathes() { return noteDataPathes; }

    void DebugNoteData()
    {
        for(int k = 0; k < 1; k++)
        {
            int elem = noteDatas[k].index.Count;

            for(int i = 0; i < elem; i++)
            {
                Debug.Log(noteDatas[k].index[i]);
                Debug.Log(noteDatas[k].messengerName[i]);
                Debug.Log(noteDatas[k].title[i]);
                Debug.Log(noteDatas[k].message[i]);
                Debug.Log(noteDatas[k].unlockNum[i]);
            }
        }
    }
}
