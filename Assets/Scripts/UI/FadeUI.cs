using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    Image image;

    [SerializeField] float fadeSpeed;

    void Start() {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        GameSceneManager.Instance.onSceneChange.AddListener(fadeIn);
        fadeOut();
    }

    public void fadeIn() {
        StartCoroutine(fadeInCoroutine());
    }

    IEnumerator fadeInCoroutine() {
        while (image.color.a < 1) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }

        GameSceneManager.Instance.changeScene(); 
    }

    public void fadeOut() {
        StartCoroutine(fadeOutCoroutine());
    }

    IEnumerator fadeOutCoroutine() {
        while (image.color.a > 0) {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime * fadeSpeed / 2);
            yield return null;
        }
    }
}
