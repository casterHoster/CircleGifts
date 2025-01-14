using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform _boardTransform;
    [SerializeField] private Cell _cellPrefab;

    private void Start()
    {
        for (int x = 0; x < Config.BoardWidth; x++)
        {
            for (int y = 0; y < Config.BoardHeight; y++)
            {
                var cell = Instantiate(_cellPrefab, _boardTransform);
                cell.Transform.position = GetBoardPosition(new Point(x, y));
            }
        }
    }

    private Vector2 GetBoardPosition(Point point)
    {
        return new Vector2(Config.PieceSize / 2 + Config.PieceSize * point.X,
            -Config.PieceSize / 2 - Config.PieceSize * point.Y);
    }
}
