using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift512 : Cell
{
    private int _value = 512;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
