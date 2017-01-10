using UnityEngine;
using System.Collections;

public class SliedPanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    GameObject leftButton;

    [SerializeField]
    GameObject rightButton;

    int count = 0;

    void Start()
    {
        if(count <= 0)
        {
            leftButton.SetActive(false);
        }
    }

    void Update()
    {
        if(count > 0)
        {
            leftButton.SetActive(true);
        }

        if(count < 9)
        {
            rightButton.SetActive(true);
        }
    }

    public void OnClickLeft()
    {
        if (count <= 0)
        {
            return;
        }

        count -= 1;
        panel.transform.position = new Vector3(panel.transform.position.x + 1334, panel.transform.position.y, panel.transform.position.z);

        if(count <= 0)
        {
            leftButton.SetActive(false);
        }
    }

    public void OnClickRight()
    {
        if(count >= 9)
        {
            return;
        }

        count += 1;
        panel.transform.position = new Vector3(panel.transform.position.x - 1334, panel.transform.position.y, panel.transform.position.z);

        if(count >= 9)
        {
            rightButton.SetActive(false);
        }
    }
}
