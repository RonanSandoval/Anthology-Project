using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public enum GameState {Exploring, Talking};
    public GameState currentState;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            currentState = GameState.Exploring;
            DontDestroyOnLoad(gameObject);
        } 
    }

    public GameState getCurrentState() {
        return currentState;
    }

    public bool checkState(GameState state) {
        return currentState == state;
    }

    public void setCurrentState(GameState state) {
        currentState = state;
    }
}
