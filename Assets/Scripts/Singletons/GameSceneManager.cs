using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public int spawnIndex;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this;
        } 
    }

    public void changeScene(string sceneName) {
       SceneManager.LoadScene(sceneName); 
    }

    public void setSpawnIndex(int index) {
        spawnIndex = index;
    }
}
