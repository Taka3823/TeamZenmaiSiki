using System.Collections;
using System.Collections.Generic;

/// <summary>
/// バトルシーンにおけるダメージ等の計算。
/// </summary>
public class BattleCalculation {

    /// <summary>
    /// プレイヤー・エネミー間の攻撃計算。
    /// </summary>
    /// <param name="_characterSTR">攻撃を仕掛ける側の攻撃力。</param>
    /// <param name="_characterDEF">攻撃を受ける側の防御力。</param>
    /// <returns>最終的なダメージ数値。</returns>
    public int FromPlayerToEnamyDamage(int _characterSTR, int _characterDEF)  
    {
        int _resultDamage = _characterSTR - _characterDEF;
        PointClamp(ref _resultDamage);
        return _resultDamage;
    }

    /// <summary>
    /// 負の数字にならないように調整。
    /// </summary>
    /// <param name="_result">0が返却</param>
    private void PointClamp(ref int _result)
    {
        if (_result >= 0) return;
            _result = 0;
    }
}
