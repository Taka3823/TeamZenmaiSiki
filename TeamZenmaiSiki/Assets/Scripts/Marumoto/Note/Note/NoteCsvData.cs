using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteCsvData
{

    public class NoteData
    {
        public List<int> index = new List<int>();
        public List<string> messengerName = new List<string>();
        public List<string> title = new List<string>();
        public List<string> message = new List<string>();
        public List<int> unlockNum = new List<int>();
    }

    List<string> noteDataPathes = new List<string>();

    List<NoteData> noteDatas = new List<NoteData>();

    /// <summary>
    /// NoteSceneのCSVファイル読み込み。
    /// </summary>
	public NoteCsvData()
    {
        noteDataPathes = CanvasManager.Instance.GetNoteDataPathes();
        for(int k = 0; k < noteDataPathes.Count; k++)
        {
            CsvRead(k);
        }
    }

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
                        noteData.index.Add(Convert.ToInt32(_separatedData[j]));
                        break;

                    case 2:
                        noteData.messengerName.Add(_separatedData[j]);
                        break;

                    case 3:
                        noteData.title.Add(_separatedData[j]);
                        break;

                    case 4:
                        noteData.message.Add(_separatedData[j]);
                        break;

                    case 5:
                        noteData.unlockNum.Add(Convert.ToInt32(_separatedData[j]));
                        break;
                }
            }
        }
        noteDatas.Add(noteData);
    }

    public List<NoteData> GetNoteDatas() { return noteDatas; }

    private string[] MyCsvRead(string path_)
    {
        //ファイル読み込み
        StreamReader sr = new StreamReader(path_);
        //stringに変換
        string strStream = sr.ReadToEnd();

        //カンマとカンマの間に何もなかったら格納しないことにする設定
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        //行に分ける
        string[] lines = strStream.Split(new string[] { "@last" }, option);
        return lines;
    }
}
