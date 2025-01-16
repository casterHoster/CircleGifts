using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift64 : Cell
{
    private int _value = 64;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
