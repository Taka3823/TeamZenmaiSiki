using UnityEngine;
using System.Collections;

public class NoteViewClose : MonoBehaviour {

    public void NoteViewClosing()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
