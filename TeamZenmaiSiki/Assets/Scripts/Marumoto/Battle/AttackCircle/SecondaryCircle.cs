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
        colliderIsActive = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerAttackUpdateManager.Instance.SetIsHit(true);
        PlayerAttackUpdateManager.Instance.SetTargetObject(other.gameObject);
    }

    /// <summary>
    /// SecondaryCircleの回転。
    /// </summary>
    public void SecondaryRotating()
    {
        if (!GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        transform.RotateAround(primaryCircle.transform.position, Vector3.forward, angleSpeed);
    }

    /// <summary>
    /// CircleCollider2Dを有効化する。
    /// </summary>
    public void IsActiveCollider()
    {
        colliderIsActive = true;
    }
}
