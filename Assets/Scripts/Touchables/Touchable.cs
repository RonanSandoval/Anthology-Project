using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all touchable objects (touch to interact)
public abstract class Touchable : MonoBehaviour
{
    protected abstract void onTouch();

    [SerializeField] protected bool needDash;

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (!needDash || other.GetComponent<Player>().isDashing) {
                onTouch();
            }
        }
    }

}
