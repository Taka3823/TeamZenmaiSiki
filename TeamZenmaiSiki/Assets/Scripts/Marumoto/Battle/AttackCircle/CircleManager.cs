using UnityEngine;
using System.Collections;

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
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
        }

        if (clickCount==0)
        {
            primaryCircle.PrimaryScaling();
        }
        else if (clickCount==1)
        {
            if (!secondaryCircleScript.enabled) secondaryCircleScript.enabled = true;

            secondaryCircleScript.SecondaryRotating();
        }

        else if (clickCount == 2)
        {
            if (!circleCollider2D.enabled)
            {
                //TODO:CircleCollider2Dを有効化する処理
                circleCollider2D.enabled = true;
                PlayerAttackUpdateManager.Instance.SetCircleColliderEnable(true);
                Debug.Log("isActiveCollider");
            }
        }
	}
}
