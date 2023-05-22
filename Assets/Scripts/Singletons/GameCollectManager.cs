using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCollectManager : MonoBehaviour
{
    public static GameCollectManager Instance { get; private set; }

    [SerializeField] List<string> collection;

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
            collection = new List<string>(); 
        } 
    }

    public void collect(string name) {
        collection.Add(name);
    }

    public bool check(string name) {
        return collection.Contains(name);
    }
}
