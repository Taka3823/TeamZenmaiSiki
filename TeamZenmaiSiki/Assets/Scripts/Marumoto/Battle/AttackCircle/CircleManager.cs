using UnityEngine;
using UnityEngine.EventSystems;

public class CircleManager : MonoBehaviour
{
    [SerializeField]
    PrimaryCircle primaryCircle;
    [SerializeField]
    GameObject secondaryCircle;

    [SerializeField]
    CircleCollider2D circleCollider2D;

    SecondaryCircle secondaryCircleScript;

    private int clickCount;

	// Use this for initialization
	void Start () {
        clickCount = 0;
        secondaryCircleScript = secondaryCircle.GetComponent<SecondaryCircle>();
        circleCollider2D = secondaryCircle.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        CircleStatusChange();
	}
#if UNITY_EDITOR || UNITY_STANDALONE
    /// <summary>
    /// 左クリックされた回数をカウント。
    /// </summary>
    private void ClickIsLeft()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return; 
            clickCount++;
        }
    }

#else
    /// <summary>
    /// タップされた回数をカウント
    /// </summary>
    private void ClickIsLeft()
    {
        if (Input.touchCount <= 0) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Touch _touch = Input.GetTouch(0);
		if (_touch.phase == TouchPhase.Ended)
		{
			clickCount++;
		}
    }
#endif

    /// <summary>
    /// サークル出現時のクリック回数によって、サークルの状態を段階的に更新。
    /// </summary>
    private void CircleStatusChange()
    {
        ClickIsLeft();

        if (clickCount == 0)
        {
            primaryCircle.PrimaryScaling();
        }
        else if (clickCount == 1)
        {
            if (!secondaryCircleScript.enabled) secondaryCircleScript.enabled = true;

            secondaryCircleScript.SecondaryRotating();
        }
        else if (clickCount == 2)
        {
            if (!circleCollider2D.enabled)
            {
				AudioManager.Instance.PlaySe("tomeru.wav", 0.03f);
				AudioManager.Instance.PlaySe("zyuusei.wav", 0.2f);
                circleCollider2D.enabled = true;
                PlayerAttackUpdateManager.Instance.SetCircleColliderEnable(true);
                PlayerAttackUpdateManager.Instance.SecondaryCirclePos = secondaryCircle.transform.position;
            }
        }
    }
}
