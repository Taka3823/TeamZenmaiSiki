using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyDataPlate : MonoBehaviour {

    Image image;
    // Use this for initialization
    float t_;
    bool isstart;
    bool isend;
    bool isdelet;
    public bool IsDelet()
    {
        return isdelet;
    }
    public void SetIsEnd(bool end)
    {
        isend= end;
    }
    void Start () {
        image = GetComponent<Image>();
        var color = new Color(1.0f, 1.0f, 1.0f);
        color.a = 0.0f;
        image.color = color;
        isstart = true;
        isend = false;
        isdelet = false;
    }

    // Update is called once per frame
    void Update () {
        if (isstart&&(!isend))
        {
            t_ += 1 / (60.0f * 1.0f);
            if (t_ > 1.0f)
            {
                t_ = 1;
                isstart = false;
            }
        }
        if (isend)
        {
            t_ -= 1 / (60.0f * 1.0f);
            if (t_ <= 0.0f)
            {
                t_ = 0;
                isend = false;
                isdelet = true;
            }
        }
        setAlfa();
	}
    public void set(EnemyData.EnemyInternalDatas data)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Name")
            {
                child.GetComponent<Text>().text = data.name;
            }
            if (child.name == "Age")
            {
                child.GetComponent<Text>().text = data.age.ToString();
            }
            if (child.name == "BloodType")
            {
                child.GetComponent<Text>().text = data.bloodType;
            }
            if (child.name == "FirstMemo")
            {
                child.GetComponent<Text>().text =data.memos[0];
            }
            if (child.name == "SecondMemo")
            {
                child.GetComponent<Text>().text = data.memos[1];
            }
        }
    }

    private void setAlfa()
    {
        var color = image.color;
        color.a = EasingCubicOut(t_,0,1);
        image.color = color;
    }
    public void setAlfapub(float i)
    {
        var color = image.color;
        color.a = i;
        Debug.Log("はいった");
        image.color = color;
    }
    float EasingCubicOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t + 1) + b;
    }
}
