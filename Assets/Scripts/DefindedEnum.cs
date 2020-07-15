public enum JewelKind : byte
{
    Empty = 0,
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet
};

[System.Flags]
public enum MoveDirection
{
    None  = 0,
    Up    = 1 << 0,
    Down  = 1 << 1,
    Left  = 1 << 2,
    Right = 1 << 3,

    Horizontal = Left | Right,
    Vertical = Up | Down,
};

public struct Move
{
    public int x;
    public int y;
    public MoveDirection direction;

    public override string ToString() => $"[{x};{y}] - {direction}";
};

