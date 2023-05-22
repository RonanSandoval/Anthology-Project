using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDependent : MonoBehaviour
{
    [SerializeField] bool disappear;
    [SerializeField] GameProgressManager.ProgressFlag flag;

    [SerializeField] GameObject disappearEffect;

    // Start is called before the first frame update
    void Start()
    {
        checkProgress();
        GameProgressManager.Instance.onAddProgress.AddListener(checkProgress);
    }

    void checkProgress() {
        if ((disappear && GameProgressManager.Instance.checkProgress(flag)) || (!disappear && !GameProgressManager.Instance.checkProgress(flag))) {
            if (disappearEffect != null) {
                Instantiate(disappearEffect, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}
