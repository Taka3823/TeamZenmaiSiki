using UnityEngine;
using System.Collections;

public class EnemyDestroy : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackCircle")
        {
            Destroy(this.gameObject);
        }
    }
}
