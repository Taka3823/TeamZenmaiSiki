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

    void Start()
    {
        transform.localScale = new Vector3(circleScale, circleScale, 1.0f);
        scaleDiff = new Vector2(primaryCircle.transform.lossyScale.x / 2, 
                                primaryCircle.transform.lossyScale.y / 2);
        transform.position = new Vector3(primaryCircle.transform.position.x, 
                                         primaryCircle.transform.position.y + scaleDiff.y, 
                                         0);
    }

    void Update()
    {

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
        if (!GetComponent<CircleCollider2D>().enabled)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
