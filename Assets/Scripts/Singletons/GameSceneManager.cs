using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public int spawnIndex;
    public string toScene;

    public UnityEvent onSceneChange;

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

    public void changeSceneFade(string sceneName) {
        toScene = sceneName;

        if (sceneName == "Finale") {
            GameStateManager.Instance.setCurrentState(GameStateManager.GameState.Finale);
        }

        onSceneChange.Invoke();
    }

    public void changeScene(string sceneName) {
        toScene = sceneName;
        SceneManager.LoadScene(toScene); 
    }

    public void changeScene() {
        SceneManager.LoadScene(toScene);
    }

    public void setSpawnIndex(int index) {
        spawnIndex = index;
    }
}
