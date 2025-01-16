using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift256 : Cell
{
    private int _value = 256;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
