using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Touchable
{
    public string collectID;

    protected virtual void Start() {
        if (GameCollectManager.Instance.check(collectID)) {
            Destroy(gameObject);
        }
    }

    protected override void onTouch() {
        GameCollectManager.Instance.collect(collectID);
    }
}
