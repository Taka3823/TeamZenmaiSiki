using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonReaction : MonoBehaviour
{
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

    public void OnClick()
    {
        DataManager.Instance.ScenarioChapterNumber = this.chapterNumber - 1;
        DataManager.Instance.ScenarioSectionNumber = this.sectionNumber - 1;

        SceneManager.LoadScene("Scenario");
    }
}
