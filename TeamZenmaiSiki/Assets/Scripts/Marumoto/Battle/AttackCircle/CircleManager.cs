using UnityEngine;
using System.Collections;

public class CircleManager : MonoBehaviour
{
    [SerializeField]
    PrimaryCircle primaryCircle;
    [SerializeField]
    SecondaryCircle secondaryCircle;

    private int clickCount;

	// Use this for initialization
	void Start () {
        clickCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
        }

        if (clickCount==0)
        {
            primaryCircle.PrimaryScaling();
        }

        if (clickCount==1)
        {
            if (!secondaryCircle.enabled) secondaryCircle.enabled = true;

            secondaryCircle.SecondaryRotating();
        }

        if (clickCount == 2)
        {
            secondaryCircle.IsActiveCollider();
        }
	}
}
