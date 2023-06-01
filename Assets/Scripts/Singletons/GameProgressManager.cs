using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance { get; private set; }

    public enum ProgressFlag {
        None,
        TalkedToDenial,
        CaughtDreamBunnies,
        FinishedDenial,
        TalkedToAnger,
        BrokeRocks,
        FinishedAnger,
        TalkedToBargaining,
        ObtainedPapers,
        FinishedBargaining,
        TalkedToDepression,
        ObtainedFear,
        FinishedDepression,
        ReadyForFinale,
        AngerTeleport,
        GameStart,
        EndingDenial,
        EndingAnger,
        EndingBargaining,
        EndingDepression,
        EndingAcceptance
        };

    public List<ProgressFlag> progressComplete;

    public UnityEvent onAddProgress;

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            addProgress(ProgressFlag.GameStart);
        }
    }

    void Start() {
        MessageUI.instance.showMessage("Press E to Start");
    }

    public void addProgress(ProgressFlag flag) {
        if (!progressComplete.Contains(flag)) {
            progressComplete.Add(flag);
            onAddProgress.Invoke();

            if (flag == ProgressFlag.TalkedToDenial) {
                MessageUI.instance.showTimedMessage("Press Z or SPACE to dash", 8f);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setDash(true);
            }

            if (flag == ProgressFlag.GameStart) {
                MessageUI.instance.showTimedMessage("Use ARROW KEYS or WASD to Move", 8f);
            }
        }
    }

    public bool checkProgress(ProgressFlag flag) {
        return progressComplete.Contains(flag);
    }
}
