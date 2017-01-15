using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class MoveNote : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Note");
    }
}
