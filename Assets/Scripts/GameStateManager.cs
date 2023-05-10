using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState {Exploring, Talking};
    public GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Exploring;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
