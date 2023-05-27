using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    enum Dimension {X, Y, Z};

    [SerializeField] Dimension dimension;

    [SerializeField] float amplitude;
    [SerializeField] float speed;
    [SerializeField] bool negative;

    Vector3 startPoint;
    int dir;

    void Start() {
        startPoint = transform.position;
        dir = negative ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (dimension == Dimension.X) {
            transform.position = new Vector3(startPoint.x + ((Mathf.Sin(Time.time * speed) + 1) / 2 * amplitude * dir), startPoint.y, startPoint.z);
        } else if (dimension == Dimension.Y) {
            transform.position = new Vector3(-startPoint.z, startPoint.y + (Mathf.Sin(Time.time * speed) + 1) / 2 * amplitude * dir, startPoint.z);
        } else if (dimension == Dimension.Z) {
            transform.position = new Vector3(startPoint.x, startPoint.y, startPoint.z + (Mathf.Sin(Time.time * speed) + 1) / 2 * amplitude * dir);
        }
    }
}
