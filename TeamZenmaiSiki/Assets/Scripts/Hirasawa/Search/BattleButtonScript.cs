using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        Debug.Log("Buttonテスト");
        SceneManager.LoadScene("Battle");
    }
}
