using UnityEngine;
using System.Collections;

public interface ISceneBase
{
    //この関数は意図して遠回りな処理をさせる
    //状況によって編集がしやすいようにである。
    void SceneChange(string nextSceneName_);
}
