using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Touchable
{
    protected override void onTouch()
    {
        GameTaskManager.Instance.updateTask(0, 1);
        Destroy(gameObject);
    }
}