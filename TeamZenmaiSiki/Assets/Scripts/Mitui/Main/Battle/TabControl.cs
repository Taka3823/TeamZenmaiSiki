using UnityEngine;
using System.Collections;

public class TabControl : MonoBehaviour
{

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    public GameObject target = null;

    Direction direction = new Direction();

    public enum Direction
    {
        right,
        left,
        touch
    }

    // ターゲットの情報取得
    public void setTarget()
    {
        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(tapPoint);

        // 何かのColliderに当たってる
        if (collider != null)
        {
            Debug.Log("hit");

            // そのColliderのタグがInfoTabであるなら処理
            if (collider.gameObject.name == "InfoTab")
            {
                //GameObject obj = collider.transform.gameObject;
                //Debug.Log("Info");
                //obj.transform.position= new Vector3(transform.localPosition.x-2.0f, transform.localPosition.y, 0);
            }
        }
    }


    // フリック操作
    void Flick()
    {
        // タッチ開始位置
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            setTarget();
        }
        // 指を放した位置
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
    }

    // フリックの方向
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            // 右フリック
            if (30 < directionX)
            {
                direction = Direction.right;
                Debug.Log("right");

            }
            // 左フリック
            else if (-30 > directionX)
            {
                direction = Direction.left;
                Debug.Log("left");

            }
        }
        else //タッチ
        {
            direction = Direction.touch;
            Debug.Log("touch");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flick();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        }
        switch (direction)
        {
            case Direction.right:
                break;

            case Direction.left:
                
                break;

            case Direction.touch:
                break;
        }

    }
}
