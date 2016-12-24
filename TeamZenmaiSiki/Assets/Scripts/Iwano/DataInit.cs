using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DataInit : MonoBehaviour
{
    public void OnClick()
    {
        Init();
    }


    void Init()
    {
        DataManager.PlayerDatas pD = new DataManager.PlayerDatas();

        pD.hp = DataManager.PlayerDatas.MAX_HP;
        pD.attack = DataManager.PlayerDatas.CONSTANT_ATTACK;

        DataManager.Instance.PlayerData = pD;

        DataManager.Instance.CameraPos = Vector3.zero;
        DataManager.Instance.KillNames = new List<string>();
        DataManager.Instance.IsTargetKilled = new List<string>();
        DataManager.Instance.KillNum = 0;
        DataManager.Instance.IsAchieveBounties = new List<bool>();
        DataManager.Instance.BrakedCoreCount = 0;
        DataManager.Instance.IsUnitDestroys = new List<bool>();

    }
}
