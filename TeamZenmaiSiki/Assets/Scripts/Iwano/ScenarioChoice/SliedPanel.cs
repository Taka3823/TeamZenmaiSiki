using UnityEngine;
using System.Collections;

public class SliedPanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    public void OnClickLeft()
    {
        panel.transform.position = new Vector3(panel.transform.position.x + 1334, panel.transform.position.y, panel.transform.position.z);
    }

    public void OnClickRight()
    {
        panel.transform.position = new Vector3(panel.transform.position.x - 1334, panel.transform.position.y, panel.transform.position.z);
    }
}
