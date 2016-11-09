using UnityEngine;
using System.Collections;

public class SecondaryCircle : MonoBehaviour {
    [SerializeField]
    GameObject primaryCircle;

    [SerializeField, Range(0.1f, 10.0f), Tooltip("円の回転速度(0.1~10.0)")]
    float angleSpeed;

    [SerializeField, Range(0.01f, 2.0f), Tooltip("円の大きさ(0.01~2.0)")]
    float circleScale;

    private Vector2 scaleDiff;
    private bool isHit;
    public bool colliderIsActive;
    private GameObject deleteObject;

    void Start()
    {
        transform.localScale = new Vector3(circleScale, circleScale, 1.0f);
        scaleDiff = new Vector2(primaryCircle.transform.lossyScale.x / 2, 
                                primaryCircle.transform.lossyScale.y / 2);
        transform.position = new Vector3(primaryCircle.transform.position.x, 
                                         primaryCircle.transform.position.y + scaleDiff.y, 
                                         0);
        isHit = false;
        colliderIsActive = false;
    }

    void Update()
    {
        //HitSequence();
        //Debug.Log(GetComponent<CircleCollider2D>().enabled);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SecondaryCircle Hit!!");
        PlayerAttackUpdateManager.Instance.SetIsHit(true);
        PlayerAttackUpdateManager.Instance.SetTargetObject(other.gameObject);
        //ObjToPhaseManager.Instance.SetPosition(this.gameObject.transform.root.gameObject.transform.position);
        //ObjToPhaseManager.Instance.SetEraseFlag(true);
        //ObjToPhaseManager.Instance.SetProgressPhaseFlag(true);
        //Destroy(other.gameObject);
        //Destroy(this.gameObject.transform.root.gameObject);
    }

    public void SecondaryRotating()
    {
        if (!GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        transform.RotateAround(primaryCircle.transform.position, Vector3.forward, angleSpeed);
    }

    public void IsActiveCollider()
    {
        colliderIsActive = true;
    }

    private void HitSequence()
    {
        if (GetComponent<CircleCollider2D>().enabled)
        {
            if(!isHit)
            {
                Debug.Log("Non-Hit");
                //ObjToPhaseManager.Instance.SetProgressPhaseFlag(true);
                //Destroy(this.gameObject.transform.root.gameObject);
            }
            else
            {
                Debug.Log("isHit");
                //ObjToPhaseManager.Instance.SetPosition(this.gameObject.transform.root.gameObject.transform.position);
                //ObjToPhaseManager.Instance.SetEraseFlag(true);
                //ObjToPhaseManager.Instance.SetProgressPhaseFlag(true);
                //Destroy(deleteObject);
               // Destroy(this.gameObject.transform.root.gameObject);
                //isHit = false;
            }
            colliderIsActive = false;
        }
    }

    public void SetColliderEnabled(bool _cond)
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = _cond;
        Debug.Log(this.gameObject.GetComponent<CircleCollider2D>().enabled);
    }
}
