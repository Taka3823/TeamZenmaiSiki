using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    //BGMがフェードする時間のベース
    private const float BGM_FADE_TIME_FAST = 0.9f;
    private const float BGM_FADE_TIME_SLOW = 0.3f;

    //BGMがフェードする時間の変数
    private float bgmFadeSpeed = BGM_FADE_TIME_FAST;
    //BGMをフェードアウト中かどうか
    private bool isFadeOut = false;

    //次流すBGM名とSE名
    private string nextBgmName;
    private string nextSeName;

    //BGM用とSE用ののAudioSourceを準備
    private AudioSource bgmAudio;
    private AudioSource seAudio;

    //※カメラについているのでコメントアウト
    //リスナーもセットする
    //private AudioListener listener;

    //デフォルトの音量を保存する変数
    private float bgmVolume;

    //全BGMを所持
    private Dictionary<string, AudioClip> bgmDictionary;
    //全SEを所持
    private Dictionary<string, AudioClip> seDictionary;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        bgmAudio = gameObject.AddComponent<AudioSource>();
        seAudio  = gameObject.AddComponent<AudioSource>();

        //listener = gameObject.AddComponent<AudioListener>();

        bgmDictionary = new Dictionary<string, AudioClip>();
        seDictionary  = new Dictionary<string, AudioClip>();

        var bgmList = Resources.LoadAll<AudioClip>("Audios/BGM");
        var seList  = Resources.LoadAll<AudioClip>("Audios/SE");

        foreach(AudioClip bgm in bgmList)
        {
            bgmDictionary.Add(bgm.name + ".ogg", bgm);
        }

        foreach(AudioClip se in seList)
        {
            seDictionary.Add(se.name + ".wav", se);
        }
    }

    void Start()
    {
        //BGMとSEの音量初期値を設定
        bgmVolume = bgmAudio.volume;
    }

    //******************************************** 
    //         SE
    //********************************************

    //第一引数…SEの名前
    //第二引数…指定した秒数分再生まで遅延する
    public void PlaySe(string seName_, float delay = 0.0f)
    {
        //nullチェック
        if (!seDictionary.ContainsKey(seName_))
        {
            Debug.Log(seName_ + "というSEがありません" );
            return;
        }
        //再生するSEを指定
        nextSeName = seName_;
        //指定時間ディレイをかけて再生
        Invoke("DelayPlaySe", delay);
    }

    //SEを再生
    private void DelayPlaySe()
    {
        seAudio.PlayOneShot(seDictionary[nextSeName] as AudioClip);
    }

    //******************************************** 
    //         BGM
    //********************************************

    //第一引数…流したいBGMの名前
    //第二引数…指定した割合でフェードアウトするスピードを変える
    public void PlayBgm(string bgmName_,float fadeOutSpeed_ = BGM_FADE_TIME_FAST)
    {
        //nullチェック
        if(!bgmDictionary.ContainsKey(bgmName_))
        {
            Debug.Log(bgmName_ + "というBGMがありません");
            return;
        }

        //現在BGMが流れていなければそのまま流す
        //流れている場合は、その前のBGMをフェードアウトさせてから流す
        //同じBGMなら何もしない
        if(!bgmAudio.isPlaying)
        {
            nextBgmName = "";
            bgmAudio.clip = bgmDictionary[bgmName_] as AudioClip;
            bgmAudio.Play();
        }
        else if(bgmAudio.clip.name != bgmName_)
        {
            nextBgmName = bgmName_;
            ToFadeOutBGM(fadeOutSpeed_);
        }
    }

    //BGMの再生をフェードアウトさせながら止める。
    //第一引数がBGM_FADE_TIME_FASTだと、早いフェード
    public void ToFadeOutBGM(float fadeOutSpeed_ = BGM_FADE_TIME_SLOW)
    {
        bgmFadeSpeed = fadeOutSpeed_;
        isFadeOut = true;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if(!isFadeOut)
        {
            return;
        }

        bgmAudio.volume -= Time.deltaTime * bgmFadeSpeed; 

        if(bgmAudio.volume <= 0)
        {
            bgmAudio.Stop();
            bgmAudio.volume = bgmVolume;
            isFadeOut = false;

            if(!string.IsNullOrEmpty(nextBgmName))
            {
                PlayBgm(nextBgmName);
            }
        }
	}
}
