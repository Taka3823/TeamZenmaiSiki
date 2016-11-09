using UnityEngine;
using System.Collections;

public class BattleButton : MonoBehaviour {
    public void BattleStart()
    {
        TurnManager.Instance.ProgressFunction();
    }
}
