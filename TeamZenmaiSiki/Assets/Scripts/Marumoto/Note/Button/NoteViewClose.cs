using UnityEngine;
using System.Collections;

public class NoteViewClose : MonoBehaviour {

    public void NoteViewClosing()
    {
		AudioManager.Instance.PlaySe("note_button.wav");
        Destroy(this.transform.parent.gameObject);
    }
}
