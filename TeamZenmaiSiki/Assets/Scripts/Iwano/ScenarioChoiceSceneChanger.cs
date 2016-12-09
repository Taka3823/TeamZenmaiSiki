using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenarioChoiceSceneChanger : MonoBehaviour , ISceneBase
{   
    //ここでは、引数に「Note」(手記)「Scenario」のどちらか
	public void SceneChange(string nextSceneName_)
    {
        SceneManager.LoadScene(nextSceneName_);
    }

   
        //SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        //SceneManager.UnloadScene("Battle");
}