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
	List<GameObject> tabs;
	[SerializeField]
	List<TabControl> tabControls;

	Vector3 beginMousePos;
	Vector3 endMousePos;

	//二種類のEasing関数を使い分けるための要素番号
	const int GOING_NUM = 0;
	const int RETURN_NUM = 1;

	void Awake()
	{
		if (instance == null) { instance = this; }
	}

	void Update()
	{
		DragBegin();
		DragEnd();
	}

	public bool IsAnyDisplaying()
	{
		foreach(TabControl _tabControl in tabControls)
		{
			if (_tabControl.isDisplay == true) return true;
		}
		return false;
	}

	void ClosingTab()
	{

	}

	void DisplayingTab(RaycastHit2D _hit2D)
	{
		const int _null = 3;
		int _tabIndex = SearchTabIndex(_hit2D);
		if (_tabIndex == _null) return;

		tabControls[_tabIndex].ActiveEasing();
	}

	int SearchTabIndex(RaycastHit2D _hit2D)
	{
		if (_hit2D.transform.name == "LeftTab") return 0;
		else if (_hit2D.transform.name == "CenterTab") return 1;
		else if (_hit2D.transform.name == "RightTab") return 2;
		else return 3;
	}

	RaycastHit2D Raycast()
	{
		Vector2 clickPointWorld = Camera.main.ScreenToWorldPoint(beginMousePos);

		Ray2D ray2D = new Ray2D();
		ray2D.origin = clickPointWorld;
		ray2D.direction = new Vector2(0.0f, 0.0f);

		float maxDistance = 20;

		var result = Physics2D.Raycast(ray2D.origin, ray2D.direction, maxDistance);

		return result;
	}

	bool IsDrag()
	{
		if (endMousePos.x - beginMousePos.x <= -60.0f) return true;
		return false;
	}

	void DragBegin()
	{
		if (!Input.GetMouseButtonDown(0)) return;

		beginMousePos = Input.mousePosition;

		RaycastHit2D hit2D = Raycast();
		if (hit2D.collider)
		{
			if (hit2D.transform.tag == "InfoTab")
			{
				hit2D.transform.SetAsLastSibling();
			}
		}
	}

	void DragEnd()
	{
		if (!Input.GetMouseButtonUp(0)) return;
		endMousePos = Input.mousePosition;
		if (!IsDrag()) return;

		RaycastHit2D hit2D = Raycast();
		if (hit2D.collider)
		{
			if (hit2D.transform.tag == "InfoTab")
			{
				hit2D.transform.SetAsLastSibling();
				DisplayingTab(hit2D);
			}
		}
	}
}
