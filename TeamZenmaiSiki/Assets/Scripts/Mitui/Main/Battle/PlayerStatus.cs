using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public GameObject targetEnemy = null;

    void Start()
    {
    }

    void Update()
    {
        setTargetEnemy();
        attack_LeftClick();
    }

    private void setTargetEnemy()
    {
        // クリックした位置から真っ直ぐ奥に行く光線.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ヒット情報を格納する変数を作成
        RaycastHit hitInfo;
        // カメラから光線を出し、もし何かに当たったら
        if (Physics.Raycast(ray, out hitInfo))
        {
            // その当たったオブジェクトのタグ名が Enemy なら
            if (hitInfo.collider.gameObject.tag == "Enemy")
            {
                // 当たったオブジェクトを、参照。
                targetEnemy = hitInfo.collider.gameObject;
                // ターゲットが見つかったので、処理を抜ける
                return;
            }
        }
        targetEnemy = null;
    }

    private void attack_LeftClick()
    {
        // 左クリックが押されたら
        if (Input.GetMouseButtonDown(0))
        {
            // 的に敵が入っていたら (変数が空じゃないなら)
            if (targetEnemy != null)
            {
                // 敵を消滅させる。
                //Destroy(targetEnemy);
            }
        }
    }
}
