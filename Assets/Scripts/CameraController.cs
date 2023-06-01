using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    private GameObject partnerObject;
    private bool onPartner = false;
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

        Vector3 targetPosition;
        if (GameStateManager.Instance.checkState(GameStateManager.GameState.Exploring)) {
            targetPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 3 + yOffset, playerObject.transform.position.z - 9);
        } else if (!onPartner) {
            targetPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + 2 + yOffset, playerObject.transform.position.z - 5);
        } else {
            try {
                targetPosition = new Vector3(partnerObject.transform.position.x, partnerObject.transform.position.y + 2 + yOffset, partnerObject.transform.position.z - 5);
            } catch {
                targetPosition = transform.position;
            }
        }
        

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

    public void setCameraPartner(GameObject partner) {
        partnerObject = partner;
    }

    public void setOnPartner(bool setting) {
        onPartner = setting;
    }
}
