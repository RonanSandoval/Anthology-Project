using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Collectable
{
    protected override void onTouch()
    {
        base.onTouch();
        Destroy(gameObject);
    }
}
