using UnityEngine;
using System.Collections;

public class TitleLogoMover : MonoBehaviour
{
    bool isClick = false;

	// Update is called once per frame
	void Update ()
    {
        if(isClick)
        {
            transform.position += new Vector3(0, 1.658f, 0);
        }

        if(transform.localPosition.y >= 450)
        {
            Destroy(this.gameObject);
        }
	}

    public void OnClick()
    {
        isClick = true;
    }
}
