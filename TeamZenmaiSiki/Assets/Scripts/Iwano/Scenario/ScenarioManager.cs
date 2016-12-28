using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour,ISceneBase
{
    [SerializeField]
    Text[] uiText;

    [SerializeField]
    Text uiName;

    //読み込んだシナリオデータの格納場所
    ReadScenario.ScenariosData[] scenariosData;

    [SerializeField, Tooltip("1文字の表示にかかる時間")]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    //表示にかかる時間
    float timeUntilDisplay = 0;
    //文字列の表示を開始した時間
    float timeElapsed = 1;
    //現在の要素番号
    int currentLine = 1;
    //表示中の文字数
    int lastUpdateCharacter = -1;

    //表示する文字
    string[] drawSentences;
    //一要素の文章の行番号
    int lineNumber;
    //一要素の文章の最大番号
    int maxSentenceElement;
    //シナリオデータの最大要素番号
    int maxScenariosDataElement;

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }
    
    void Start()
    { 
        ReadScenario.Instance.ReadFile(DataManager.Instance.ScenarioChapterNumber,DataManager.Instance.ScenarioSectionNumber);

        scenariosData = new ReadScenario.ScenariosData[ReadScenario.Instance.ScenariosDatas.Length];
        scenariosData = ReadScenario.Instance.ScenariosDatas;

        maxScenariosDataElement = scenariosData.Length;
   
        SetNextLine(currentLine);
    }

    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            //現在の行のラストまで行っていない状態でクリックするとテキストを更新
            if (lineNumber < maxSentenceElement)
            {
                lineNumber += 1;

                ExcuteSentenceSystem(lineNumber);
            }
            else if (maxSentenceElement <= lineNumber && Input.GetMouseButtonDown(0))
            {
                currentLine += maxSentenceElement + 1;

                if ((currentLine + 1) <= maxScenariosDataElement)
                {
                    SetNextLine(currentLine);
                }
                else
                {
                    SceneChange("Search");
                }
            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }
        
        //クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * drawSentences[lineNumber].Length + 1);


        // 表示文字数が前回の表示文字数と異なるならテキストを更新する
        if (displayCharacterCount != lastUpdateCharacter)
        {
            if(drawSentences[lineNumber].Length < displayCharacterCount)
            {
                displayCharacterCount = drawSentences[lineNumber].Length;
            }

            uiText[lineNumber].text = drawSentences[lineNumber].Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    //第一引数…シナリオデータの要素番号
    void SetNextLine(int currentLineNum_)
    {
        uiName.text = scenariosData[currentLineNum_].drawName;

        for (int i = 0; i < uiText.Length; i++)
        {
            uiText[i].text = null;
        }

        lineNumber = 0;

        if (scenariosData[currentLineNum_].command == ReadScenario.COMMAND_SENTENCE ||
            scenariosData[currentLineNum_].command == ReadScenario.COMMAND_BRANK)
        {
            drawSentences = new string[scenariosData[currentLineNum_].sentences.Count];

            maxSentenceElement = drawSentences.Length - 1;

            ExcuteSentenceSystem(lineNumber);
        }
        else if (scenariosData[currentLineNum_].command == "")
        {
            ExcuteSentenceSystem(lineNumber);
        }
        else if (scenariosData[currentLineNum_].command == ReadScenario.COMMAND_DRAW)
        {
            ExcuteCommandIsDraw(lineNumber);
        }
    }

    //第一引数…文章の行番号
    void ExcuteSentenceSystem(int elementNum_)
    {
        drawSentences[elementNum_] = scenariosData[currentLine].sentences[elementNum_];
        
        if (scenariosData[currentLine].charaSprite[elementNum_] != "" &&
            scenariosData[currentLine].drawCharacterPos != "" &&
            scenariosData[currentLine].charaSprite[elementNum_] != null)
        {
            DrawManager.Instance.DrawCharacter(scenariosData[currentLine].drawCharacterPos, scenariosData[currentLine].charaSprite[elementNum_]);
        }

        if(scenariosData[currentLine].drawCharacterPos != null)
        {
            DrawManager.Instance.DrawBalloon(scenariosData[currentLine].drawCharacterPos);
        }

        if (scenariosData[currentLine].backGround[elementNum_] != "")
        {
            DrawManager.Instance.DrawBackGround(scenariosData[currentLine].backGround[elementNum_]);
        }

        if (scenariosData[currentLine].backGroundBgm[elementNum_] != "")
        {
            AudioManager.Instance.PlayBgm(scenariosData[currentLine].backGroundBgm[elementNum_]);
        }

        if (scenariosData[currentLine].soundEffect[elementNum_] != "")
        {
            AudioManager.Instance.PlaySe(scenariosData[currentLine].soundEffect[elementNum_]);
        }

        timeUntilDisplay = drawSentences[elementNum_].Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        lastUpdateCharacter = -1;
    }

    void ExcuteCommandIsDraw(int elementNum_)
    {
        if (scenariosData[currentLine].charaSprite[elementNum_] != "")
        {
            DrawManager.Instance.DrawCharacter(scenariosData[elementNum_].drawCharacterPos, scenariosData[currentLine].charaSprite[elementNum_]);

            DrawManager.Instance.DrawBalloon(scenariosData[elementNum_].drawCharacterPos);
        }

        if (scenariosData[currentLine].backGround[elementNum_] != "")
        {
            DrawManager.Instance.DrawBackGround(scenariosData[currentLine].backGround[elementNum_]);
        }

        currentLine += 1;

        SetNextLine(currentLine);
    }

    public void SceneChange(string nextSceneName_)
    {
        SceneManager.LoadScene("Search");

        DataManager.Instance.CameraPos = Vector3.zero;

        AudioManager.Instance.ToFadeOutBGM(0.7f);
    }
}
