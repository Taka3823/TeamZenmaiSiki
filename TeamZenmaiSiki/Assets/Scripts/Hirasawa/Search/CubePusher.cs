using UnityEngine;
using System.Collections;

public class CubePusher : MonoBehaviour
{

    [SerializeField]
    GameObject cubeTest;

    // Use this for initialization
    void Start()
    {
        string[] str = ReadCsvFoundation.ReadCsvData(Application.dataPath + "/CSVFiles/Search/Episode1/Stage1/Unit1.csv");
        char[] commaSpliter = { ',' };
        string[] str2 = ReadCsvFoundation.DataSeparation(str[0],commaSpliter,3);
        
        //GameObject obj = Instantiate(cubeTest, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
