using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SkipButtonReaction : MonoBehaviour,ISceneBase
{
    public void OnClick()
    {
        AudioManager.Instance.ToFadeOutBGM(1.0f);
        SceneChange("Search");
    }

    public void SceneChange(string nextSceneName_)
    {
        SceneManager.LoadScene("Search");
    }
}
