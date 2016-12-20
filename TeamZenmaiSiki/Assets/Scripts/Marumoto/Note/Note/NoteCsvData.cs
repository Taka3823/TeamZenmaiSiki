using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteCsvData
{

    public class NoteData
    {
        public List<int> Index { get; private set; }
        public List<string> MessengerName { get; private set; }
        public List<string> Title { get; private set; }
        public List<string> Message { get; private set; }
        public List<int> UnlockNum { get; private set; }

        public NoteData()
        {
            Index = new List<int>();
            MessengerName = new List<string>();
            Title = new List<string>();
            Message = new List<string>();
            UnlockNum = new List<int>();
        }
    }

    List<string> noteDataPathes = new List<string>();

    List<NoteData> noteDatas = new List<NoteData>();

    public Dictionary<string, int> ContentsNum { get; private set; }

    /// <summary>
    /// NoteSceneのCSVファイル読み込み。
    /// </summary>
	public NoteCsvData()
    {
        ContentsNum = new Dictionary<string, int>();
        noteDataPathes = CanvasManager.Instance.GetNoteDataPathes();
        for(int k = 0; k < noteDataPathes.Count; k++)
        {
            CsvRead(k);
        }
    }

    /// <summary>
    /// 手記データ読み込めるようにCSV読み込みを自作。
    /// </summary>
    /// <param name="_pathIndex">CSVファイルPath</param>
    private void CsvRead(int _pathIndex)
    {
        NoteData noteData = new NoteData();
        string[] _lines = MyCsvRead(noteDataPathes[_pathIndex]);
        int _linesNum = _lines.Length;

        for(int i = 2; i < _linesNum; i++)
        {
            string[] _separatedData = ReadCsvFoundation.DataSeparation(_lines[i], ",".ToCharArray(), 6);

            for (int j = 0; j < _separatedData.Length; j++)
            {
                switch (j)
                {
                    case 0:
                        break;

                    case 1:
                        noteData.Index.Add(Convert.ToInt32(_separatedData[j]));
                        break;

                    case 2:
                        noteData.MessengerName.Add(_separatedData[j]);
                        break;

                    case 3:
                        noteData.Title.Add(_separatedData[j]);
                        break;

                    case 4:
                        noteData.Message.Add(_separatedData[j]);
                        break;

                    case 5:
                        noteData.UnlockNum.Add(Convert.ToInt32(_separatedData[j]));
                        break;
                }
            }
        }

        int result;
        if (!ContentsNum.TryGetValue(noteData.MessengerName[0], out result))
        {
            ContentsNum.Add(noteData.MessengerName[0], _linesNum - 2);
        }

        noteDatas.Add(noteData);
    }

    public List<NoteData> GetNoteDatas() { return noteDatas; }

    private string[] MyCsvRead(string path_)
    {
        //ファイル読み込み
        //StreamReader sr = new StreamReader(path_);
        WWW www = new WWW(path_);
        while (!www.isDone)
        {
            //ファイル読み込み終わるまで空回し。
        }

        //stringに変換
        string strStream = www.text;

        //カンマとカンマの間に何もなかったら格納しないことにする設定
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        //行に分ける
        string[] lines = strStream.Split(new string[] { "@last" }, option);
        return lines;
    }
}
