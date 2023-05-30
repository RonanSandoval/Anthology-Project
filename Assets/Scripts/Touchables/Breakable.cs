using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Collectable
{
    [SerializeField] GameObject destroyEffect;

    SoundController sc;

    protected override void Start() {
        sc = GetComponent<SoundController>();
        base.Start();
    }

    protected override void onTouch()
    {
        base.onTouch();
        sc.playSoundWorldly(0);
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        GameTaskManager.Instance.updateTask(GameTaskManager.TaskName.Rocks, 1);
        Destroy(gameObject);
    }
}
