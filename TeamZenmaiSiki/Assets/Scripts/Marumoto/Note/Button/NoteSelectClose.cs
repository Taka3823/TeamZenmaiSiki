using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSelectClose : MonoBehaviour {
    public void ReturnToScenarioChoice()
    {
		AudioManager.Instance.PlaySe("note_button.wav");
		float _waitTime = 1.5f;
		FadeManager.Instance.FadeInOut(_waitTime - 0.1f, 0.3f);
		Invoke("LoadScenarioChoice", _waitTime);
    }

	private void LoadScenarioChoice()
	{
		SceneManager.LoadScene("ScenarioChoice");
	}
}
