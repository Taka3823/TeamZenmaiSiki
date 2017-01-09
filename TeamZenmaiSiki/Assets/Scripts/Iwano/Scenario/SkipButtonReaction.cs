using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SkipButtonReaction : MonoBehaviour
{
    [SerializeField]
    ScenarioManager scenarioManager;

    public void OnClick()
    {
        AudioManager.Instance.ToFadeOutBGM(1.0f);

        scenarioManager.SceneAJudgment();
    }
}
