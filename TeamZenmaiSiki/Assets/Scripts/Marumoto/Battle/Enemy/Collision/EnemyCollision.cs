using UnityEngine;
using System.Collections;

public class EnemyCollision {

    public int Collision(Vector3 _enemyPos, Vector3 _circlePos)
    {
        float _division = 30;
        Vector3 _pivotPos = _enemyPos - new Vector3(0.5f, -0.5f, 0);
        Vector3 _tipSize = new Vector3(1.0f / _division, 1.0f / _division, 0);
        Vector3 _hitPos = _circlePos - _pivotPos;
        _hitPos = new Vector3(_hitPos.x, -_hitPos.y);


        for(int i = 0; i < _division; i++)
        {
            for(int j = 0; j < _division; j++)
            {
                Vector2 _min = new Vector2(_tipSize.x * i, _tipSize.y * i);
                Vector2 _max = new Vector2(_tipSize.x * (i + 1), _tipSize.y * (i + 1));

                if (IsHit(_min, _max, _hitPos))
                {
                    return EnemyManager.Instance.CollisionIndex[EnemyManager.Instance.CurrentTargetIndex][i][j];
                }
            }
        }
        return 0;
    }

    private bool IsHit(Vector2 _min, Vector2 _max, Vector3 _hitPos)
    {
        if (!((_min.x <= _hitPos.x) && (_max.x >= _hitPos.x))) return false;
        if (!((_min.y <= _hitPos.y) && (_max.y >= _hitPos.y))) return false;
        return true;
    }
}
