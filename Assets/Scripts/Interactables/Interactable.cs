using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all interactable objects (must press 'E')
public abstract class Interactable : MonoBehaviour
{
    protected GameObject keyIndicator;

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
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            keyIndicator.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)
            && GameStateManager.Instance.checkState(GameStateManager.GameState.Exploring)) {
            onInteract();
        }
    }
}
