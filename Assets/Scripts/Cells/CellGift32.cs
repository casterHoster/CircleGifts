using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift32 : Cell
{
    private int _value = 32;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
