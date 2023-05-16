using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    BoxCollider bc;
    [SerializeField] Material myMaterial;
    [SerializeField] float transitionSpeed;

    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().onDash.AddListener(toggle);

        activated = bc.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        float currOpacity = myMaterial.GetFloat("Opacity");

        if (activated) {
            myMaterial.SetFloat("Opacity", Mathf.Lerp(currOpacity, 1f, Time.deltaTime * transitionSpeed));
        } else {
            myMaterial.SetFloat("Opacity", Mathf.Lerp(currOpacity, 0.2f, Time.deltaTime * transitionSpeed));
        }
    }

    public void toggle() {
        if (bc.enabled) {
            bc.enabled = false;
            activated = false;
        } else {
            bc.enabled = true;
            activated = true;
        }
    }
}
