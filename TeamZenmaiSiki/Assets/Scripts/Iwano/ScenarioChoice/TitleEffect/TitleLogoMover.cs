using UnityEngine;
using System.Collections;

public class TitleLogoMover : MonoBehaviour
{
    bool isClick = false;

    [SerializeField, Range(1, 4)]
    float moveSpeed;

	// Update is called once per frame
	void Update ()
    {
        if(isClick)
        {
            transform.localPosition += new Vector3(0, moveSpeed, 0);
        }

        if(transform.localPosition.y >= 700)
        {
            Destroy(this.gameObject);
        }
	}

    public void OnClick()
    {
        isClick = true;
    }
}
