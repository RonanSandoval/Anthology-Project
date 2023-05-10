using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    public enum ProgressFlag {None, TalkedToDenial, FinishedDenial};

    public List<ProgressFlag> progressComplete;

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
            progressComplete.Add(ProgressFlag.None);
        } 
    }
}
