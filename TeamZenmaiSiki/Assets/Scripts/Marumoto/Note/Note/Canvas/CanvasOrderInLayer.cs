using UnityEngine;
using System.Collections;

public class CanvasOrderInLayer : MonoBehaviour {
    [SerializeField]
    int layerIndex;

    void Awake()
    {
        this.transform.SetSiblingIndex(layerIndex);
    }

    //void Update()
    //{
    //    this.transform.SetSiblingIndex(layerIndex);
    //}
}
