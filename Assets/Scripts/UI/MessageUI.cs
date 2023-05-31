using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public static MessageUI instance; 

    Image image;
    Text messageText;

    float boxOpacity;

    [SerializeField] float appearSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        image = GetComponent<Image>();
        messageText = GetComponentInChildren<Text>();

        boxOpacity = image.color.a;
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public void showTimedMessage(string message, float length) {
        StopAllCoroutines();
        messageText.text = message;
        StartCoroutine(showTimedCoroutine(length));
    }

    IEnumerator showTimedCoroutine(float length) {
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (messageText.color.a < 1f) {
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, messageText.color.a + Time.deltaTime * appearSpeed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime * boxOpacity * appearSpeed));
            yield return null;
        }

        yield return new WaitForSeconds(length);

        while (messageText.color.a > 0) {
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, messageText.color.a - Time.deltaTime * appearSpeed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime * boxOpacity * appearSpeed));
            yield return null;
        }
    }

    public void showMessage(string message) {
        StopAllCoroutines();
        messageText.text = message;
        StartCoroutine(showCoroutine());
    }

    IEnumerator showCoroutine() {
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (messageText.color.a < 1f) {
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, messageText.color.a + Time.deltaTime * appearSpeed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime * boxOpacity * appearSpeed));
            yield return null;
        }
    }

    public void hideMessage() {
        StopAllCoroutines();
        StartCoroutine(hideCoroutine());
    } 

    IEnumerator hideCoroutine() {
        while (messageText.color.a > 0) {
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, messageText.color.a - Time.deltaTime * appearSpeed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime * boxOpacity * appearSpeed));
            yield return null;
        }
    }
}
