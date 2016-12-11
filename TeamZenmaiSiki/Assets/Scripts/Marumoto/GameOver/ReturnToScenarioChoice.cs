using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToScenarioChoice : MonoBehaviour {

	public void ReturnScenarioChoice()
    {
        SceneManager.LoadScene("ScenarioChoice");
    }
}
