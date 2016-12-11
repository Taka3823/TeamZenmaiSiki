using System;
using System.Collections;
using System.Collections.Generic;

public class CollisionData {
    public CollisionData()
    {
        CollisionPath = new List<string>();
        CollisionIndex = new List<List<List<int>>>();
        CollisionPath = EnemyManager.Instance.CollisionPath;

        CollisionCsvRead();
    }

    /// <summary>
    /// 当たり判定のインデックス。
    /// </summary>
    public List<List<List<int>>> CollisionIndex { get; private set; }

    /// <summary>
    /// 当たり判定ファイルのパス。
    /// </summary>
    private List<string> CollisionPath { get; set; }

    /// <summary>
    /// 当たり判定のCSVファイルを読み込み。
    /// </summary>
    private void CollisionCsvRead()
    {
        for (int k = 0; k < CollisionPath.Count; k++)
        {
            string[] _lines = ReadCsvFoundation.ReadCsvData(CollisionPath[k]);
            int _linesNum = _lines.Length;

            List<List<int>> _lineElems = new List<List<int>>();
            for (int i = 0; i < _linesNum; i++)
            {
                string[] _separatedData = ReadCsvFoundation.NotOptionDataSeparation(_lines[i], ",".ToCharArray(), 30);

                List<int> _elems = new List<int>();
                for (int j = 0; j < _separatedData.Length; j++)
                {
                    _elems.Add(Convert.ToInt32(_separatedData[j]));
                }
                _lineElems.Add(_elems);
            }
            CollisionIndex.Add(_lineElems);
        }
    }
}