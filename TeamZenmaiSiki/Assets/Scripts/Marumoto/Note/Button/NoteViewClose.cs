using UnityEngine;
using System.Collections;

public class NoteViewClose : MonoBehaviour {

    public void NoteViewClosing()
    {
        CanvasManager.Instance.NoteViewHide();
    }
}
