using UnityEngine;
using System.Collections;

public class ObjToPhaseManager : MonoBehaviour {
    private static ObjToPhaseManager instance;
    public static  ObjToPhaseManager Instance
    {
        get { return instance; }
    }
    private Vector3 enemyPos;
    private bool eraseFlag;
    private bool progressPhaseFlage;

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        enemyPos = new Vector3();
        eraseFlag = false;
        progressPhaseFlage = false;
    }

    public void SetPosition(Vector3 _pos) { enemyPos = _pos; }
    public Vector3 GetPosition() { return enemyPos; }
    public void SetEraseFlag(bool _cond) { eraseFlag = _cond; }
    public bool GetEraseFlag() { return eraseFlag; }
    public void SetProgressPhaseFlag(bool _cond) { progressPhaseFlage = _cond; }
    public bool GetProgressPhaseFlag() { return progressPhaseFlage; }
}
