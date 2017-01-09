using UnityEngine;
using System.Collections;

public class FadeTest : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;
    bool baka = false;
    public void FadeOuts()
    {
        baka = false;
        Debug.Log(baka);
        fade.FadeIn(5, () =>
        {
            testdayo();
        });
    }
    public void OnClick()
    {
        fade.FadeIn(1, () =>
        {
            fade.FadeOut(1);
        });
    }
    public void testdayo()
    {
        baka = true;
        Debug.Log(baka);
    }
}
