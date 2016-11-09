using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    void Start()
    {
    }


    private int Damage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncreaseDamage();
        }
        PlayerRead();
    }

    public void PlayerRead()
    {
        string pass = Application.dataPath + "/Scripts/Mitui/Main/Battle/";

        string[] str = ReadCsvFoundation.ReadCsvData(pass + "Player" + ".csv");
        char[] commaSpliter = { ',' };

        for (int i = 0; i < str.Length; i++)
        {
            string[] str2 = ReadCsvFoundation.DataSeparation(str[i], commaSpliter, 3);

            string LucusStatus = str2[0] + "    " + "HP:" + (int.Parse(str2[1]) - Damage) + "  ATK:" + int.Parse(str2[2]);
            Text textComponent = GetComponent<Text>();
            textComponent.text = LucusStatus;
        }
    }

    public void IncreaseDamage(int _value = 1)
    {
        Damage += _value;
    }
}
