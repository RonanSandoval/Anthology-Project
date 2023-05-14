using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Interactable
{
    // Scene that this telepoint will go to
    public string toScene;
    public int toIndex;
    
    public int index;

    protected override void onInteract() {
        GameSceneManager.Instance.setSpawnIndex(toIndex);
        GameSceneManager.Instance.changeScene(toScene);
    }
}
