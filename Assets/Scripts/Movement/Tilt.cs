using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float speed;

    float offset;

    void Start() {
        offset = Random.Range(0, 10.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(offset + Time.time * speed) * amplitude));
    }
}
