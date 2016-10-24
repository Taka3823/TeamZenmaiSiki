using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    // Use this for initialization
    private bool isViewData;
    public bool getIsViewData()
    {
        return isViewData;
    }
    public void setIsViewData(bool _isViewData)
    {
        isViewData = _isViewData;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        ////レイヤーマスク作成
        ////int layerMask = LayerMaskNo.DEFAULT;

        ////Rayの長さ
        //float maxDistance = 10;

        //RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance, layerMask);

        ////なにかと衝突した時だけそのオブジェクトの名前をログに出す
        //if (hit.collider)
        //{
        //    Debug.Log(hit.collider.gameObject.name);
        //}
    }

}
