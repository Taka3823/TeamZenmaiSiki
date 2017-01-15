using UnityEngine;
using System.Collections.Generic;

public class BattleButton : MonoBehaviour {
	[SerializeField]
	List<TabControl> tabControls;

    public void BattleStart()
    {
		AudioManager.Instance.PlaySe("attack_button.wav");
        TurnManager.Instance.ProgressFunction();
    }

	public void ClosingTab()
	{
		foreach(TabControl _tab in tabControls)
		{
			_tab.OnClickLightIsOut();
		}
	}
}
