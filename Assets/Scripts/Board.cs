using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private RectTransform _boardTransform;
    [SerializeField] private Cell _cellPrefab;

    private void Start()
    {
        for (int x = 0; x < Config.BoardWidth; x++)
        {
            for (int y = 0; y < Config.BoardHeight; y++)
            {
                var cell = Instantiate(_cellPrefab, _boardTransform);
                cell.rectTransform.anchoredPosition = GetBoardPosition(new Point(x, y));
            }
        }
    }

    private Vector2 GetBoardPosition(Point point)
    {
        float width = _boardTransform.rect.width;
        float height = _boardTransform.rect.height;

        return new Vector2((-width / Config.IndentX + Config.PieceSize * point.X),
            height / Config.IndentY - Config.PieceSize * point.Y);
    }
}
