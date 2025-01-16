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
}
