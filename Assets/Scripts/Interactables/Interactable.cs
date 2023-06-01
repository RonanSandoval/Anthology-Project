using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all interactable objects (must press 'E')
public abstract class Interactable : MonoBehaviour
{
    protected GameObject keyIndicator;
    protected bool touchingPlayer;

    protected abstract void onInteract();

    protected virtual void Start()
    {
        keyIndicator = transform.Find("Key Indicator").gameObject;
        keyIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            keyIndicator.GetComponent<SpriteRenderer>().enabled = true;
            touchingPlayer = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            keyIndicator.GetComponent<SpriteRenderer>().enabled = false;
            touchingPlayer = false;
        }
    }

     public void Update()
 {
      if (Input.GetKeyDown(KeyCode.E) && touchingPlayer
      && !GameStateManager.Instance.checkState(GameStateManager.GameState.Talking))
     {
        onInteract();
     }

    if (GameStateManager.Instance.currentState == GameStateManager.GameState.Talking) {
        keyIndicator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    } else {
            keyIndicator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
 }
}
