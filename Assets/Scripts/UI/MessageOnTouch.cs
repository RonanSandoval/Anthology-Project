using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageOnTouch : MonoBehaviour
{
    [SerializeField] string message;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            MessageUI.instance.showMessage(message);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            MessageUI.instance.hideMessage();
        }
    }
}
