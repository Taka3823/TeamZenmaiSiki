using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {
	[SerializeField]
	GameObject pausePopup;

	private bool isDisplayPopup;

	void Awake()
	{
		isDisplayPopup = false;
	}

	/// <summary>
	/// ポーズボタン(画面左上)押された時の処理
	/// </summary>
	public void PauseButtonPush()
	{
		if (!isDisplayPopup)
		{
			DisplayPopup();
		}
		else
		{
			ClosePopup();
		}
	}

	/// <summary>
	/// ポップアップを開く
	/// </summary>
	private void DisplayPopup()
	{
		Time.timeScale = 0.0f;
		isDisplayPopup = true;
		pausePopup.SetActive(true);
	}

	/// <summary>
	/// ポップアップを閉じる
	/// </summary>
	private void ClosePopup()
	{
		Time.timeScale = 1.0f;
		isDisplayPopup = false;
		pausePopup.SetActive(false);
	}

	/// <summary>
	/// "Back"ボタン押下時
	/// </summary>
	public void BackButtonPush()
	{
		ClosePopup();
	}

	/// <summary>
	/// "BackToTitle"ボタン押下時
	/// </summary>
	public void BackToTitlePush()
	{
		SceneManager.LoadScene("ScenarioChoice");
	}
}
