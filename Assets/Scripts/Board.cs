using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable]
public class Level
{
    public List<JewelKind> board;
    public int width;
    public int height;

    public JewelKind this[int index]
    {
        get => board[index];
        set => this[index] = value;
    }

    public JewelKind this[int x, int y]
    {
        get => this[x + (y * width)];
        set => this[x + (y * width)] = value;
    }
}

public class Board
{
    private readonly MoveDirection[] allDirections = {
            MoveDirection.Up, MoveDirection.Down,
            MoveDirection.Left,MoveDirection.Right
    };

    private Level level;

    public Board(Level l) => level = l;

    public int GetWidth() => level.width;
    public int GetHeight() => level.height;

    private JewelKind GetJewel(int x, int y) => level[x, y];
    private JewelKind GetJewel(Vector2Int pos) => level[pos.x, pos.y];
    private void SetJewel(int x, int y, JewelKind kind) => level[x, y] = kind;

    //Implement this function
    public Move CalculateBestMoveForBoard()
    {
        int max = 1;
        Move finded = default;

        for (int y = 0; y < GetHeight(); y++)
        {
            for (int x = 0; x < GetWidth(); x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                foreach (var move in GetMoves(pos))
                {
                    JewelKind kind = GetJewel(pos);
                    Vector2Int newPos = pos + GetOffset(move.direction);
                    List<Vector2Int> matchesIdx = new List<Vector2Int>();
                    MoveDirection dir = move.direction;

                    matchesIdx.AddRange(
                        GetMatch(
                            newPos, 
                            kind,
                            MoveDirection.Horizontal.HasFlag(dir) ? dir : MoveDirection.Horizontal)
                        );
                    matchesIdx.AddRange(
                        GetMatch(
                            newPos,
                            kind,
                            MoveDirection.Vertical.HasFlag(dir) ? dir : MoveDirection.Vertical)
                        );

                    if (max < matchesIdx.Count)
                    {
                        max = matchesIdx.Count;
                        finded = move;
                    }
                }
            }
        }

        return finded;
    }

    private List<Vector2Int> GetMatch(Vector2Int start, JewelKind kind, MoveDirection direction)
    {
        Vector2Int[] offsets = allDirections
            .Where(f => direction.HasFlag(f))
            .Select(GetOffset)
            .ToArray();

        List<Vector2Int> match = new List<Vector2Int>();

        for (int i = 0; i < offsets.Length; i++)
        {
            var offset = offsets[i];

            Vector2Int idx = start + offset;
            while (IsValidIndex(ref idx))
            {
                if (kind == GetJewel(idx))
                    match.Add(idx);
                else
                    break;

                idx += offset;
            }
        }

        if (match.Count < 2)
            match.Clear();

        return match;

    }

    private IEnumerable<Move> GetMoves(Vector2Int pos)
    {
        foreach (var dir in allDirections)
        {
            var move = new Move()
            {
                x = pos.x,
                y = pos.y,
                direction = dir
            };

            if (IsValidMove(ref move))
                yield return move;
        }
    }

    private bool IsValidMove(ref Move move)
    {
        JewelKind kind = GetJewel(move.x, move.y);

        if (kind == JewelKind.Empty) return false;

        Vector2Int idx = new Vector2Int(move.x, move.y) + GetOffset(move.direction);

        if (!IsValidIndex(ref idx))
            return false;

        JewelKind nextKind = GetJewel(idx);

        if (kind != nextKind)
            return true;

        return false;
    }

    private bool IsValidIndex(ref Vector2Int idx)
        => GetWidth() > idx.x && idx.x >= 0 && GetHeight() > idx.y && idx.y >= 0;

    private Vector2Int GetOffset(MoveDirection direction)
    {
        if (direction == MoveDirection.Up)
            return Vector2Int.up;
        if (direction == MoveDirection.Down)
            return Vector2Int.down;
        if (direction == MoveDirection.Left)
            return Vector2Int.left;
        if (direction == MoveDirection.Right)
            return Vector2Int.right;

        return Vector2Int.zero;
    }
    
};


