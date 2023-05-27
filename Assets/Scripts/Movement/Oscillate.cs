using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    float offset;

    void Start() {
        offset = Random.Range(0,10.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, Mathf.Cos(offset + Time.time * frequency) * amplitude * Time.deltaTime, 0));
    }
}
