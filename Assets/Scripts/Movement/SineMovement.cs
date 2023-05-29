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
    [SerializeField] bool cosMode;

    Vector3 startPoint;
    int dir;
    float offset;

    void Start() {
        startPoint = transform.position;
        dir = negative ? -1 : 1;
        offset = cosMode ? Mathf.PI / 2 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dimension == Dimension.X) {
            transform.position = new Vector3(startPoint.x + ((Mathf.Sin(offset + Time.time * speed ) + 1) / 2 * amplitude * dir), transform.position.y, transform.position.z);
        } else if (dimension == Dimension.Y) {
            transform.position = new Vector3(transform.position.x, startPoint.y + (Mathf.Sin(offset + Time.time * speed) + 1) / 2 * amplitude * dir, transform.position.z);
        } else if (dimension == Dimension.Z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPoint.z + (Mathf.Sin(offset + Time.time * speed) + 1) / 2 * amplitude * dir);
        }
    }
}
