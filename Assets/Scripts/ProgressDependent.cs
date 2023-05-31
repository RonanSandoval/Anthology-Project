using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDependent : MonoBehaviour
{
    [SerializeField] bool disappear;
    [SerializeField] GameProgressManager.ProgressFlag flag;

    [SerializeField] GameObject disappearEffect;

    [SerializeField] bool fade;

    // Start is called before the first frame update
    void Start()
    {
        if ((disappear && GameProgressManager.Instance.checkProgress(flag)) || (!disappear && !GameProgressManager.Instance.checkProgress(flag))) {
            if (disappearEffect != null) {
                Instantiate(disappearEffect, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
            
        }
        
        GameProgressManager.Instance.onAddProgress.AddListener(checkProgress);
    }

    void checkProgress() {
        if ((disappear && GameProgressManager.Instance.checkProgress(flag)) || (!disappear && !GameProgressManager.Instance.checkProgress(flag))) {
            if (disappearEffect != null) {
                Instantiate(disappearEffect, transform.position, Quaternion.identity);
            }
            
            if (!fade) {
                Destroy(gameObject);
            } else {
                StartCoroutine(fadeCoroutine());
            }
            
        }
    }

    IEnumerator fadeCoroutine() {
        Image img = GetComponent<Image>();
        while (img.color.a > 0) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
