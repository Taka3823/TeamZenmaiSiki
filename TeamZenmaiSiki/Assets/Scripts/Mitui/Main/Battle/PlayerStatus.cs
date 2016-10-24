using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackCircle")
        {
            Debug.Log("Hit!");
            Destroy(this.gameObject);
        }
    }
}