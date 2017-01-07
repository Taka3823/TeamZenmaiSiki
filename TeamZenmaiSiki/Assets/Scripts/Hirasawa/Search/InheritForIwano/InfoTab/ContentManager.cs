using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ContentManager : MonoBehaviour {
    private static ContentManager instance;

    public static ContentManager Instance
    {
        get { return instance; }
    }
    public Vector3 pos()
    {
        return transform.localPosition;
    }
    List<float> easY;
    float startpos;
    float endpos;
    bool iseasing;
    bool startset;
    int count;
    float t_;

    public void addEaz(Vector3 pos)
    {
        easY.Add(pos.y);
        iseasing = true;
    }
    void Awake()
    {
        instance = this;
        iseasing = false;
        easY = new List<float>();
        startset = false;
        t_ = 1;
        count = 0;
    }
    // Use this for initialization
    void Start () {
 
	}
	void setEasing(float spos,float epos)
    {
        t_ = 0;
        startpos = spos;
        endpos = epos;
    }
    float culcY(float y)
    {
        if (y <= 250)
            y = 250;
        float max = DataManager.Instance.DirectiveDatas[DataManager.Instance.ScenarioChapterNumber][DataManager.Instance.ScenarioSectionNumber].collectionTargetName.Count * 100 - 350;
        Debug.Log(max);
        if (y >= max)
            y = max;
        return y;
    }
	// Update is called once per frame
	void Update () {
        //Debug.Log("lo" + transform.localPosition);
        if (!iseasing) return;
        if (!startset)
        {
            startset = true;
            Vector3 posi = transform.localPosition;
            posi.y = culcY(-easY[0]);
            transform.localPosition = posi;
        }
        t_ += 1 / (60.0f*1.0f);
        if (t_ >= 1.0f)
        {
            count++;
            if (count >= easY.Count)
            {
                iseasing = false;
                return;
            }
            setEasing(transform.localPosition.y,-easY[count]);
        }
        float posy = EasingCubicOut(t_, startpos, culcY(endpos));
        Vector3 pos = transform.localPosition;
        pos.y = posy;
        transform.localPosition = pos;
    }
    float EasingCubicOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t + 1) + b;
    }

}
