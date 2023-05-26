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
    void Start() {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        transform.position = playerObject.transform.position;

        foreach(GameObject teleObject in GameObject.FindGameObjectsWithTag("Tele"))
        {
            try {
                if(teleObject.GetComponent<Telepoint>().index == GameSceneManager.Instance.spawnIndex)
                {
                    transform.position = teleObject.transform.position;
                    transform.Translate(new Vector3(0,1,0));
                    return;
                }
            } catch {}
        }
        
        transform.position = new Vector3(0,2,0);
    }

    void Update()
    {   
        cameraSin += Time.deltaTime * cameraSinFrequency;
        float yOffset = Mathf.Sin(cameraSin) * cameraSinAmplitude;

        Vector3 targetPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 3 + yOffset, playerObject.transform.position.z - 9);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
