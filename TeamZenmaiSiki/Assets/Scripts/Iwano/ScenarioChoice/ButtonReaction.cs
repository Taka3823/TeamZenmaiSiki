using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonReaction : MonoBehaviour
{
    DataInit dataInit = new DataInit();

    private GameObject fade;

    public GameObject Fade
    {
        set { fade = value; }
    }

    private int chapterNumber;

    public int ChapterNumber
    {
        get { return chapterNumber; }
        set { chapterNumber = value; }
    }

    private int sectionNumber;

    public int SectionNumber
    {
        get { return sectionNumber; }
        set { sectionNumber = value; }
    }

    bool canClick = false;

    public void OnClick()
    {
        if (canClick)
        {
            return;
        }

        dataInit.OnClick();

        canClick = true;

        DataManager.Instance.ScenarioChapterNumber = this.chapterNumber - 1;
        DataManager.Instance.ScenarioSectionNumber = this.sectionNumber;

        fade.SetActive(true);       
        GameObject ch = fade.transform.FindChild("RawImage").gameObject;

        ch.GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(0, 0, 0, 0);
        fade.GetComponent<IwanoFade>().IsFadeIn = true;
        
        SceneChange();
    }

    void SceneChange()
    {
        SceneManager.LoadScene("Scenario");
    }
}
