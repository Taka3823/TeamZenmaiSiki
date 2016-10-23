using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;


public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get { return instance; }
    }

	// Use this for initialization
	void Start ()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
