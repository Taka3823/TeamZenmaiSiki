using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeButtonSprite : MonoBehaviour {
	[SerializeField]
	Sprite spriteByOn;
	[SerializeField]
	Sprite spriteByOff;
	[SerializeField]
	Image image;
	[SerializeField]
	Button button;
	
	void Start()
	{
		StartCoroutine(ChangeButtonSprite());
	}

	IEnumerator ChangeButtonSprite()
	{
		while (true)
		{
			if (button.interactable)
			{
				image.sprite = spriteByOn;
			}
			else
			{
				image.sprite = spriteByOff;
			}
			yield return new WaitForSeconds(0.2f);
		}
	}
}
