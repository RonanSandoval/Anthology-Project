using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    public float cameraSpeed = 5f;

    private float cameraSin = 0f;
    public float cameraSinFrequency;
    public float cameraSinAmplitude;

    // Update is called once per frame
    void Update()
    {   
        cameraSin += Time.deltaTime * cameraSinFrequency;
        float yOffset = Mathf.Sin(cameraSin) * cameraSinAmplitude;

        Vector3 targetPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 2 + yOffset, playerObject.transform.position.z - 7);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
