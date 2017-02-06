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

	public bool isPullingTab = false;

	void Awake()
	{
		if (instance == null) { instance = this; }
	}

	void LateUpdate()
	{
		DragBegin();
		DragEnd();
	}

	/// <summary>
	/// どれか一つでもTabが開いているかどうか。
	/// </summary>
	/// <returns></returns>
	public bool IsAnyDisplaying()
	{
		foreach(TabControl _tabControl in tabControls)
		{
			if (_tabControl.isDisplay == true) return true;
		}
		return false;
	}

	/// <summary>
	/// タブを閉じる。
	/// </summary>
	/// <param name="_hit2D"></param>
	void ClosingTab(RaycastHit2D _hit2D)
	{
		const int _null = 3;
		int _tabIndex = SearchTabIndex(_hit2D);
		if (_tabIndex == _null) return;

		tabControls[_tabIndex].OnClickLightIsOut();
	}

	/// <summary>
	/// タブを開く。
	/// </summary>
	/// <param name="_hit2D"></param>
	void DisplayingTab(RaycastHit2D _hit2D)
	{
		const int _null = 3;
		int _tabIndex = SearchTabIndex(_hit2D);
		if (_tabIndex == _null) return;

		tabControls[_tabIndex].ActiveEasing();
	}

	/// <summary>
	/// どのタブにタップしたか。
	/// </summary>
	/// <param name="_hit2D"></param>
	/// <returns></returns>
	int SearchTabIndex(RaycastHit2D _hit2D)
	{
		if (_hit2D.transform.name == "LeftTab") return 0;
		else if (_hit2D.transform.name == "CenterTab") return 1;
		else if (_hit2D.transform.name == "RightTab") return 2;
		else return 3;
	}

	/// <summary>
	/// タップしたポイントにレイキャスト。
	/// </summary>
	/// <returns>当たったらRaycastHit2Dを返す。</returns>
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

	/// <summary>
	/// ドラッグ判定の条件を満たしたかどうか。
	/// </summary>
	/// <returns>満たせばtrue</returns>
	bool IsDrag()
	{
		if (endMousePos.x - beginMousePos.x <= -60.0f) return true;
		return false;
	}

	/// <summary>
	/// 指でタップおよびマウスでPushした瞬間。
	/// </summary>
	void DragBegin()
	{
		if (!IsTappingDown()) return;
		SetupBeginPos();

		RaycastHit2D hit2D = Raycast();
		if (hit2D.collider)
		{
			if (hit2D.transform.tag == "CloseTab")
			{
				ClosingTab(hit2D);
			}
			if (hit2D.transform.tag == "InfoTab")
			{
				hit2D.transform.SetAsLastSibling();
				isPullingTab = true;
			}
		}
	}

	/// <summary>
	/// 指およびマウスをPullした瞬間。
	/// </summary>
	void DragEnd()
	{
		if (!IsTappingUp()) return;
		isPullingTab = false;
		SetupEndPos();

		if (!IsDrag()) return;

		RaycastHit2D hit2D = Raycast();
		if (hit2D.collider)
		{
			if (hit2D.transform.tag == "InfoTab")
			{
				AudioManager.Instance.PlaySe("tab_pull.wav");
				hit2D.transform.SetAsLastSibling();
				DisplayingTab(hit2D);
			}
		}
	}

	/// <summary>
	/// 左クリックかシングルタップされたかどうか。
	/// </summary>
	/// <returns></returns>
	bool IsTappingDown()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonDown(0)) return true;

#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Began) return true;
		}
#endif
		return false;
	}

	/// <summary>
	/// 左クリックが離されたか、タップした指を離されたかどうか。
	/// </summary>
	/// <returns></returns>
	bool IsTappingUp()
	{
#if UNITY_STANDALONE
		if (Input.GetMouseButtonUp(0)) return true;

#elif UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch _touch = Input.GetTouch(0);
			if (_touch.phase == TouchPhase.Ended) return true;
		}
#endif
		return false;
	}

	void SetupBeginPos()
	{
#if UNITY_STANDALONE
		beginMousePos = Input.mousePosition;

#elif UNITY_ANDROID
		Touch _touch = Input.GetTouch(0);
		beginMousePos = _touch.position;
#endif
	}

	void SetupEndPos()
	{
#if UNITY_STANDALONE
		endMousePos = Input.mousePosition;
#elif UNITY_ANDROID
		Touch _touch = Input.GetTouch(0);
		endMousePos = _touch.position;
#endif
	}
}
