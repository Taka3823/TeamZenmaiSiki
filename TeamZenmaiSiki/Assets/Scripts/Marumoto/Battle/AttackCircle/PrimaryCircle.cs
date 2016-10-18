using UnityEngine;
using System.Collections;

public class PrimaryCircle : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.5f), Tooltip("円の大きさが変わる速度(0.01~0.5)")]
    float circleScalingSpeed;

    [SerializeField, Range(0.01f, 5.0f), Tooltip("円の最小の大きさ(0.01~5.0)")]
    float minCircleRadius;

    [SerializeField, Range(0.1f, 5.0f), Tooltip("最小の大きさから最大までの差分(0.1~5.0)")]
    float diffCircleRadius;

    private Vector3 primaryCircleScale;
    private float primaryAngle;

    void Start()
    {
        primaryCircleScale = new Vector3(minCircleRadius, minCircleRadius, 1f);
        primaryAngle = 0f;
    }

    public void PrimaryScaling()
    {
        transform.localScale = primaryCircleScale + new Vector3((diffCircleRadius / 2) + (diffCircleRadius / 2) * Mathf.Sin(primaryAngle),
                                                                (diffCircleRadius / 2) + (diffCircleRadius / 2) * Mathf.Sin(primaryAngle),
                                                                0f);
        primaryAngle += circleScalingSpeed;
        if (primaryAngle >= (Mathf.PI * 2))
        {
            primaryAngle = 0f;
        }
    }
}
