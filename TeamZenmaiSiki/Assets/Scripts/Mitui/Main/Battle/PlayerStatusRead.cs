using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerStatusRead : MonoBehaviour
{

    [SerializeField]
    Text playerStatusHP;
    [SerializeField]
    Text playerStatusATK;


    private int PlayerHP;
    private int PlayerATK;

    //void PlayerRead()
    //{

    //    string pass = Application.dataPath + "/Scripts/Mitui/Main/Battle/";

    //    string[] str = ReadCsvFoundation.ReadCsvData(pass + "Player" + ".csv");
    //    char[] commaSpliter = { ',' };

    //    for (int i = 0; i < str.Length; i++)
    //    {
    //        string[] str2 = ReadCsvFoundation.DataSeparation(str[i], commaSpliter, 3);

    //        string LucusStatus = str2[0] + "    " + "HP:" + (int.Parse(str2[1]) - Damage) + "  ATK:" + int.Parse(str2[2]);
    //        Text textComponent = GetComponent<Text>();
    //        textComponent.text = LucusStatus;
    //    }
    //}

    void PlayerSetup()
    {
        DataManager.PlayerDatas playerDatas;
        playerDatas.hp = PlayerHP;
        playerDatas.attack = PlayerATK;

        playerStatusHP.text = "HP: " + PlayerHP + "/" + DataManager.PlayerDatas.MAX_HP;
        playerStatusATK.text = "ATK: " + PlayerATK.ToString();
    }
    
    public int getBattlePlayerHp() { return PlayerHP; }
    public int getBattlePlayerAtk() { return PlayerATK; }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //IncreaseDamage();
        }
        PlayerSetup();
    }

}
