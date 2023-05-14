using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Interactable
{
    // Scene that this telepoint will go to
    [SerializeField] private string toScene;
    
    public int index;

    protected override void onInteract() {
        GameSceneManager.Instance.changeScene(toScene);
    }
}
