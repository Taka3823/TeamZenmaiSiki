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

    int screenWidth = Screen.width;

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

        if (count < SaveManager.Instance.GetClearChapterNum() + 1 &&
            count >= 9)
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
        panel.transform.position = new Vector3(panel.transform.position.x + screenWidth,
                                               panel.transform.position.y,
                                               panel.transform.position.z);

        if(count <= 0)
        {
            leftButton.SetActive(false);
        }
    }

    public void OnClickRight()
    {
        if(count >= SaveManager.Instance.GetClearChapterNum() + 1 &&
           count >= 9)
        {
            return;
        }

        count += 1;
        panel.transform.position = new Vector3(panel.transform.position.x - screenWidth, 
                                               panel.transform.position.y, 
                                               panel.transform.position.z);

        if(count >= SaveManager.Instance.GetClearChapterNum() + 1 &&
           count >= 9)
        {
            rightButton.SetActive(false);
        }
    }
}
