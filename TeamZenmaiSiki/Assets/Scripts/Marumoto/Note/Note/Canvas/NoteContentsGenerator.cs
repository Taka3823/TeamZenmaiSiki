using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NoteContentsGenerator : MonoBehaviour {
    [SerializeField]
    GameObject noteContentsVerFlick;

    private GameObject noteSelectContents;
    private GameObject contentButton;
    private GameObject popup;

    private const int MAX_CONTENT_BUTTON_NUM = 6;
    private int currentMessengerIndex = 0;
    private int totalTrailNum = 0;
	private int achieveNum = 0;

    void Start()
    {
		achieveNum = SaveManager.Instance.AllAchieveSpecialNum();
		UIDataSetting();
		GenerateUI();
	}

    /// <summary>
    /// Prefab読み込みと、データ格納。
    /// </summary>
    private void UIDataSetting()
    {
        popup = Resources.Load<GameObject>("Prefabs/Note/UIParts/Popup");
        noteSelectContents = Resources.Load<GameObject>("Prefabs/Note/UIParts/NoteSelectContents");
        contentButton = Resources.Load<GameObject>("Prefabs/Note/UIParts/ContentButton");
    }

    //TODO:コルーチンならもっときれいにかけるんじゃないか(？)
    /// <summary>
    /// UIを生成。
    /// </summary>
    private void GenerateUI()
    {
        foreach (KeyValuePair<string, int> pair in CanvasManager.Instance.PageForeachCharacter)
        {
            totalTrailNum = 0;
            GenerateSelectContent(pair.Key, pair.Value);
            currentMessengerIndex++;
        }
    }

    /// <summary>
    /// 各ページを空の状態で生成。
    /// </summary>
    /// <param name="_pairKey">ディクショナリのキー</param>
    /// <param name="_pairValue">ディクショナリの値</param>
    private void GenerateSelectContent(string _pairKey, int _pairValue)
    {
        int _contentsNum = CanvasManager.Instance.ContentsNum[_pairKey];

        for (int i = 0; i < _pairValue; i++)
        {
            GameObject _refSelectContent = Instantiate(noteSelectContents);
            _refSelectContent.transform.SetParent(noteContentsVerFlick.transform);

            GenerateContentButton(ref _contentsNum, _refSelectContent);
        }
    }

    /// <summary>
    /// コンテンツページの各ボタンを生成。
    /// </summary>
    /// <param name="_contentsNum">キャラクターごとのコンテンツ数</param>
    /// <param name="_refSelectContent">ボタンの親に設定したいオブジェクト</param>
    private void GenerateContentButton(ref int _contentsNum, GameObject _refSelectContent)
    {
        int _trailNum = 0;

        if (_contentsNum < MAX_CONTENT_BUTTON_NUM) { _trailNum = _contentsNum; }
        else { _trailNum = MAX_CONTENT_BUTTON_NUM; }

        for (int j = 0; j < _trailNum; j++)
        {
            GameObject _refContentButton = Instantiate(contentButton);
            _refContentButton.transform.SetParent(_refSelectContent.transform);
            Text _text = _refContentButton.GetComponentInChildren<Text>();
            _text.text = CanvasManager.Instance.NoteDatas[currentMessengerIndex].Title[totalTrailNum];
            Button _button = _refContentButton.GetComponent<Button>();

			int _unlockNum = CanvasManager.Instance.NoteDatas[currentMessengerIndex].UnlockNum[totalTrailNum];
			if (_unlockNum > achieveNum)
			{
				_text.color = new Color(0f, 0f, 0f, 0.1f);
				_button.interactable = false;
			}

            int _cMIndex = currentMessengerIndex;
            int _tTNum = totalTrailNum;
            _button.onClick.AddListener(() => { PopupGenerate(_cMIndex, _tTNum); });

            totalTrailNum++;
        }
        _contentsNum -= _trailNum;
    }

    /// <summary>
    /// ポップアップ生成。
    /// クリック時のコールバック。
    /// </summary>
    /// <param name="_charaIndex">キャラクターの番号</param>
    /// <param name="_noteIndex">キャラクター内の手記の本文インデックス</param>
    public void PopupGenerate(int _charaIndex, int _noteIndex)
    {
        GameObject instantiatePopup = Instantiate(popup);
        instantiatePopup.transform.SetParent(this.transform, false);
        Text text = instantiatePopup.GetComponentInChildren<Text>();
        text.text = "<size=60>" + CanvasManager.Instance.NoteDatas[_charaIndex].Title[_noteIndex] + "</size>\n"
                    + CanvasManager.Instance.NoteDatas[_charaIndex].Message[_noteIndex].Trim('"');
    }
}
