using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonReaction : MonoBehaviour
{
    DataInit dataInit = new DataInit();

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

        SceneManager.LoadScene("Scenario");
    }
}
