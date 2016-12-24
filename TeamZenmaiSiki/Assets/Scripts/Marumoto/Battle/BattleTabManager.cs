using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleTabManager : MonoBehaviour {
	private static BattleTabManager instance;
	public static BattleTabManager Instance
	{
		get { return instance; }
	}

	[SerializeField]
	List<TabControl> tabControles;

	void Awake()
	{
		if (instance == null) { instance = this; }
	}

	public bool IsAnyDisplaying()
	{
		foreach(TabControl _tabControl in tabControles)
		{
			if (_tabControl.isDisplay == true) return true;
		}
		return false;
	}
}
