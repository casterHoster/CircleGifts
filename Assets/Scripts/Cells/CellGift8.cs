using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift8 : Cell
{
    private int _value = 8;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
