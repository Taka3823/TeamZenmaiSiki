using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonReaction : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Scenario");
    }
}
