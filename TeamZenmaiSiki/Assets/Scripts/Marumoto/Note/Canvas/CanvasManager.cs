using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour {
    private static CanvasManager instance;
    public static CanvasManager Instance { get { return instance; } }

    List<string> noteDataPathes = new List<string>();
    NoteCsvData noteCsvData;

    public List<NoteCsvData.NoteData> NoteDatas { get; private set; }
    public Dictionary<string, int> ContentsNum { get; private set; }
    public List<int> ContentsIndex { get; private set; }
    public Dictionary<string, int> PageForeachCharacter { get; private set; }
    public int TotalPageNum { get; private set; }

    public List<string> GetNoteDataPathes() { return noteDataPathes; }

    void Awake()
    {
        if (instance == null) { instance = this; }
        Setup();
        DebugNoteData();
    }

    void Setup()
    {
#if UNITY_STANDALONE
		noteDataPathes.Add("file://" + Application.streamingAssetsPath + "/CSVFiles/Note/note_master1.csv");
#elif UNITY_ANDROID
		noteDataPathes.Add("jar:file://" + Application.dataPath + "!assets" + "/CSVFiles/Note/note_master1.csv");
#endif
		noteCsvData = new NoteCsvData();
        NoteDatas = new List<NoteCsvData.NoteData>();
        ContentsIndex = new List<int>();
        NoteDatas = noteCsvData.GetNoteDatas();
        ContentsNum = noteCsvData.ContentsNum;
        CalcContentsPage();
        foreach (int value in PageForeachCharacter.Values)
        {
            ContentsIndex.Add(value);
        }
    }

    //デバッグ用:CSVデータ読み込みの正確性確認用。
    /// <summary>
    /// CSVデータが正しく読み込まれているかのデバッグ出力関数。
    /// </summary>
    void DebugNoteData()
    {
        for(int k = 0; k < 1; k++)
        {
            int elem = NoteDatas[k].Index.Count;

            for(int i = 0; i < elem; i++)
            {
                Debug.Log(NoteDatas[k].Index[i]);
                Debug.Log(NoteDatas[k].MessengerName[i]);
                Debug.Log(NoteDatas[k].Title[i]);
                Debug.Log(NoteDatas[k].Message[i]);
                Debug.Log(NoteDatas[k].UnlockNum[i]);
            }
        }
    }
    
    /// <summary>
    /// キャラクターごとに割くページ数を計算。
    /// </summary>
    private void CalcContentsPage()
    {
        Dictionary<string, int> _result = new Dictionary<string, int>();
        int _totalPageNum = 0;

        foreach (KeyValuePair<string, int> pair in ContentsNum)
        {
            int _pageNum = 0;

            _pageNum += pair.Value / 6;

            if ((pair.Value % 6) != 0)
            {
                _pageNum += 1;
            }

            _totalPageNum += _pageNum;
            _result.Add(pair.Key, _pageNum);
        }

        TotalPageNum = _totalPageNum;
        PageForeachCharacter = _result;
    }
}
