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

    void PlayerSetup()
    {
        DataManager.PlayerDatas playerDatas;
		playerDatas = DataManager.Instance.PlayerData;
		//PlayerHP = playerDatas.hp;
		PlayerATK = playerDatas.attack;

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
