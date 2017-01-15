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
    public int GetLug()
    {
        return lug;
    }
    List<float> easY;
    float startpos;
    float endpos;
    bool iseasing;
    bool startset;
    int lug = 90;
    int lugcount = 0;
    float speed = 1.0f;
    int count;
    float t_;
    public float getSpeed()
    {
        return speed;
    }
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
        AudioManager.Instance.PlaySe("role.wav");
    }
    float culcY(float y)
    {
        if (y <= 192)
            y = 192;
        float max = DataManager.Instance.DirectiveDatas[DataManager.Instance.ScenarioChapterNumber][DataManager.Instance.ScenarioSectionNumber].collectionTargetName.Count * 110 - 193;
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
        if (lugcount <= lug)
        {
            lugcount++;
            return;
        }
       
        t_ += 1 / (60.0f*speed);
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
