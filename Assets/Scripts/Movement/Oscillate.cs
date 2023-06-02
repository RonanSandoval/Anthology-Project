using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    Vector3 startPoint;

    float offset;

    void Start() {
        offset = Random.Range(0,10.1f);
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPoint.y + Mathf.Sin(offset + Time.time * frequency) * amplitude, transform.position.z);
    }
}
