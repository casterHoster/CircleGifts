using System;
using System.Numerics;
using Unity.VisualScripting;

[System.Serializable]
public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x; 
        Y = y;
    }

    public void Multiply(int value)
    {
        X *= value;
        Y *= value;
    }

    public void Add(Point point)
    {
        X += point.X;
        Y += point.Y;
    }

    public bool Equals(Point point)
    {
        return X == point.X && Y == point.Y;
    }

    public Vector2 ToVector()
    {
        return new Vector2(X, Y);
    }

    public static Point FromVector2(Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }

    public static Point FromVector2(Vector3 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }

    public static Point Multiply(Point point, int value)
    {
        return new Point(point.X * value, point.Y * value);
    }

    public static Point Add(Point point1, Point point2)
    {
        return new Point(point1.X + point2.X, point1.Y + point2.Y);
    }

    public static Point Clone(Point point)
    {
        return new Point(point.X, point.Y);
    }

    public static Point Zero()
    {
        return new Point(0, 0);
    }

    public static Point Up()
    {
        return new Point(0, 1);
    }

    public static Point Down()
    {
        return new Point(0, -1);
    }

    public static Point Left()
    {
        return new Point(-1, 0);
    }

    public static Point Right()
    {
        return new Point(1, 0);
    }
}
