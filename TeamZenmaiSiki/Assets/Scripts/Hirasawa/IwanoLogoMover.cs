using UnityEngine;
using System.Collections;

public class IwanoLogoMover : MonoBehaviour
{
    [SerializeField]
    GameObject titleLogo;

    bool canMove = false;

	void Update ()
    {
        if(canMove)
        {
            transform.position += new Vector3(0,1.4f,0);
        }

        if(transform.localPosition.y >= 400)
        {
            Destroy(this.gameObject);
        }
	}

    public void OnClick()
    {
        canMove = true;
    }

}
