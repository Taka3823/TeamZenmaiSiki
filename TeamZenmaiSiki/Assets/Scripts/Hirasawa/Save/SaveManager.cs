using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class SaveManager : MonoBehaviour
{

    // Use this for initialization
    private static SaveManager instance;

    public static SaveManager Instance
    {
        get { return instance; }
    }
    public enum ClearType
    {
        UNCLEAR = 0, CLEAR, LOCK
    }
    public struct SaveData
    {
        public int stagename;
        public ClearType cleartype;
        public int destroyNum;
        public int achieveSpecial;
    }
    List<List<SaveData>> savedata;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savedata = new List<List<SaveData>>();
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        AllRead();
        Debug.Log("clear"+GetClearChapterNum());
    }
    public int AllAchieveSpecialNum()
    {
        int num = 0;
        for(int i = 0; i < savedata.Count; i++)
        {
            for(int j = 0; j < savedata[i].Count; j++)
            {
                num += savedata[i][j].achieveSpecial;
            }
        }
        return num;
    }
    public void ScenarioSave(int destroynum, int specialnum)
    {
        SaveData buf = new SaveData();
        int chapter = DataManager.Instance.ScenarioChapterNumber;
        int section = DataManager.Instance.ScenarioSectionNumber;
        buf.stagename = section + 1;
        buf.cleartype = ClearType.CLEAR;
        buf.destroyNum = destroynum;
        buf.achieveSpecial = specialnum;
        Write(chapter, section, buf);
        //Debug.Log("こうんと"+savedata[chapter].Count);
        if ((section+1) != savedata[chapter].Count)//次のセクションが最後でなければ
        {
            //Debug.Log("さいごじゃないだよ");
            if (savedata[chapter][section + 1].cleartype == ClearType.LOCK)
            {
                buf.stagename = section + 2;
                buf.cleartype = ClearType.UNCLEAR;
                buf.destroyNum = 0;
                buf.achieveSpecial = 0;
                Write(chapter, section + 1, buf);
            }

        }
        else
        {
            //Debug.Log("さいごだよ");
            if (chapter == 9 && (section+1) == savedata[chapter].Count) return;
            if (savedata[chapter+1][0].cleartype == ClearType.LOCK)
            {
               
                buf.stagename = 1;
                buf.cleartype = ClearType.UNCLEAR;
                buf.destroyNum = 0;
                buf.achieveSpecial = 0;
                Write(chapter+1, 0, buf);
            }
        }
    }
    public int GetClearChapterNum()
    {
        int max = 10;
        if (savedata[max - 1][0].cleartype != ClearType.LOCK) return (max - 1);
        for (int i = 0; i < max; i++)
        {
            if (savedata[i][savedata[i].Count - 1].cleartype != ClearType.CLEAR)
            {
                return i;
            }
            
        }
        return 0;
    }
    void Write(int chapter,int section, SaveData data)
    {
        string path = Application.dataPath + "/CSVFiles/Save/SaveChapter" + (chapter + 1).ToString() + ".csv";//書き込み聞く
        StreamWriter sw = new StreamWriter(path, false); //true=追記 false=上書き
        sw.WriteLine("セクション数,そのセクションをクリアしたか,倒した敵の数,達成した特別指令数");
        for (int i = 0; i < savedata[chapter].Count; i++)
        {
            if (i == section)
            {
                string names = data.stagename.ToString();
                string cleartypes = ((int)data.cleartype).ToString();
                string killnums = data.destroyNum.ToString();
                string specialnums = data.achieveSpecial.ToString();
                sw.WriteLine(names + "," + cleartypes + "," + killnums + "," + specialnums);
            }
            else
            {
                string names = savedata[chapter][i].stagename.ToString();
                string cleartypes = ((int)savedata[chapter][i].cleartype).ToString();
                string killnums = savedata[chapter][i].destroyNum.ToString();
                string specialnums = savedata[chapter][i].achieveSpecial.ToString();
                sw.WriteLine(names + "," + cleartypes + "," + killnums + "," + specialnums);
            }

        }
        savedata[chapter][section] = data;
        sw.Flush();
        sw.Close();

    }
    void AllRead()
    {
        string path = "file://" + Application.dataPath +
                   "/CSVFiles/Save/SaveChapter";

        char[] commaSpliter = { ',' };

        for (int i = 0; i < 10; i++)
        {
            List<SaveData> bufdatalist = new List<SaveData>();
            string savepath = path + (i + 1).ToString() + ".csv";
            string[] lines = ReadCsvFoundation.ReadCsvData(savepath);
            for (int j = 1; j < lines.Length; j++)
            {
                string[] data = ReadCsvFoundation.DataSeparation(lines[j], commaSpliter, 4);
                SaveData bufdata = new SaveData();
                bufdata.stagename = int.Parse(data[0]);
                bufdata.cleartype = (ClearType)(int.Parse(data[1]));
                bufdata.destroyNum = int.Parse(data[2]);
                bufdata.achieveSpecial = int.Parse(data[3]);
                bufdatalist.Add(bufdata);
            }
            savedata.Add(bufdatalist);
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }

}
