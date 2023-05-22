using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Interactable
{
    // Scene that this telepoint will go to
    public string toScene;
    public int toIndex;
    public int index;
    [SerializeField] GameProgressManager.ProgressFlag accessFlag;
    bool accessible;

    SpriteRenderer sr;

    protected override void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();

        accessible = GameProgressManager.Instance.checkProgress(accessFlag);
        sr.color = accessible ? new Color(1,1,1,0.5f) : new Color(1,1,1,0.1f);
        GameProgressManager.Instance.onAddProgress.AddListener(checkAccess);

        base.Start();
    }

    protected override void onInteract() {
        if (accessible) {
            GameSceneManager.Instance.setSpawnIndex(toIndex);
            GameSceneManager.Instance.changeSceneFade(toScene);
        }
    }

    void checkAccess() {
        accessible = GameProgressManager.Instance.checkProgress(accessFlag);
        sr.color = accessible ? new Color(1,1,1,0.5f) : new Color(1,1,1,0.1f);
    }
}
