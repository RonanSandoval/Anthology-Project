using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, -5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.EndingAcceptance)) {
            img.color = new Color(1, 1, 1, img.color.a + Time.deltaTime);
        }
    }
}
