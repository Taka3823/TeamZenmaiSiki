using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
    //最大表示行数
    const int MAX_NUMBER_OF_LINES = 3;
    //CSVから読み込んだデータ
    ReadScenario.ScenariosData[] scenariosData;
    //表示する文字列
    string[] drawSentences = new string[3];
    
    //背景画像
    //[SerializeField]
    //Image backGround;

    ////吹き出し画像
    //[SerializeField]
    //Image balloon;

    ////キャラクターの名前
    //[SerializeField]
    //Text uiName;

    //描画する文章
    [SerializeField]
    Text[] uiText = new Text[3];

    // 1文字の表示にかかる時間
    //[SerializeField, Range(0.001f, 0.3f)]
    //float intervalForCharacterDisplay = 0.05f;

    //// 現在の文字列
    //private string currentText = string.Empty;
    //// 表示にかかる時間
    //private float timeUntilDisplay = 0;
    //// 文字列の表示を開始した時間
    //private float timeElapsed = 1;
    //現在の行番号
    private int currentLine = 0;
    ////表示中の文字数
    //private int lastUpdateCharacter = -1;

    //// 文字の表示が完了しているかどうか
    //public bool IsCompleteDisplayText
    //{
    //    get { return Time.time > timeElapsed + timeUntilDisplay; }
    //}

    //何番目のシナリオデータか
    int scenariosDataElement = 0;
    //シナリオデータの最後の要素番号
    int maxScenariosDataElement;

    //何番目の文章データか
    int sentenceElement = 0;
    //文章データの最後の要素番号
    int maxsentenceElement;

    void Awake()
    {
        //TIPS:本来はこちらを使用する。
        //ReadFile(DataManager.Instance.ScenarioDictionaryNumber);
        //現在はデバッグ用のためこうしている
        ReadScenario.Instance.ReadFile(0);

        scenariosData = ReadScenario.Instance.ScenariosDatas;
        maxScenariosDataElement = scenariosData.Length;


       // for(int i= 0;i < scenariosData[])
        {

        }

        /**
        for (int i = 0; i < scenariosData.Length; i++)
        {
            BindingOfSentence(i);
        }
        /**/
    }

    void Start()
    {
        //SetNextLine();

        //drawSentences = scenariosData[scenariosDataElement].sentences[];

        TextUpdate();
    }

    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        //if (IsCompleteDisplayText)
        //{
        //    // 現在の行番号がラストまで行ってない状態でクリックすると、テキストを更新する
        //    // if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
        //    //if (currentLine < scenariosData[] && Input.GetMouseButtonDown(0))
        //    {
        //        SetNextLine();
        //    }
        //}
        //else
        //{
        //    // 完了してないなら文字をすべて表示する
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        timeUntilDisplay = 0;
        //    }
        //}

        // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
        //int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

        //// 表示文字数が前回の表示文字数と異なるならテキストを更新する
        //if (displayCharacterCount != lastUpdateCharacter)
        //{
        //    uiText.text = currentText.Substring(0, displayCharacterCount);
        //    lastUpdateCharacter = displayCharacterCount;
        //}

        if(currentLine < drawSentences.Length && Input.GetMouseButtonDown(0))
        {
            TextUpdate();
        }

    }

    void TextUpdate()
    {
        //現在の行のテキストをuiTextに流し込み、現在の行番号を一つ追加する
        //uiText.text = drawSentences[currentLine];
        currentLine++;

        //最大行数を超えていたら
        if (currentLine > MAX_NUMBER_OF_LINES)
        {
            currentLine = 0;
            //drawSentences = 
        }
    }

    //void SetNextLine()
    //{
    //    //（現在の文字列の更新）
    //    //表示する文字列の更新
    //   // currentText = scenariosData[currentLine].sentences[]

    //    // 想定表示時間と現在の時刻をキャッシュ
    //    timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
    //    //文字列の表示を開始した時間を今の時間に再設定する
    //    timeElapsed = Time.time;
    //    //行番号を１つ追加する
    //    currentLine++;
    //    // 文字カウントを初期化
    //    lastUpdateCharacter = -1;
    //}

    //三行の文章を結合する
    /**
    void BindingOfSentence(int i)
    {
        if (scenariosData[i].sentences.Count == 1)
        {
            drawSentences[i] = scenariosData[i].sentences[0];
        }
        else if (scenariosData[i].sentences.Count == 2)
        {
            CharacterBond(i, scenariosData[i].sentences[0], scenariosData[i].sentences[1]);
        }
        else if (scenariosData[i].sentences.Count == 3)
        {
            CharacterBond(i, scenariosData[i].sentences[0], scenariosData[i].sentences[1], scenariosData[i].sentences[2]);
        }
        else if(scenariosData[i].sentences.Count == 0)
        {
            return;
        }
        else
        {
            Debug.LogError("イレギュラーが発生しました");
        }

        scenariosData[i].sentences.Clear();
    }
    /**/

    //文字列の結合
    /**
    void CharacterBond(int elementNum_, string sentence_1, string sentence_2, string sentence_3 = "")
    {
        if (sentence_3 != "")
        {
            drawSentences[elementNum_] = sentence_1 + sentence_2;
        }
        else
        {
            drawSentences[elementNum_] = sentence_1 + sentence_2 + sentence_3;
        }
    }
    /**/

    void CommandCheck(int element_)
    {
        if(scenariosData[element_].command == ReadScenario.COMMAND_SENTENCE)
        {

        }
       // else if(scenariosData[element_])
        {

        }

    }

}

