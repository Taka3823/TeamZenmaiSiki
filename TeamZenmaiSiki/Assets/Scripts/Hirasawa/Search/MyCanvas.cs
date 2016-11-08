using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※ここを追加する

public class MyCanvas : MonoBehaviour
{

    static Canvas _canvas;
    void Start()
    {
        // Canvasコンポーネントを保持
        _canvas = GetComponent<Canvas>();
        SetInteractive("Button", false);
    }

    /// 表示・非表示を設定する
    public static void SetActive(string name, bool b)
    {
        foreach (Transform child in _canvas.transform)
        {
            // 子の要素をたどる
            if (child.name == name)
            {
                // 指定した名前と一致
                // 表示フラグを設定
                child.gameObject.SetActive(b);
                // おしまい
                return;
            }
        }
        // 指定したオブジェクト名が見つからなかった
        Debug.LogWarning("Not found objname:" + name);
    }
    public static void SetInteractive(string name, bool b)
    {
        foreach (Transform child in _canvas.transform)
        {
            // 子の要素をたどる
            if (child.name == "InfoTabBase")
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "BattleButton")
                    {
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.name == name)
                            {
                                // Buttonコンポーネントを取得する
                                Button btn = child3.GetComponent<Button>();
                                // 有効・無効フラグを設定
                                btn.interactable = b;
                                // おしまい
                                return;
                            }
                        }
                    }
                }
                    // 指定した名前と一致
            
            }
        }
        
        // 指定したオブジェクト名が見つからなかった
        Debug.LogWarning("Not found objname:" + name);
    }

}
