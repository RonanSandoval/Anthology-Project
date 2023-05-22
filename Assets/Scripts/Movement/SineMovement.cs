using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    enum Dimension {X, Y, Z};

    [SerializeField] Dimension dimension;

    [SerializeField] float amplitude;
    [SerializeField] float speed;

    Vector3 startPoint;

    // Update is called once per frame
    void Update()
    {
        if (dimension == Dimension.X) {
            transform.Translate(new Vector3(Mathf.Sin(Time.time * speed) * amplitude * Time.deltaTime, 0, 0));
        } else if (dimension == Dimension.Y) {
            transform.Translate(new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude * Time.deltaTime, 0));
        } else if (dimension == Dimension.Z) {
            transform.Translate(new Vector3(0, 0, Mathf.Sin(Time.time * speed) * amplitude * Time.deltaTime));
        }
    }
}
