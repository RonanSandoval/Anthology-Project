using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Touchable
{
    protected override void onTouch()
    {
        Destroy(gameObject);
    }
}
