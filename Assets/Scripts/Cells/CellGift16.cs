using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGift16 : Cell
{
    private int _value = 16;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
