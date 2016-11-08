using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleButtonScript : MonoBehaviour , ISceneBase
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        Debug.Log("Buttonテスト");
        SceneChange("Battle");
    }

    public void SceneChange(string nextSceneName)
    {

         SceneManager.LoadScene("Battle");


    }

}
