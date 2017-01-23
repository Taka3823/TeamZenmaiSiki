using UnityEngine;

/// <summary>
/// エネミーの当たり判定。
/// 細かい部位ごとに判定を取るために、30*30のマップチップ方式で当たり判定を取っている。
/// </summary>
public class EnemyCollision {

	/// <summary>
	/// 当たり判定。
	/// </summary>
	/// <param name="_enemyPos">エネミーの位置。</param>
	/// <param name="_circlePos">止まったSecondaryCircleの位置。</param>
	/// <param name="_enemySize">エネミーのScale</param>
	/// <returns>当たった部位がどこなのか示すint値を返す。はずれ0:, 胴体:1…など。</returns>
    public int Collision(Vector3 _enemyPos, Vector3 _circlePos, Vector3 _enemySize)
    {
        float _division = 30;
        Vector3 diff = _circlePos - _enemyPos;
        Vector3 _tipSize = new Vector3(_enemySize.x / _division, _enemySize.y / _division, 0);
        Vector3 _hitPos = new Vector3(_enemySize.x / 2 + diff.x, _enemySize.y / 2 - diff.y);

        for (int i = 0; i < _division; i++)
        {
            for(int j = 0; j < _division; j++)
            {
                Vector2 _min = new Vector2(_tipSize.x * j, _tipSize.y * i);
                Vector2 _max = new Vector2(_tipSize.x * (j + 1), _tipSize.y * (i + 1));

                if (IsHit(_min, _max, _hitPos))
                {
                    return EnemyManager.Instance.CollisionIndex[EnemyManager.Instance.CurrentTargetIndex][i][j];
                }
            }
        }
        return 0;
    }

	/// <summary>
	/// ただ単にヒットしているかどうか。
	/// </summary>
	/// <param name="_min">画像の最小位置。</param>
	/// <param name="_max">画像の最大位置。</param>
	/// <param name="_hitPos">当たったとされる位置。</param>
	/// <returns></returns>
    private bool IsHit(Vector2 _min, Vector2 _max, Vector3 _hitPos)
    {
        if (!((_min.x <= _hitPos.x) && (_max.x >= _hitPos.x))) return false;
        if (!((_min.y <= _hitPos.y) && (_max.y >= _hitPos.y))) return false;
        return true;
    }
}
