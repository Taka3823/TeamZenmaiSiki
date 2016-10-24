using UnityEngine;
using System.Collections;

public class ManagerLorder : MonoBehaviour
{
    //ゲームを起動すると勝手に呼ばれる関数。呼ばれるタイミングはAwakeよりも早い
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void DataManagerAutoLoader()
    {
        //データマネージャーオブジェクトの生成
        GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Managers/DataManager"));
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AudioManagerAutoLoader()
    {
        //オーディオマネージャーオブジェクトの生成
        GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Managers/AudioManager"));
    }
}
