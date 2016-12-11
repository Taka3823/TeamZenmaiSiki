using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SerchSceneChanger : MonoBehaviour , ISceneBase
{
    public void SceneChange(string nextSceneName)
    {
        SceneManager.LoadScene("Battle");
    }
}
