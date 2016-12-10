using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {
    private static MapManager instance;

    public static MapManager Instance
    {
        get { return instance; }
    }
    public enum MapType
    {
        MAPTYPE_INDEX=0,TOWNLIGHT,SMOG,TOWN,TOWER,SKY,MAPTYPEMAX
    }
    public string GetMapTextureType(MapType type)
    {
        return mapTextureType[(int)type];
    }
    [SerializeField]
    GameObject mapTownLight;
    [SerializeField]
    GameObject mapSmog;
    [SerializeField]
    GameObject mapTown;
    [SerializeField]
    GameObject mapTower;
    [SerializeField]
    GameObject mapSky;
    private string[] mapTextureType;
	// Use this for initialization
	void Start () {
        instance = this;
        mapTextureType = new string[(int)MapType.MAPTYPEMAX];
        mapTextureType = ReadMapSetData(ReadMapIndex(1,1));//ここで変えるよ
        setSprites(mapTownLight,MapType.TOWNLIGHT);
        setSprites(mapTown, MapType.TOWN);
        setSprites(mapSky, MapType.SKY);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    string ReadMapIndex(int episode,int stage)
    {
        string readnum = episode.ToString() + "_" + stage.ToString();

        string pass = Application.dataPath +
          "/CSVFiles/Search/Map/MapIndex.csv";

        string[] str = ReadCsvFoundation.ReadCsvData(pass);
        char[] commaSpliter = { ',' };
        for(int i = 1; i < str.Length; i++)
        {
            string[] str2 = ReadCsvFoundation.NotOptionDataSeparation(str[i],commaSpliter, 2);
           
            if (str2[0] == readnum)
            {
                Debug.Log(str2[1]);
                return str2[1];
            }
        }
        Debug.Log("エラーです:マップタイプ");
        return "1_1";
    }
    string[] ReadMapSetData(string mapType)
    {
        string pass = Application.dataPath +
          "/CSVFiles/Search/Map/MapType.csv";

        string[] str = ReadCsvFoundation.ReadCsvData(pass);
        char[] commaSpliter = { ',' };
        for (int i = 1; i < str.Length; i++)
        {
            string[] str2 = ReadCsvFoundation.NotOptionDataSeparation(str[i], commaSpliter, (int)MapType.MAPTYPEMAX);
            if (str2[0] == mapType)
            {
                return str2;
            }
        }
        Debug.Log("エラーです:マップセット");
        string[] debug=new string[6];
        return debug;
    }
    void setSprites(GameObject map,MapType type)
    {
        string pass = "Sprits/Search/BackGround/" + GetMapTextureType(type);
        Debug.Log(GetMapTextureType(type));
        Sprite image = new Sprite();
        image = Resources.Load<Sprite>(pass);
        map.GetComponent<SpriteRenderer>().sprite = image;
    }
}
