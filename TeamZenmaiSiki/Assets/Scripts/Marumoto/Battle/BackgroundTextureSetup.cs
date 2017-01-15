using UnityEngine;
using UnityEngine.UI;

public class BackgroundTextureSetup : MonoBehaviour {
	[SerializeField]
	Image image;

	Sprite sprite;

	void Awake()
	{
		sprite = Resources.Load<Sprite>("Sprits/Battle/UIParts/CompleteVersion/BackGround/" + DataManager.Instance.BttleTexturePath);
		image.sprite = sprite;
	}
}
