using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, Mathf.Cos(Time.time * frequency) * amplitude * Time.deltaTime, 0));
    }
}
