using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NoteSelectClose : MonoBehaviour {
    public void ReturnToScenarioChoice()
    {
        SceneManager.LoadScene("ScenarioChoice");
    }
}
