using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Touchable
{
    protected override void onTouch()
    {
        Destroy(gameObject);
    }
}
