using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class NewCamera : MonoBehaviour {

    private static NewCamera instance;
    public float cameraposx;
    private const int WAIT_TIME = 20;
    private float easingtime = 2.0f;
    private bool anothertouch;
    private int waitcount;
    private float movepos;
    private float movestartpos;
    private float easingstartpos;
    private float easingendpos;
    private float easingT;
    bool ismoving;
    private int pullcount;
    private float touchPos;
    private float frontPos;
    private float mapstart;
    private float mapend;
    private bool unitTouchMove;
    private float unit_t;
    private float unitTouchStartPos;
    [SerializeField]
    GameObject backLight;
    public static NewCamera Instance
    {
        get { return instance; }
    }
    public void SetUnitT(float _t)
    {
        unit_t = _t;
    }
    public void SetUnitStartPos(float pos)
    {
        unitTouchStartPos = pos;
    }
    // Use this for initialization
    void Start () {
        instance = this;
        mapstart = 0;
        cameraposx = DataManager.Instance.CameraPos.x;
        waitcount = 0;
        frontPos = cameraposx;
        easingT = 0;
        pullcount = 0;
        anothertouch = false;
        unitTouchMove = false;
        mapend = -2.5f*backLight.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        UnitTouchMove();
        if (TabManager.Instance.Getisblood()) return;
        if (TabManager.Instance.GetIsDisplay()) return;
        if (unitTouchMove) return;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            anothertouch = true;
            ismoving = false;
        }
        if (Input.GetMouseButtonDown(0)&&(!anothertouch))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            movestartpos = cameraposx;
            ismoving = false;
          
            //cameraposx = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
   
        if (Input.GetMouseButton(0)&&(!anothertouch))
        {
            frontPos = cameraposx;
            movepos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - touchPos;
            cameraposx = movestartpos+ movepos;
            if (cameraposx != frontPos)
            {
                ismoving = true;
                waitcount = 0;
                easingT = 0;
            }
            waitcount++;
            if (waitcount > WAIT_TIME)
            {
                waitcount = 0;
                ismoving = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            anothertouch = false;
            if (ismoving)
            {
                easingendpos = 5 * (cameraposx-frontPos) + cameraposx;
                easingstartpos = cameraposx;
                waitcount = 0;
            }
        }
        if (ismoving && !Input.GetMouseButton(0)&&(!anothertouch))
        {
            easingT += 1.0f / (easingtime * 60);
            if (easingendpos==mapstart||easingendpos==mapend)
            {
                cameraposx = EasingBackOut(easingT, easingstartpos, easingendpos);
            }
            else
            {
                cameraposx = EasingCircOut(easingT, easingstartpos, easingendpos);
            }
            if (easingT >= 1.0)
            {
                easingT = 0;
                ismoving = false;
            }
        }
        if (cameraposx >= mapstart)
        {
            reset(mapstart);
        }
        if (cameraposx <= mapend)
        {
            reset(mapend);
        }
    }
    float EasingBackOut(float t, float b, float e)
    {
        float s = 1.70158f;
        t -= 1.0f;
        return (e - b) * (t * t * ((s + 1) * t + s) + 1) + b;
    }
    float EasingQuintOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t * t * t + 1) + b;
    }
    float EasingCubicOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * (t * t * t + 1) + b;
    }
    float EasingCircOut(float t, float b, float e)
    {
        t -= 1.0f;
        return (e - b) * Mathf.Sqrt(1 - t * t) + b;
    }
    void reset(float value)
    {
        cameraposx = value;
        easingT = 0;
        ismoving = false;
    }
    void UnitTouchMove()
    {
        if (SearchManager.Instance.GetisUnitTouch())
        {
            if (unit_t >= 1)
            {
                unit_t = 1;
              
            }
            unitTouchMove = true;
            unit_t += 1 / (60.0f * 1.0f);
            cameraposx = EasingCubicOut(unit_t, unitTouchStartPos, -2*SearchManager.Instance.GetUnitPos());
            if (unit_t >= 1)
            {
                unitTouchMove = false;
                SearchManager.Instance.SetisUnitTouch(false);
            }
        }
    }
}
