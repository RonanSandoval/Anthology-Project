using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : Collectable
{
    SoundController sc;
    [SerializeField] GameObject disappearEffect;

    protected override void Start() {
        sc = GetComponent<SoundController>();
        base.Start();
    }

    protected override void onTouch()
    {
        sc.playSoundWorldly(0);
        GameTaskManager.Instance.updateTask(GameTaskManager.TaskName.Paper, 1);
        Instantiate(disappearEffect, transform.position, Quaternion.identity);
        base.onTouch();
        Destroy(gameObject);
    }
}
