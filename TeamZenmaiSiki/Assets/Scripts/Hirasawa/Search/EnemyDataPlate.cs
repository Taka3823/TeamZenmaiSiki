using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyDataPlate : MonoBehaviour {

    Image image;
    // Use this for initialization
    float t_;
    float move_t;
    bool isstart;
    bool isend;
    bool isdelet;
    Vector3 startpos = new Vector3();
    Vector3 endpos = new Vector3();
    public bool IsDelet()
    {
        return isdelet;
    }
    public void SetIsEnd(bool end)
    {
        isend= end;
    }
    public bool IsEnd()
    {
        return isend;
    }
    public void SetEndPosition(Vector3 pos)
    {
        endpos = pos;
    }
    void Start () {
        image = GetComponent<Image>();
        var color = new Color(1.0f, 1.0f, 1.0f);
        color.a = 0.0f;
        image.color = color;
        isstart = true;
        isend = false;
        isdelet = false;
        startpos = transform.position;
        move_t = 0;
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
        EaingPos();
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
    private void EaingPos()
    {
        if (isend) return;
        if (TabManager.Instance.GetIsDisplay())
        {
            move_t += 1 / (60.0f * 0.5f);
            if (move_t > 1)
                move_t = 1;
        }
        else
        {
            move_t -= 1 / (60.0f * 0.5f);
            if (move_t < 0)
                move_t = 0;
        }
        float x = EasingCubicOut(move_t,startpos.x,endpos.x);
        float y = EasingCubicOut(move_t, startpos.y, endpos.y);
        float z = EasingCubicOut(move_t, startpos.z, endpos.z);
        transform.position = new Vector3(x, y, z);
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
