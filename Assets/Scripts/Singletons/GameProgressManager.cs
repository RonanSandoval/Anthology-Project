using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance { get; private set; }

    public enum ProgressFlag {None, TalkedToDenial, CaughtDreamBunnies, FinishedDenial};

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

    void Start() {
        MessageUI.instance.showTimedMessage("Use ARROW KEYS to Move", 8f);
    }

    public void addProgress(ProgressFlag flag) {
        if (!progressComplete.Contains(flag)) {
            progressComplete.Add(flag);
        }

        if (flag == ProgressFlag.TalkedToDenial) {
            MessageUI.instance.showTimedMessage("Press SPACE to dash", 8f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setDash(true);
        }
    }

    public bool checkProgress(ProgressFlag flag) {
        return progressComplete.Contains(flag);
    }
}
