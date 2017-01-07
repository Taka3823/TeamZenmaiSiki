using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BloodImage : MonoBehaviour {


    private bool iseffect = false;
    private bool isrotate = false;
    Color color = new Color();
    float time = 60 * 0.5f;
    float r_time = 60 * 2;
    float r_t = 0;
    int lug =0;
    int lugcount = 0;
    bool iset = false;
    float r_speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	public void setiseffect(bool _iseffect,int _lug) {
        iseffect = _iseffect;
        isrotate = iseffect;
        lug = _lug;
       
    }
    public bool IsEffect()
    {
        return iseffect;
    }
	// Update is called once per frame
	void Update () {
        if (lugcount >= 1 && iseffect&&(!iset))
        {
            iset = true;
            GameObject parent = transform.parent.gameObject;
            Debug.Log("pos" +parent.transform.localPosition.y);
            ContentManager.Instance.addEaz(parent.transform.localPosition);
        }
        if (lugcount<=lug)
        {
            lugcount++;
            return;
        }

        Rotate();
        ColorChange();


	}
    void Rotate()
    {
        if (!isrotate) return;
        r_t += 1 / (r_time);
        GameObject parent2 = transform.parent.gameObject;
        if (r_t >= 1.0f)
            r_t = 1.0f;
        parent2.transform.rotation = Quaternion.Euler(EasingExpoOut(r_t,0,720), 0, 0);
        if (r_t >= 1.0f)
        {
            isrotate = false;
        }
        //parent2.transform.Rotate(new Vector3(r_speed, 0, 0));
        //r_speed -= 0.1f;
        //if(r_speed)
    }

    void ColorChange()
    {
        if (!iseffect) return;
        color = GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a + (1.0f / time));

        if (color.a >= 0.5f)
        {
            color = new Color(color.r, color.g, color.b, 0.5f);
            iseffect = false;
        }
        GetComponent<Image>().color = color;
    }
    float EasingExpoOut(float t, float b, float e)
    {
        return (t == 1) ? e : (e - b) * (-Mathf.Pow(2, -10 * t) + 1) + b;
    }
}
