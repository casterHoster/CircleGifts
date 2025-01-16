using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift128 : Cell
{
    private int _value = 128;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
